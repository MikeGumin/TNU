using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models.Enum;
using TNU.Core.Services.ClockAction;

namespace TNU.Core.Models
{
    public partial class JobEntryClock
    {
        public JobEntry Entry { get; set; } = new JobEntry();
        public ClockActionService Timer { get; set; } = new ClockActionService();

        //public void StartTimer()
        //{
        //    Timer.StartTimer();
        //    Entry.RecordStatus = RecordStatusEnum.Start;
        //}


        //[RelayCommand]
        //public void StopTimer()
        //{
        //    Timer.StopTimer();
        //    Entry.RecordStatus = RecordStatusEnum.Stop;
        //}

        //public void ChangeTimerStatus()
        //{
        //    if (Entry.RecordStatus == RecordStatusEnum.Start)
        //        StopTimer();
        //    else
        //        StartTimer();
        //}

        //public void EndTimer()
        //{
        //    if (Entry.RecordStatus != RecordStatusEnum.Stop)
        //        StopTimer();
        //}
    }
}
