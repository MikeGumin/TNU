using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace TNU.Core.Services.FileOpener
{
    public class FileOpenerServise
    {
        public void OpenFile()
        {
            // длаем из относительного пути полный 
            string relativePath = SystemConst.JobNameFilePath;
            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

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
