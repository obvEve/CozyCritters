namespace UpdateHandler;

public class ModpackChanges
{
    public bool AnyChanges { get; set; }
    
    /// <summary>
    /// Generates the changelog between 2 <see cref="ModpackCache"/>s and returns a <see cref="ModpackChanges"/> containing every change.
    /// </summary>
    public static ModpackChanges Generate(ModpackCache oldVersion, ModpackCache newVersion)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a proper mark-down version of what <see cref="Generate"/> produces.
    /// </summary>
    public string GenerateMarkdownChangelog() => throw new NotImplementedException();
}