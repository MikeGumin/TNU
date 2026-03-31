using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Concurrency;

namespace TNU.Models
{

    public enum RecordStatusEnum
    {
        Start,
        Stop,
        Finish
    }

    /// <summary>
    /// Модель записи работы
    /// </summary>
    public partial class JobEntry : ReactiveObject
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };

        private string jobTimer = "";
        public string JobTimer
        {
            get => jobTimer;
            private set => this.RaiseAndSetIfChanged(ref jobTimer, value);
        }

        public RecordStatusEnum RecordStatus = RecordStatusEnum.Start;
        public Worker JobWorker { get; set; }
        public DateTimeOffset JobDate { get; set; } = DateTimeOffset.Now;
        public string JobName { get; set; }
        public string JobDescription { get; set; }



        public JobEntry()
        {
            JobWorker = new Worker() { FullName = "test" };

            _timer.Tick += (_, __) =>
            {
                TimeSpan elapsed = _stopwatch.Elapsed;
                JobTimer = $"{elapsed.Minutes:00}:{elapsed.Seconds:00}:{elapsed.Milliseconds:000}";

            };

            StartTimer();
        }

        public void StartTimer()
        {
            _stopwatch.Start();
            _timer.Start();
        }

        [RelayCommand]
        public void StopTimer()
        {
            _timer.Stop();
            _stopwatch.Stop();

            RecordStatus = RecordStatusEnum.Stop;
        }
    }
}
