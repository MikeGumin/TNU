using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using TNU.Core.Models.Enum;
using TNU.Core.Services.ClockAction;

namespace TNU.Core.Models
{
    public partial class JobEntryClock : NotifyChangedModel
    {

        public bool IsSavePrepareJob { get; set; }
        public string BtnText { get; private set; } = "Stop";
        public JobEntry Entry { get; set; } = new JobEntry();
        public ClockActionService Timer { get; set; } = new ClockActionService();

        public void ChangeBtnText()
        {
            if (BtnText == "Stop")
                BtnText = "Start";
            else
                BtnText = "Stop";

            OnPropertyChanged("BtnText");
        }
    }
}
