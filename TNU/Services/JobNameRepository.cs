using System.Collections.ObjectModel;
using TNU.Models;

namespace TNU.Services
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
            AddJob(new JobTitleEnum("Тестовое задание"));
            AddJob(new JobTitleEnum("Совершенно другое дело"));
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
