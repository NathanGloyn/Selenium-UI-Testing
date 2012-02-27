using System;
using System.Diagnostics;
using System.IO;

namespace Selenium.Tests
{
    public class Support
    {
        const int iisPort = 1392;
        private string _applicationName;
        private Process _iisProcess;

        public void StartIIS(string applicationName)
        {
            _applicationName = applicationName;

            var running = Process.GetProcessesByName("iisexpress",".");
            if (running.Length == 0)
            {
                var applicationPath = GetApplicationPath(_applicationName);
                var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

                _iisProcess = new Process();
                _iisProcess.StartInfo.CreateNoWindow = true;
                _iisProcess.StartInfo.FileName = programFiles + "\\IIS Express\\iisexpress.exe";
                _iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, iisPort);
                _iisProcess.Start();
            }
            else
            {
                _iisProcess = running[0];
            }
        }

        public void StopIIS()
        {
            if (_iisProcess.HasExited == false)
            {
                _iisProcess.Kill();
            }
             
        }

        protected virtual string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            return Path.Combine(solutionFolder, applicationName);
        }

    }
}