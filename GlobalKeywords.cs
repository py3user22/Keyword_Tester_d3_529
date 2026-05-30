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

        public static List<string> XorIndicators = new()
        {
            "xor",
            "^",
            "0x",
            "0xFF",
            "0xAA",
            "0x55",
            "byte[] xor",
            "xor decrypt",
            "xor encode",
            "xor key",
            "xor loop"
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

            // 4. XOR indicators
            foreach (var xor in XorIndicators)
            {
                if (line.Contains(xor, StringComparison.OrdinalIgnoreCase))
                    findings.Add($"XOR indicator: {xor}");
            }

            // 5. Advanced XOR detection
            if (Regex.IsMatch(line, @"\b\w+\s*\^\s*0x[0-9A-Fa-f]{1,2}\b"))
                findings.Add("XOR operation: value ^ hex constant");

            if (Regex.IsMatch(line, @"\b\w+\s*\^\s*\w+\b"))
                findings.Add("XOR operation: variable ^ variable");

            if (Regex.IsMatch(line, @"\bfor\s*\(.*\)\s*.*\^="))
                findings.Add("XOR loop detected (for)");

            if (Regex.IsMatch(line, @"\bwhile\s*\(.*\)\s*.*\^="))
                findings.Add("XOR loop detected (while)");

            if (Regex.IsMatch(line, @"xor\s*(key|decrypt|decode|payload)", RegexOptions.IgnoreCase))
                findings.Add("XOR keyword: decrypt/decode/payload");

            if (Regex.IsMatch(line, @"0x[0-9A-Fa-f]{2}"))
                findings.Add("Hex constant detected (possible XOR key)");

            if (Regex.IsMatch(line, @"[A-Za-z0-9+/]{12,}={0,2}.*\^"))
                findings.Add("Base64 + XOR hybrid obfuscation");

            return findings;
        }
    }
}
