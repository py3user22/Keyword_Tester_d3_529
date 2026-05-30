using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeScannerApp
{
    public static class KeywordScanner
    {
        public static List<string> ScanLine(string line)
        {
            List<string> findings = new();

            // 1. Keyword matches
            foreach (var keyword in GlobalKeywords.MaliciousKeywords)
            {
                if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    findings.Add($"Keyword match: {keyword}");
            }

            // 2. Suspicious extensions
            foreach (var ext in GlobalKeywords.SuspiciousExtensions)
            {
                if (line.Contains(ext, StringComparison.OrdinalIgnoreCase))
                    findings.Add($"Suspicious extension: {ext}");
            }

            // 3. Regex patterns
            foreach (var pattern in GlobalKeywords.MaliciousPatterns)
            {
                if (Regex.IsMatch(line, pattern))
                    findings.Add($"Pattern match: {pattern}");
            }

            return findings;
        }
    }
}
