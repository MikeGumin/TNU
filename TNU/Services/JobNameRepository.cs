using System.Collections.ObjectModel;
using TNU.Models;

namespace TNU.Services
{
    /// <summary>
    /// Репозиторий для наименований возможных работ
    /// </summary>
    internal class JobNameRepository
    {
        /// <summary>
        /// Лист с перечислениями возможных работ
        /// </summary>
        public ObservableCollection<JobTitleEnum> JobNameList { get;private set; } = [];

        /// <summary>
        /// Метод добавления новых работ
        /// </summary>
        /// <param name="newJob"></param>
        public void AddJob(JobTitleEnum newJob)
        {
            JobNameList.Add(newJob);
        }
    }
}
