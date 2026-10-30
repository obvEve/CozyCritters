using System.Text.RegularExpressions;

namespace UpdateHandler;

public static class Program
{
    public static CacheTypeData[] CacheTypes { get; } = [
        new(CacheType.Configs, "config"),
        new(CacheType.Mods, "mods", StoredCacheType.FileName),
        new(CacheType.KubeJs, "kubejs"),
        new(CacheType.Shaders, "shaderpacks", StoredCacheType.FileName),
        new(CacheType.ResourcePacks, "resourcepacks", StoredCacheType.FileName)];
    
    public static void Main(string[] args)
    {
        CacheController.ActiveModpackDirectory = GetModpackDirectoryCache(args);

        Console.WriteLine("Generate Cache (gc) or Create Changelog (cc)?");
        Action action = Console.ReadLine()?.Trim().ToLowerInvariant() switch
        {
            "gc" or "cache" => Action.CreateCache,
            "cc" or "changelog" => Action.CreateChangelog,
            _ => Action.None
        };
        
        if (action == Action.None)
            return;

        Version version = ReadVersion();

        switch (action)
        {
            case Action.CreateCache:
                CacheController.SaveVersionCache(version);
                break;

            case Action.CreateChangelog:
                CacheController.GenerateChangelog(version);
                break;
        }
    }

    private static DirectoryInfo GetModpackDirectoryCache(string[] args)
    {
        string? lastKnownDirectory = CacheController.LastKnownDirectoryLocation;
        if (!string.IsNullOrEmpty(lastKnownDirectory))
        {
            if (Directory.Exists(lastKnownDirectory))
            {
                return new DirectoryInfo(lastKnownDirectory);
            }
        }

        // check args
        
        // 3. Ask user
        while (true)
        {
            Console.WriteLine("Please provide your current modpack directory!");
            string? input = Console.ReadLine()?.Trim().Trim('"');

            if (string.IsNullOrWhiteSpace(input) || !Directory.Exists(input))
            {
                Console.WriteLine("Directory does not exist. Please try again.");
                continue;
            }

            CacheController.LastKnownDirectoryLocation = input;
            return new DirectoryInfo(input);
        }
    }
    
    private static Version ReadVersion()
    {
        Console.WriteLine("Input version:");

        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (input != null && Regex.IsMatch(input, @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)$") && Version.TryParse(input, out Version? version))
                return version;

            Console.WriteLine("Invalid version. Expected format: 1.0.0");
        }
    }

    public record class CacheTypeData(CacheType Type, string Location, StoredCacheType StoredCacheType = StoredCacheType.File);

    public enum CacheType
    {
        Configs,
        KubeJs,
        Mods,
        Shaders,
        ResourcePacks,
    }

    public enum StoredCacheType
    {
        File,
        FileName,
    }
    
    private enum Action
    {
        None,
        CreateCache,
        CreateChangelog,
    }
}