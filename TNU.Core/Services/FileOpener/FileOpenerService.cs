using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using CsvHelper.Configuration;

namespace TNU.Core.Services.FileOpener
{
    public class FileOpenerService : IFileOpenerService
    {
        public void OpenFile(string filePath)
        {
            // длаем из относительного пути полный 
            string relativePath = filePath;
            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

            if (File.Exists(fullPath))
            {
                ReaderFile(fullPath);
            }
        }
        
        public void OpenFrdFile(string fileName)
        {
            // длаем из относительного пути полный 
            string fullPath = Path.Combine(AppContext.BaseDirectory, fileName);
            
            // Console.WriteLine(fullPath);

            if (File.Exists(fullPath))
            {
                ReaderFile(fullPath);
            }
        }

         public void OpenExploier()
        {
            var filePath = SystemConst.JobNameFilePath;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{filePath}\"") { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string dir = Path.GetDirectoryName(filePath)!;
                Process.Start("xdg-open", dir);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", $"-R \"{filePath}\"");
            }
        }

         private void ReaderFile(string fullPath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = fullPath,
                UseShellExecute = true
            });
        }
    }
}
