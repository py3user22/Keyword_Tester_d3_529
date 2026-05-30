using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeScannerApp
{
    public static class GlobalKeywords
    {
        // Simple keyword matches
        public static readonly string[] MaliciousKeywords =
        {
            "cmd.exe",
            "powershell",
            "curl",
            "wget",
            "nc.exe",
            "mimikatz",
            "reverse shell",
            "shellcode",
            "dropper",
            "keylogger"
        };

        // Suspicious file extensions
        public static readonly string[] SuspiciousExtensions =
        {
            ".exe",
            ".dll",
            ".bat",
            ".ps1",
            ".vbs",
            ".js",
            ".scr",
            ".jar"
        };

        // Regex patterns
        public static readonly string[] MaliciousPatterns =
        {
            @"[A-Za-z0-9+/]{40,}={0,2}",           // Base64-like
            @"\b\d{1,3}(\.\d{1,3}){3}:\d{2,5}\b",  // IP:port
            @"https?://[^\s""']+"                 // URL
        };

        // ⭐ NEW METHOD: Loop through all keyword lists
        public static List<string> ScanLine(string line)
        {
            List<string> findings = new();

            if (string.IsNullOrWhiteSpace(line))
                return findings;

            // 1. Keyword matches
            foreach (var keyword in MaliciousKeywords)
            {
                if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    findings.Add($"Keyword match: {keyword}");
            }

            // 2. Suspicious extensions
            foreach (var ext in SuspiciousExtensions)
            {
                if (line.Contains(ext, StringComparison.OrdinalIgnoreCase))
                    findings.Add($"Suspicious extension: {ext}");
            }

            // 3. Regex patterns
            foreach (var pattern in MaliciousPatterns)
            {
                if (Regex.IsMatch(line, pattern))
                    findings.Add($"Pattern match: {pattern}");
            }

            return findings;
        }
    }
}
