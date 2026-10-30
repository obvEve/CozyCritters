namespace UpdateHandler;

public class ModpackCache : IComparable<ModpackCache>
{
    public ModpackCache(Version version, DirectoryInfo cacheLocation)
    {
        Version = version;
        CacheDirectory = cacheLocation;
    }
    
    public Version Version { get; }
    
    public DirectoryInfo CacheDirectory { get; }

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
    
    public ModpackChanges Compare(ModpackCache other)
    {
        return null!;
        // Compare files between this cache and another.
    }
}