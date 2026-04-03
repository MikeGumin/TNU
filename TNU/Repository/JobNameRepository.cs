using DynamicData;
using System.Collections.ObjectModel;
using TNU.Models.Enum;
using TNU.Services;

namespace TNU.Repository
{
    /// <summary>
    /// Репозиторий для наименований возможных работ
    /// </summary>
    static internal class JobNameRepository
    {
        /// <summary>
        /// Лист с перечислениями возможных работ
        /// </summary>
        static public ObservableCollection<JobTitleEnum> JobNameList { get;private set; } = [];

        static JobNameRepository()
        {
            foreach (string str in ReadCsvFile.Read())
            {
                AddJob(new JobTitleEnum(str));
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
