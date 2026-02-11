using CodeBundler.Bundling;

string[] EXCLUDE_DIRS = ["bin", "obj", ".git", ".vs", "node_modules", "Migrations", "build"];
string[] EXCLUDE_FILE_NAMES = [".gitignore"];
string[] EXCLUDE_FILE_EXTENSIONS = [".code-workspace", ".yml", ".dockerignore"];

var scanRoot = (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
    ? Path.GetFullPath(args[0])
    : Directory.GetCurrentDirectory();

if (!Directory.Exists(scanRoot))
{
    Console.Error.WriteLine($"Folder not found: {scanRoot}");
    return;
}

var outputPath = Path.Combine(
    Directory.GetCurrentDirectory(),
    $"bundle_{Path.GetFileName(scanRoot.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))}.txt"
);

var options = BundleOptions.Create(scanRoot, outputPath, EXCLUDE_DIRS, EXCLUDE_FILE_NAMES, EXCLUDE_FILE_EXTENSIONS);

var collector = new FileCollector(options);
var files = collector.CollectFiles();

ConsoleReporter.PrintRunInfo(options, files);

var writer = new BundleWriter(options);
writer.WriteBundle(files);

Console.WriteLine($"Done. Written: {options.OutputPath}");