using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
namespace SVNExtension
{
    public class SVNManager : IDisposable
    {

        public List<string> Files { get; private set; }
        private string output = "SVNReports";

        public SVNManager()
        {
            Files = new List<string>();
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }
        }

        public void Generate(string url, int startRevision)
        {
            var fileName = GetFileName(url);
            var cmd = "svn";
            var args = string.Format("log --xml -r {0}:HEAD {1} -v", startRevision, url);
            var content = StartProcess(cmd, args);
            var filePath = Path.Combine(output, string.Format("{0}_{1}_.xml", fileName, DateTime.Now.ToString("yyyyMMdd-hhmmss")));
            File.WriteAllText(filePath, content);
            Files.Add(filePath);
        }

        private string GetFileName(string url)
        {
            var result = string.Empty;
            var pattern = @"(?:.*)(?:[/\\])(?<repos>.*)$";
            var matches = Regex.Matches(url, pattern);
            foreach (Match match in matches)
            {
                if(match.Groups.Count > 0)
                {
                    result = match.Groups["repos"].Value;
                }
            }

            if (result.Equals(string.Empty))
            {
                throw new ArgumentException("Invalid url : " + url);
            }

            return result;
        }

        private string StartProcess(string cmd, string args)
        {
            string result = null;
            using (Process process = new Process())
            {
                Process proc = new Process();
                proc.StartInfo.FileName = cmd;
                proc.StartInfo.Arguments = args;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                StreamReader sr = proc.StandardOutput;
                result = sr.ReadToEnd();
                proc.WaitForExit();
            }
            return result;
        }

        public void Dispose()
        {
            Directory.Delete(output, true);
        }


        /*C:\Users\leonardo.kobus\Desktop>svn log --xml "file:///c:/users/leonardo.kobus/d
esktop/games/Gamification/SVNExtension.UnitTest/bin/Debug/RepositorioNET" -v > o
ut.txt
         * */
    }
}
