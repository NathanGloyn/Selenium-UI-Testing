using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using EnvDTE100;

namespace Selenium.Tests.Support
{
    public class IISExpress
    {
        private System.Diagnostics.Process iisProcess;

        public void Start(string applicationName, int iisPort)
        {
            var running = System.Diagnostics.Process.GetProcessesByName("iisexpress", ".");
            if (running.Length == 0)
            {
                var applicationPath = GetApplicationPath(applicationName);
                var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

                iisProcess = new System.Diagnostics.Process();
                iisProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                iisProcess.StartInfo.FileName = programFiles + "\\IIS Express\\iisexpress.exe";
                iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, iisPort);
                iisProcess.Start();
            }
            else
            {
                iisProcess = running[0];
            }
        }

        public void Stop()
        {
            if (iisProcess.HasExited == false)
            {
                iisProcess.Kill();
            }
             
        }

        /// <summary>
        /// Intention is to find the solutin folder, unfortunatley no easy
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        protected virtual string GetApplicationPath(string applicationName)
        {
            EnvDTE80.DTE2 dte2 = (EnvDTE80.DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.11.0");

            return Path.Combine(Path.GetDirectoryName(dte2.Solution.FullName), applicationName);
        }

    }
}