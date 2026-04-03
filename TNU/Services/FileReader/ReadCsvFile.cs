using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TNU.Repository;

namespace TNU.Services
{
    static public class ReadCsvFile
    {
        static public string FilePath { get; private set; } = SystemConst.JobNameFilePath;
        static public List<string> Read()
        {
            string[] lines = File.ReadAllLines(FilePath);

            List<string> result = new List<string>();

            foreach (string line in lines)
            {
                result.AddRange(line.Split(','));
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
