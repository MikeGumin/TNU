using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                File.AppendAllText(FilePath, Environment.NewLine + jobName, Encoding.UTF8);
            }
        }

        public static void DeleteEntry(string jobName)
        {
            var newEntryList = File.ReadAllLines(FilePath)
                .Where(line => line != jobName);
            
            File.WriteAllLines(FilePath, newEntryList);
        }
    }
}
