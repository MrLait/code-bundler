namespace CodeBundler.Bundling;

public sealed class BundleOptions
{
    public required string ScanRoot { get; init; }
    public required string OutputPath { get; init; }

    public required HashSet<string> ExcludeDirs { get; init; }
    public required HashSet<string> ExcludeFileNames { get; init; }
    public required HashSet<string> ExcludeFileExtensions { get; init; }

    public static BundleOptions Create(
        string scanRoot,
        string outputPath,
        IEnumerable<string> excludeDirs,
        IEnumerable<string> excludeFileNames,
        IEnumerable<string> excludeFileExtensions)
        => new()
        {
            ScanRoot = Path.GetFullPath(scanRoot).TrimEnd(Path.DirectorySeparatorChar),
            OutputPath = Path.GetFullPath(outputPath),

            ExcludeDirs = new HashSet<string>(excludeDirs, StringComparer.OrdinalIgnoreCase),
            ExcludeFileNames = new HashSet<string>(excludeFileNames, StringComparer.OrdinalIgnoreCase),
            ExcludeFileExtensions = new HashSet<string>(excludeFileExtensions, StringComparer.OrdinalIgnoreCase),
        };
}