using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Diagnostics;

namespace TNU.Models
{

    /// <summary>
    /// Перечеслиение статусов записи
    /// </summary>
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
        /// <summary>
        /// Переменная для отсчета времени
        /// </summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();
        /// <summary>
        /// Переменная для обновления отображаемого времени
        /// </summary>
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };

        
        private string jobTimer = "";
        /// <summary>
        /// Таймер для отображения
        /// </summary>
        public string JobTimer
        {
            get => jobTimer;
            private set => this.RaiseAndSetIfChanged(ref jobTimer, value);
        }

        /// <summary>
        /// Статус записи
        /// </summary>
        public RecordStatusEnum RecordStatus { get; private set; }
        /// <summary>
        /// Работник для которого делаеться запись
        /// </summary>
        public Worker JobWorker { get; set; }
        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTimeOffset JobDate { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// Наименование выполняемой работы
        /// </summary>
        public string JobName { get; set; }
        
        /// <summary>
        /// Время выполнения задачи
        /// </summary>
        public string JobSample { get; set; }
        
        /// <summary>
        /// Описание выполняемой работы
        /// </summary>
        public string JobDescription { get; set; }

        public JobEntry()
        {
            //Определение работки для которого ведется заметка
            JobWorker = new Worker() { FullName = "test" };

            // Добавления метрода для тиков _timer
            _timer.Tick += (_, __) =>
            {
                TimeSpan elapsed = _stopwatch.Elapsed;
                JobTimer = $"{elapsed.Minutes:00}:{elapsed.Seconds:00}:{elapsed.Milliseconds:000}";

            };

            // Запуск таймера
            StartTimer();
        }

        /// <summary>
        /// Метод запуска таймера
        /// </summary>
        public void StartTimer()
        {
            _stopwatch.Start();
            _timer.Start();

            RecordStatus = RecordStatusEnum.Start;
        }

        /// <summary>
        /// Метод остановки таймера
        /// </summary>
        [RelayCommand]
        public void StopTimer()
        {
            _timer.Stop();
            _stopwatch.Stop();

            RecordStatus = RecordStatusEnum.Stop;
        }
        
        /// <summary>
        /// Метод завершения записи
        /// </summary>
        [RelayCommand]
        public void EndTimer()
        {
            JobSample = _stopwatch.Elapsed.ToString().Split(".")[0];
            RecordStatus = RecordStatusEnum.Finish;
        }
    }
}
