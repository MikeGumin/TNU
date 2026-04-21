using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models;
using TNU.Core.Models.Enum;

namespace TNU.Core.Services
{
    public partial class TimerControlService
    {
        private JobEntryClock jobTimer { get; set; }

        public TimerControlService(JobEntryClock timer)
        {
            this.jobTimer = timer;
        }


        static public void StartTimer(JobEntryClock jobTimer)
        {
            jobTimer.Timer.StartTimer();
            jobTimer.Entry.RecordStatus = RecordStatusEnum.Start;
        }

        static public void StopTimer(JobEntryClock jobTimer)
        {
            jobTimer.Timer.StopTimer();
            jobTimer.Entry.RecordStatus = RecordStatusEnum.Stop;
        }

        static public void ChangeTimer(JobEntryClock jobTimer)
        {
            if (jobTimer.Entry.RecordStatus == RecordStatusEnum.Start)
            {
                StopTimer(jobTimer);
            }
            else
            {
                StartTimer(jobTimer);
            }
        }

        [RelayCommand]
        static public void EndTimer(JobEntryClock jobTimer)
        {
            if (jobTimer.Entry.RecordStatus != RecordStatusEnum.Stop)
                StopTimer(jobTimer);
        }

    }
}
