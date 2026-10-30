namespace UpdateHandler;

public static class CacheController
{
    private const string CozyCritterFolder = "CozyCritters";
    private const string LastKnownDirectoryCache = "LastKnownDirectoryCache.dat";

    public static DirectoryInfo CacheDirectory
    {
        get
        {
            if (field == null)
            {
                DirectoryInfo? directory = new(AppContext.BaseDirectory);

                while (directory != null)
                {
                    if (directory.Name == CozyCritterFolder)
                        return field = Directory.CreateDirectory(Path.Combine(directory.FullName, "CustomScripts", "_Cache"));

                    directory = directory.Parent;
                }

                throw new DirectoryNotFoundException("Could not find the CozyCritters directory.");
            }

            return field;
        }
    }

    public static string? LastKnownDirectoryLocation
    {
        get
        {
            if (field == null)
            {
                string path = Path.Combine(CacheDirectory.FullName, LastKnownDirectoryCache);
                field = File.Exists(path) ? File.ReadAllText(path) : null;
            }

            return field;
        }
        set
        {
            if (value == null)
                return;
            
            field = value;
            File.WriteAllText(Path.Combine(CacheDirectory.FullName, LastKnownDirectoryCache), value);
        }
    }

    public static DirectoryInfo ActiveModpackDirectory
    {
        get;
        set
        {
            field = value;
            LastKnownDirectoryLocation = value.FullName;
        }
    }

    public static DirectoryInfo GetDirectoryForVersion(Version version) => CacheDirectory.CreateSubdirectory($"v{version}");

    public static void SaveVersionCache(Version version)
    {
        DirectoryInfo directory = GetDirectoryForVersion(version);
        
        ModpackCache cache = new(version, directory);
        
        if (directory.EnumerateFileSystemInfos().Any())
        {
            Console.WriteLine($"Cache for version {version} already exists.");
            return;
        }

        cache.Read();
    }

    public static void GenerateChangelog(Version newVersion)
    {
        ModpackCache? oldVersion = GetLatestCachedVersion(exclude: newVersion);

        if (oldVersion == null)
        {
            Console.WriteLine("No previous version found.");
            return;
        }

        DirectoryInfo newDirectory = GetDirectoryForVersion(newVersion);

        if (!newDirectory.EnumerateFileSystemInfos().Any())
            SaveVersionCache(newVersion);

        // Compare oldDirectory and newDirectory
        // Generate changelog
        ModpackChanges.Generate(oldVersion, new ModpackCache(newVersion, GetDirectoryForVersion(newVersion))).GenerateMarkdownChangelog();
    }
    
    private static ModpackCache? GetLatestCachedVersion(Version? exclude = null)
    {
        return CacheDirectory.GetDirectories("v*")
            .Select(d => Version.TryParse(d.Name[1..], out Version? version)
                ? new ModpackCache(version, d)
                : null)
            .Where(c => c != null && (exclude == null || c.Version != exclude))
            .Max();
    }
}