using System;

namespace CodeScannerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== MALWARE SCANNER ===");
                Console.WriteLine("1. Scan a file");
                Console.WriteLine("2. View keyword lists");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunFileScan();
                        break;

                    case "2":
                        KeywordViewer.ShowMenu();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void RunFileScan()
        {
            Console.Clear();
            Console.Write("Enter file path to scan: ");
            string path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n=== SCAN RESULTS ===");

            int lineNumber = 1;
            foreach (var line in File.ReadLines(path))
            {
                var results = GlobalKeywords.ScanLine(line);

                foreach (var hit in results)
                    Console.WriteLine($"Line {lineNumber}: {hit}");
                
                lineNumber++;
            }

            Console.WriteLine("\nScan complete. Press any key...");
            Console.ReadKey();
        }
    }
}
