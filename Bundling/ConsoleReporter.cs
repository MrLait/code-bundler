namespace CodeBundler.Bundling;

public static class ConsoleReporter
{
    public static void PrintRunInfo(BundleOptions opt, IReadOnlyList<string> files)
    {
        Console.WriteLine($"Scan root: {opt.ScanRoot}");
        Console.WriteLine($"Output:    {opt.OutputPath}");
        Console.WriteLine();

        Console.WriteLine("Files to include:");
        Console.WriteLine($"Total: {files.Count}");
        Console.WriteLine();

        foreach (var f in files)
            Console.WriteLine(Path.GetRelativePath(opt.ScanRoot, f));

        Console.WriteLine(new string('-', 60));
        Console.WriteLine();
    }
}