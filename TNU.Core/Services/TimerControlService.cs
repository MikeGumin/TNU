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


        public void StartTimer()
        {
            jobTimer.Timer.StartTimer();
            jobTimer.Entry.RecordStatus = RecordStatusEnum.Start;
        }

        [RelayCommand]
        public void StopTimer()
        {
            jobTimer.Timer.StopTimer();
            jobTimer.Entry.RecordStatus = RecordStatusEnum.Stop;
        }

        public void ChangeTimer()
        {
            if (jobTimer.Entry.RecordStatus == RecordStatusEnum.Start)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        [RelayCommand]
        public void EndTimer()
        {
            if (jobTimer.Entry.RecordStatus != RecordStatusEnum.Stop)
                StopTimer();
        }

    }
}
