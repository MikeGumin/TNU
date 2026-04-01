using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Diagnostics;

namespace TNU.Models
{
    public class Clock : ReactiveObject
    {
        /// <summary>
        /// Переменная для отсчета времени (таймер)
        /// </summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Таймер для отображения
        /// </summary>
        private string _strTimer = "00";
        public string StrTimer
        {
            get => _strTimer;
            private set => this.RaiseAndSetIfChanged(ref _strTimer, value);
        }

        public Clock()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            _stopwatch.Start();
        }

        public void StopTimer()
        {
            _stopwatch.Stop();
        }

        public void ReDrowTimer(object? sender, EventArgs e) 
        {
            TimeSpan elapsed = _stopwatch.Elapsed;
            StrTimer = $"{elapsed.Minutes:00}:{elapsed.Seconds:00}:{elapsed.Milliseconds:000}";
        }
    }
}
