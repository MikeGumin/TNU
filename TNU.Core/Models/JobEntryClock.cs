using CommunityToolkit.Mvvm.Input;
using TNU.Core.Services.ClockAction;

namespace TNU.Core.Models
{
    public partial class JobEntryClock : NotifyChangedModel
    {

        public bool IsSavePrepareJob { get; set; }
        public string BtnText { get; private set; } = "Stop";

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged();
                //this.RaiseAndSetIfChanged(ref _isVisible, value);
            }
        }

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

        [RelayCommand]
        public void CommentVisibility()
        {
            IsVisible = !IsVisible;
        }
    }
}
