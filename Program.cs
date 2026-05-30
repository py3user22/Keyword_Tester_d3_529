using System;
using System.IO;
using CodeScannerApp;

namespace CodeScannerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ensure a file path was provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: scanner.exe <path-to-text-file>");
                return;
            }

            string path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            Console.WriteLine("=== Scan Results ===");

            int lineNumber = 1;

            foreach (var line in File.ReadLines(path))
            {
                var results = GlobalKeywords.ScanLine(line);

                foreach (var result in results)
                {
                    Console.WriteLine($"Line {lineNumber}: {result}");
                }

                lineNumber++;
            }

            Console.WriteLine("=== Scan Complete ===");
        }
    }
}
