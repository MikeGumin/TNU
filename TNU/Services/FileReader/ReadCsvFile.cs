using DocumentFormat.OpenXml.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Tmds.DBus.Protocol;
using TNU.Repository;
using TNU.Views;

namespace TNU.Services
{
    static public class ReadCsvFile
    {
        static public string FilePath { get; private set; } = SystemConst.JobNameFilePath;
        static public List<string> Read()
        {
            List<string> result = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
                    result.AddRange(line.Split(','));
                }

            }
            catch(System.Exception ex)
            {
            }
            return result;
        }
        static public void Write(ICollection s)
        {
            StringBuilder csvContent = new StringBuilder();

            foreach (var line in s)
            {
                csvContent.AppendLine(s.ToString());
            }

            File.WriteAllText(FilePath, csvContent.ToString(), Encoding.UTF8);
        }
    }
}
