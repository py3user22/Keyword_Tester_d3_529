using System;
using System.Collections.Generic;

namespace CodeScannerApp
{
    public static class KeywordViewer
    {
        public static void ShowMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== VIEW KEYWORD LISTS ===");
                Console.WriteLine("1. Using Keywords");
                Console.WriteLine("2. Member Keywords");
                Console.WriteLine("3. Loop Keywords");
                Console.WriteLine("4. Base64 Patterns");
                Console.WriteLine("5. XOR Indicators");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        PrintList("Using Keywords", GlobalKeywords.MaliciousKeywords);
                        break;

                    case "2":
                        PrintList("Member Keywords", GlobalKeywords.SuspiciousExtensions);
                        break;

                    case "3":
                        PrintList("Loop Keywords", GlobalKeywords.MaliciousPatterns);
                        break;

                    case "4":
                        PrintList("Base64 Regex Patterns", GlobalKeywords.MaliciousKeywords);
                        break;

                    case "5":
                        PrintList("XOR Indicators", GlobalKeywords.XorIndicators);
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

        private static void PrintList(string title, IEnumerable<string> list)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            foreach (var item in list)
                Console.WriteLine("- " + item);

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
