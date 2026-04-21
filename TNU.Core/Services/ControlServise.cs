using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System.IO;
using System.Threading.Tasks;
using TNU.Core.Models;
using TNU.Core.Models.Enum;
using TNU.Core.Services.CsvFile;
using TNU.Core.Services.FinishedEntry;

namespace TNU.Core.Services
{
    public partial class JobControlServise : ReactiveObject
    {
        private readonly IFinishedEntryService _finishedEntryService;

        public Observation observation { get;set; }

        public TimerControlService timerControl { get;set; }

        /// <summary>
        /// Поле определяющее видимость комментария
        /// </summary>
        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }


        public JobControlServise(Observation observation, IFinishedEntryService finishedEntryService)
        {
            _finishedEntryService = finishedEntryService;
            this.observation = observation;
        }

        public async Task AddNewTask(string str ="")
        {
            JobEntryClock model = observation.AddToActivListR(str);

            File.AppendAllLines(SystemStatic.EntryFilePath, new[] { model.Entry.Id.ToString() });

            GeneralUpdateTimer.AddEvent(model);

            if (!GeneralUpdateTimer.IsEnabled)
            {
                SystemStatic.GeneralStopwatch.Start();
                GeneralUpdateTimer.StartTimer();
            }
        }

        /// <summary>
        /// Метод для смены видимости комментария у записи
        /// </summary>
        [RelayCommand]
        public void ChangeCommentVisibility()
        {
            IsVisible = !IsVisible;
        }

        public void SaveJob(JobEntryClock item)
        {
            item.Entry.JobSample = item.Timer.StrTimer;
            item.Entry.RecordStatus = RecordStatusEnum.Finish;


            //_finishedEntryService.SaveEntry(new List<JobEntry>() { item.Entry });

            // Поменять потом в классе --------------------------------
            Dispatcher.UIThread.Post(() =>
            {
                observation.FinishedEntries.Add(item);
            });
            // ------------------------------------------

            observation.JobEntriesActiv.Remove(item);

            ReadCsvFile.DeleteEntry(item.Entry.Id.ToString(), SystemStatic.EntryFilePath);

            ReadCsvFile.WriteJobInFile(item.Entry, SystemStatic.EntryFilePath);
        }

    }
}
