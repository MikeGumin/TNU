using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TNU.Core.Services;

namespace TNU.Core.Models
{
    /// <summary>
    /// Модель Наблюдения
    /// </summary>
    public class Observation : NotifyChangedModel
    {
        /// <summary>
        /// Инспектор который делает запись
        /// </summary>
        public string InspectorName { get; set; }

        /// <summary>
        /// Наблюдаемый за кем ведется наблюдение 
        /// </summary>
        public string? RespondentId { get; set; }

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
        public ObservableCollection<JobEntryClock> JobEntriesActiv { get; private set; } = [];

        public void AddToActivList(string jobName="") 
        {
            JobEntriesActiv.Add(CreateJobEntryServise.CreateJobEntry(jobName));
        }
        public JobEntryClock AddToActivListR(string jobName="") 
        {
            JobEntryClock jobEntryClock = CreateJobEntryServise.CreateJobEntry(jobName);
            JobEntriesActiv.Insert(0, jobEntryClock);
            return jobEntryClock;
        }

        private ObservableCollection<JobEntryClock> finishedEntries = new();

        /// <summary>
        /// Коллекция для хранения завершенных записей
        /// </summary>
        public ObservableCollection<JobEntryClock> FinishedEntries { get=> finishedEntries; 
            set 
            {
                finishedEntries = value;
                OnPropertyChanged();
            }
        } 

        


    }
}
