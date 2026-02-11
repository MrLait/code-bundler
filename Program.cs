using System.Text;

var excludeDirs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    "bin", "obj", ".git", ".vs", "node_modules", "Migrations"
};

var excludeFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".gitignore"
};

var excludeFileExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".code-workspace", ".yml", ".json", ".csproj"
};

var includeExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".cs", ".csproj", ".sln", ".json", ".yml", ".yaml", ".xml", ".md"
};

string scanRoot;
if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
    scanRoot = args[0];
else
    scanRoot = Directory.GetCurrentDirectory();

scanRoot = Path.GetFullPath(scanRoot);

if (!Directory.Exists(scanRoot))
{
    Console.Error.WriteLine($"Folder not found: {scanRoot}");
    return;
}

var outputDir = Directory.GetCurrentDirectory();
var outputPath = Path.Combine(outputDir, "bundle.txt");

var fullRoot = Path.GetFullPath(scanRoot).TrimEnd(Path.DirectorySeparatorChar);

bool IsInExcludedDir(string filePath)
{
    var dir = Path.GetDirectoryName(filePath);
    while (!string.IsNullOrEmpty(dir))
    {
        var name = Path.GetFileName(dir);
        if (excludeDirs.Contains(name))
            return true;

        var fullDir = Path.GetFullPath(dir).TrimEnd(Path.DirectorySeparatorChar);

        if (string.Equals(fullDir, fullRoot, StringComparison.OrdinalIgnoreCase))
            break;

        dir = Path.GetDirectoryName(dir);

    }

    return false;
}

bool IsExcludedFileOrExtension(string filePath)
{
    return IsExcludedFile(filePath) || IsExcludedExtension(filePath);
}

bool IsExcludedFile(string filePath)
{
    var fileName = Path.GetFileName(filePath);
    return excludeFileNames.Contains(fileName);
}

bool IsExcludedExtension(string filePath)
{
    var ext = Path.GetExtension(filePath);
    if (!string.IsNullOrEmpty(ext) && excludeFileExtensions.Contains(ext))
        return true;

    return false;
}

bool IsIncludedByExtension(string filePath)
{
    var ext = Path.GetExtension(filePath);
    return !string.IsNullOrEmpty(ext) && includeExtensions.Contains(ext);
}

var outputFull = Path.GetFullPath(outputPath);

var files = Directory.EnumerateFiles(scanRoot, "*", SearchOption.AllDirectories)
    .Where(f => !Path.GetFullPath(f).Equals(outputFull, StringComparison.OrdinalIgnoreCase))
    .Where(f => !IsInExcludedDir(f))
    .Where(f => !IsExcludedFileOrExtension(f))
    .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
    .ToList();

files = [.. files
    .Where(f => IsIncludedByExtension(f))
    .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)];


var sb = new StringBuilder();

Console.WriteLine($"Scan root: {scanRoot}");
Console.WriteLine($"Output:    {outputPath}");
Console.WriteLine();

Console.WriteLine("Files to include:");
Console.WriteLine($"Total: {files.Count}");
Console.WriteLine();

foreach (var f in files)
{
    Console.WriteLine(Path.GetRelativePath(scanRoot, f));
}
Console.WriteLine(new string('-', 60));


sb.AppendLine($"# Bundle generated at: {DateTimeOffset.Now:O}");
sb.AppendLine($"# Root: {scanRoot}");
sb.AppendLine($"# Files: {files.Count}");
sb.AppendLine();

foreach (var file in files)
{
    var relPath = Path.GetRelativePath(scanRoot, file);

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

File.WriteAllText(outputPath, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

Console.WriteLine($"Done. Written: {outputPath}");
Console.ReadLine();