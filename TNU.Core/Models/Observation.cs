using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TNU.Core.Models
{
    /// <summary>
    /// Модель Наблюдения
    /// </summary>
    public class Observation : INotifyPropertyChanged
    {
        /// <summary>
        /// Инспектор который делает запись
        /// </summary>
        public string InspectorName { get; set; }

        /// <summary>
        /// Наблюдаемый за кем ведется наблюдение 
        /// </summary>
        public int RespondentId { get; set; }

        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTimeOffset JobDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Город в котором проводиться наблюдение
        /// </summary>
        public string City { get; set; } = "";

        /// <summary>
        /// События которые входят в наблюдения
        /// </summary>
        ObservableCollection<JobEntryClock> JobEntries { get; set; }

        private ObservableCollection<JobEntry> finishedEntries = new();
        /// <summary>
        /// Коллекция для хранения завершенных записей
        /// </summary>
        public ObservableCollection<JobEntry> FinishedEntries { get=> finishedEntries; 
            set 
            {
                finishedEntries = value;
                OnPropertyChanged();
            }
        } 

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsCompleted() 
        {
            return City != "" && RespondentId != null && InspectorName != null;
        }
    }
}
