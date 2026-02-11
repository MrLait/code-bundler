using System.Text;

namespace CodeBundler.Bundling;

public sealed class BundleWriter
{
    private readonly BundleOptions _opt;

    public BundleWriter(BundleOptions opt) => _opt = opt;

    public void WriteBundle(IReadOnlyList<string> files)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"# Bundle generated at: {DateTimeOffset.Now:O}");
        sb.AppendLine($"# Root: {_opt.ScanRoot}");
        sb.AppendLine($"# Files: {files.Count}");
        sb.AppendLine();

        foreach (var file in files)
        {
            var relPath = Path.GetRelativePath(_opt.ScanRoot, file);

            sb.AppendLine($"FILE: {relPath}");

            try
            {
                var content = File.ReadAllText(file, Encoding.UTF8);
                sb.AppendLine(content.TrimEnd());
            }
            catch (Exception ex)
            {
                sb.AppendLine($"# ERROR reading file: {ex.Message}");
            }

            sb.AppendLine();
        }

        File.WriteAllText(_opt.OutputPath, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }
}