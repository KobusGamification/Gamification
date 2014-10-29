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
        static log4net.ILog log = log4net.LogManager.GetLogger(typeof(SVNManager));
        public SVNManager()
        {
            Files = new List<string>();
            if (!Directory.Exists(output))
            {
                log.DebugFormat("Creating directory {0}", output);
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
            log.DebugFormat("Writing all content to : {0}", filePath);
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
                log.Info("Starting svn process");
                proc.Start();
                StreamReader sr = proc.StandardOutput;
                result = sr.ReadToEnd();
                log.Info("Reading output and waiting for exit.");
                proc.WaitForExit();
            }
            return result;
        }

        public void Dispose()
        {
            Directory.Delete(output, true);
        }
    }
}
