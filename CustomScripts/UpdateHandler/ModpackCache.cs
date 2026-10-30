using System.Collections.ObjectModel;

namespace UpdateHandler;

/// <summary>
/// Stores info about the modpack.
/// <remarks>Used to generate changelogs between multiple caches.</remarks>
/// </summary>
public class ModpackCache : IComparable<ModpackCache>
{
    public const string ModsDirectory = "mods";
    public const string ConfigsDirectory = "config";
    public const string KubeJsDirectory = "kubejs";
    public const string ShadersDirectory = "shaderpacks";
    public const string ResourcePacksDirectory = "resourcepacks";
    public const string DataFileExtension = ".dat";
    
    public ModpackCache(Version version, DirectoryInfo cacheLocation)
    {
        Version = version;
        CacheDirectory = cacheLocation;
    }
    
    public Version Version { get; }
    public DirectoryInfo CacheDirectory { get; }
    
    public string[] Mods { get; private set; }
    public ReadOnlyDictionary<string, string> Configs { get; private set; } // File path relative to config directory + file contents
    public ReadOnlyDictionary<string, string> KubeJs { get; private set; } // File path relative to config directory + file contents
    public string[] Shaders { get; private set; }
    public string[] ResourcePacks { get; private set; }

    public void Read(bool @override = false)
    {
        if (@override || !CacheDirectory.EnumerateFileSystemInfos().Any())
        {
            Generate();
            return;
        }

        Mods = File.ReadAllLines(Path.Combine(CacheDirectory.FullName, ModsDirectory + DataFileExtension));

        Configs = new ReadOnlyDictionary<string, string>(CacheDirectory
            .GetFiles("*", SearchOption.AllDirectories)
            .ToDictionary(file => Path.GetRelativePath(CacheDirectory.FullName, file.FullName), file => File.ReadAllText(file.FullName)));
        KubeJs = new ReadOnlyDictionary<string, string>(CacheDirectory
            .GetFiles("*", SearchOption.AllDirectories)
            .ToDictionary(file => Path.GetRelativePath(CacheDirectory.FullName, file.FullName), file => File.ReadAllText(file.FullName)));

        Shaders = File.ReadAllLines(Path.Combine(CacheDirectory.FullName, ShadersDirectory + DataFileExtension));
        ResourcePacks = File.ReadAllLines(Path.Combine(CacheDirectory.FullName, ResourcePacksDirectory + DataFileExtension));
    }

    public void Generate()
    {
        DirectoryInfo activeConfigDirectory = new(Path.Combine(CacheController.ActiveModpackDirectory.FullName, ConfigsDirectory));

        // Read all Mods, Configs, KubeJs, Shaders and Resourcepacks
        Mods = new DirectoryInfo(Path.Combine(CacheController.ActiveModpackDirectory.FullName, ModsDirectory)).GetFiles().Select(x => x.Name).ToArray();

        Configs = new ReadOnlyDictionary<string, string>(activeConfigDirectory
            .GetFiles("*", SearchOption.AllDirectories)
            .ToDictionary(file => Path.GetRelativePath(activeConfigDirectory.FullName, file.FullName), file => File.ReadAllText(file.FullName)));

        DirectoryInfo kubeDirectory = new(Path.Combine(CacheController.ActiveModpackDirectory.FullName, KubeJsDirectory));
        KubeJs = new ReadOnlyDictionary<string, string>(kubeDirectory
            .GetFiles("*", SearchOption.AllDirectories)
            .ToDictionary(file => Path.GetRelativePath(kubeDirectory.FullName, file.FullName), file => File.ReadAllText(file.FullName)));

        Shaders = new DirectoryInfo(Path.Combine(CacheController.ActiveModpackDirectory.FullName, ShadersDirectory)).GetFiles().Select(x => x.Name).ToArray();
        ResourcePacks = new DirectoryInfo(Path.Combine(CacheController.ActiveModpackDirectory.FullName, ResourcePacksDirectory)).GetFiles().Select(x => x.Name).ToArray();
        
        // write all Mods, Configs, KubeJs, Shaders and Resourcepacks
        // mods.dat
        // config/[all files]
        // kubejs/[all files]
        // shaderpacks.dat
        // resourcepacks.dat
        DirectoryInfo cacheConfigDirectory = new(Path.Combine(CacheDirectory.FullName, ConfigsDirectory));
        cacheConfigDirectory.Create();
        DirectoryInfo cacheKubeJsDirectory = new(Path.Combine(CacheDirectory.FullName, ConfigsDirectory));
        cacheKubeJsDirectory.Create();
        
        File.WriteAllText(Path.Combine(CacheDirectory.FullName, ModsDirectory + DataFileExtension), string.Join(Environment.NewLine, Mods));
        foreach ((string relativePath, string contents) in Configs)
        {
            string filePath = Path.Combine(cacheConfigDirectory.FullName, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, contents);
        }
        foreach ((string relativePath, string contents) in KubeJs)
        {
            string filePath = Path.Combine(cacheKubeJsDirectory.FullName, relativePath);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, contents);
        }
        /*File.WriteAllText(Path.Combine(CacheDirectory.FullName, ConfigsDirectory + DataFileExtension), string.Join(Environment.NewLine, Configs));
        File.WriteAllText(Path.Combine(CacheDirectory.FullName, KubeJsDirectory + DataFileExtension), string.Join(Environment.NewLine, KubeJs));*/
        File.WriteAllText(Path.Combine(CacheDirectory.FullName, ShadersDirectory + DataFileExtension), string.Join(Environment.NewLine, Shaders));
        File.WriteAllText(Path.Combine(CacheDirectory.FullName, ResourcePacksDirectory + DataFileExtension), string.Join(Environment.NewLine, ResourcePacks));
        Console.WriteLine($"Generated {CacheDirectory.FullName}");
    }

    /// <summary>
    /// Compares version.
    /// </summary>
    public int CompareTo(ModpackCache? other) => other is null ? 1 : Version.CompareTo(other.Version);
    
    /// <summary>
    /// Gets a <see cref="ModpackChanges"/> between 2 versions.
    /// </summary>
    public ModpackChanges Compare(ModpackCache other) => ModpackChanges.Generate(other, this);
}