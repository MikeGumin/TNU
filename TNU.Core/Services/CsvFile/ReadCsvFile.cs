using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TNU.Core.Models;
using TNU.Core.Models.Enum;
using TNU.Core.Repository;

namespace TNU.Core.Services.CsvFile
{
    static class ReadCsvFile
    {
        private static readonly string FilePath = SystemConst.JobNameFilePath;
        public static List<string[]> Read()
        {
            string[] lines = File.ReadAllLines(FilePath);

            List<string[]> result = new List<string[]>();

            foreach (string line in lines)
            {
                var entry = line.Split(':');
                if (entry.Length != 2)
                {
                    entry = new string[2] { entry[0], string.Empty };
                }
                
                result.AddRange(entry);
            }

            return result;
        }
        public static void Write(string? jobName)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                return;
            }
            
            var jobTitle = new JobTitleEnum(jobName);
            
            if (!JobNameRepository.JobNameList.Contains(jobTitle))
            {
                File.AppendAllText(FilePath, jobName + Environment.NewLine, Encoding.UTF8);
            }
        }
        
        public static void WriteJobInFile(JobEntry entry, string filePath)
        {
            var sb = new StringBuilder();
            
            sb.Append(entry.Id + "; ");
            sb.Append(entry.JobName + "; ");
            sb.Append(entry.JobCode + "; ");
            sb.Append(entry.JobSample);
            
            File.AppendAllText(filePath, Environment.NewLine + sb, Encoding.UTF8);
        }

        public static void DeleteEntry(string jobName, string filePath)
        {
            var newEntryList = File.ReadAllLines(filePath)
                .Where(line => line.Split(":")[0] != jobName && line != string.Empty);
            
            File.WriteAllLines(filePath, newEntryList);
        }
    }
}
