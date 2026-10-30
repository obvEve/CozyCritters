namespace UpdateHandler;

/// <summary>
/// Stores info about the modpack.
/// <remarks>Used to generate changelogs between multiple caches.</remarks>
/// </summary>
public class ModpackCache : IComparable<ModpackCache>
{
    public ModpackCache(Version version, DirectoryInfo cacheLocation)
    {
        Version = version;
        CacheDirectory = cacheLocation;
    }
    
    public Version Version { get; }
    public DirectoryInfo CacheDirectory { get; }
    
    public List<string> Mods { get; private set; }
    public List<FileInfo> Configs { get; private set; }
    public List<FileInfo> KubeJs { get; private set; }
    public List<string> Shaders { get; private set; }
    public List<string> ResourcePacks { get; private set; }

    public void Read(bool @override = false)
    {
        if (!@override && CacheDirectory.EnumerateFileSystemInfos().Any())
        {
            Generate();
            return;
        }
    }

    public void Generate()
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