namespace UpdateHandler;

public class ModpackChanges
{
    public bool AnyChanges { get; set; }
    
    /// <summary>
    /// Generates the changelog between 2 <see cref="ModpackCache"/>s and returns a <see cref="ModpackChanges"/> containing every change.
    /// </summary>
    /// <returns></returns>
    public static ModpackChanges Generate(ModpackCache oldVersion, ModpackCache newVersion)
    {
    }

    public string GetMarkdownChangelog()
    {
    }
}