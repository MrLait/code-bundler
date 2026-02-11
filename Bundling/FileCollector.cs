namespace CodeBundler.Bundling;

public sealed class FileCollector
{
    private readonly BundleOptions _opt;
    private readonly BundleFilters _filters;

    public FileCollector(BundleOptions opt)
    {
        _opt = opt;
        _filters = new BundleFilters(opt);
    }

    public List<string> CollectFiles()
        => [.. Directory.EnumerateFiles(_opt.ScanRoot, "*", SearchOption.AllDirectories)
            .Where(f => !_filters.ShouldExclude(f))
            .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)];
}