using System.Collections.ObjectModel;
using TNU.Models;

namespace TNU.Services
{
    internal class JobNameRepository
    {
        public ObservableCollection<JobTitleEnum> JobNameList { get;private set; } = [];

        public void AddJob(JobTitleEnum newJob)
        {
            JobNameList.Add(newJob);
        }
    }
}
