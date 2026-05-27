using Avalonia.Threading;
using System;
using TNU.Core.Models;

namespace TNU.Core.Services
{
    delegate void ReDrowTimerStr(object? sender, EventArgs e);

    /// <summary>
    /// Общий таймер для изменения отображения времени у часов
    /// </summary>
    static internal class GeneralUpdateTimer
    {
        /// <summary>
        /// Переменная для обновления отображаемого времени
        /// </summary>
        static private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };

        static public bool IsEnabled { get => _timer.IsEnabled; }

        static private ReDrowTimerStr dl;

        static GeneralUpdateTimer()
        {
            _timer.Tick += (_, __) =>
            {
                dl?.Invoke(_, __);
            };

        }

        static public void StartTimer()
        {
            _timer.Start();
        }

        static public void StopTimer()
        {
            _timer.Stop();
        }

        static public void AddEvent(JobEntryClock model)
        {
            dl += model.Timer.ReDrowTimer;
        }
        static public void RemoveEvent(JobEntryClock model)
        {
            try
            {
                dl -= model.Timer.ReDrowTimer;
            }
            catch { }
        }
    }
}
