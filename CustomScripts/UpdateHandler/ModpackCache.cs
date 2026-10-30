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
    
    public ModpackCache(Version version, DirectoryInfo cacheLocation)
    {
        Version = version;
        CacheDirectory = cacheLocation;
    }
    
    public Version Version { get; }
    public DirectoryInfo CacheDirectory { get; }
    
    public string[] Mods { get; private set; }
    public FileInfo[] Configs { get; private set; }
    public FileInfo[] KubeJs { get; private set; }
    public string[] Shaders { get; private set; }
    public string[] ResourcePacks { get; private set; }

    public void Read(bool @override = false)
    {
        if (!@override && CacheDirectory.EnumerateFileSystemInfos().Any())
        {
            Generate();
            return;
        }
        
        Mods = new DirectoryInfo(Path.Combine(CacheDirectory.FullName, ModsDirectory)).GetFiles().Select(x => x.Name).ToArray();
        Configs = new DirectoryInfo(Path.Combine(CacheDirectory.FullName, ConfigsDirectory)).EnumerateFileSystemInfos().Select(); // need to get all directories and files and note their info
        KubeJs = new DirectoryInfo(Path.Combine(CacheDirectory.FullName, KubeJsDirectory)).EnumerateFileSystemInfos().Select();
        Shaders = new DirectoryInfo(Path.Combine(CacheDirectory.FullName, ShadersDirectory)).GetFiles().Select(x => x.Name).ToArray();
        ResourcePacks = new DirectoryInfo(Path.Combine(CacheDirectory.FullName, ResourcePacksDirectory)).GetFiles().Select(x => x.Name).ToArray();
    }

    public void Generate()
    {
        // write all Mods, Configs, KubeJs, Shaders and Resourcepacks
        // mods.json
        // config/[all files]
        // kubejs/[all files]
        // shaderpacks.json
        // resourcepacks.json
    }

    private void InternalRead()
    {
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