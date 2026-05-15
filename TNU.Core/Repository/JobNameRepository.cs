using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TNU.Core.Models.Enum;
using TNU.Core.Services.CsvFile;

namespace TNU.Core.Repository
{
    /// <summary>
    /// Репозиторий для наименований возможных работ
    /// </summary>
    internal static class JobNameRepository
    {
        /// <summary>
        /// Лист с перечислениями возможных работ
        /// </summary>
        public static ObservableCollection<JobTitleEnum> JobNameList { get;private set; } = [];
        public static Dictionary<string, string> JobNameCodeList { get;private set; } = [];

        public static void FillJobNameList()
        {
            foreach (var job in ReadCsvFile.Read())
            {
                AddJob(new JobTitleEnum(job[0])); // добавляем наименование в список из файла
                if (!string.IsNullOrWhiteSpace(job[1]))
                {
                    JobNameCodeList[job[0]] = job[1]; // добавляем наименование в
                }
            }
        }

        /// <summary>
        /// Метод добавления новых работ
        /// </summary>
        /// <param name="newJob"></param>
        static public void AddJob(JobTitleEnum newJob)
        {
            JobNameList.Add(newJob);
        }
    }
}
