using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
namespace SVNExtension
{
    public class SVNManager
    {

        public SVNManager()
        {

        }

        public SVNRepository Generate(string url)
        {
            
            var fileName = GetFileName(url);
            var cmd = "svn";
            var args = string.Format("log --xml {0} -v > {1}.xml", url, fileName);
            StartProcess(cmd, args);
            //var repos = new SVNRepository(url);
            return null;
        }

        private string GetFileName(string url)
        {
            var result = string.Empty;
            var pattern = @"(?:.*)(?:/)(?<repos>.*)$";
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

        private void StartProcess(string cmd, string args)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = cmd;
                process.StartInfo.Arguments = args;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
        }


        /*C:\Users\leonardo.kobus\Desktop>svn log --xml "file:///c:/users/leonardo.kobus/d
esktop/games/Gamification/SVNExtension.UnitTest/bin/Debug/RepositorioNET" -v > o
ut.txt
         * */
    }
}
