namespace CodeBundler.Bundling;

public sealed class BundleFilters
{
    private readonly BundleOptions _opt;
    private readonly string _fullRoot;

    public BundleFilters(BundleOptions opt)
    {
        _opt = opt;
        _fullRoot = Path.GetFullPath(_opt.ScanRoot).TrimEnd(Path.DirectorySeparatorChar);
    }

    public bool ShouldExclude(string filePath)
        => IsOutputFile(filePath)
           || IsInExcludedDir(filePath)
           || IsExcludedFileName(filePath)
           || IsExcludedExtension(filePath);

    private bool IsOutputFile(string filePath)
        => Path.GetFullPath(filePath)
            .Equals(_opt.OutputPath, StringComparison.OrdinalIgnoreCase);

    private bool IsExcludedFileName(string filePath)
        => _opt.ExcludeFileNames.Contains(Path.GetFileName(filePath));

    private bool IsExcludedExtension(string filePath)
    {
        var ext = Path.GetExtension(filePath);
        return !string.IsNullOrEmpty(ext) && _opt.ExcludeFileExtensions.Contains(ext);
    }

    private bool IsInExcludedDir(string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);

        while (!string.IsNullOrEmpty(dir))
        {
            if (_opt.ExcludeDirs.Contains(Path.GetFileName(dir)))
                return true;

            var fullDir = Path.GetFullPath(dir).TrimEnd(Path.DirectorySeparatorChar);
            if (string.Equals(fullDir, _fullRoot, StringComparison.OrdinalIgnoreCase))
                break;

            dir = Path.GetDirectoryName(dir);
        }

        return false;
    }
}