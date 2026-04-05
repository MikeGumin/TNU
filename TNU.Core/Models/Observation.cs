using System;
using System.Collections.ObjectModel;

namespace TNU.Core.Models
{
    /// <summary>
    /// Модель Наблюдения
    /// </summary>
    public class Observation
    {
        /// <summary>
        /// Инспектор который делает запись
        /// </summary>
        public Inspector Inspector { get; set; }

        /// <summary>
        /// Наблюдаемый за кем ведется наблюдение 
        /// </summary>
        public Respondent Worker { get; set; }

        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTimeOffset JobDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Город в котором проводиться наблюдение
        /// </summary>
        public string City { get; set; } = "";

        /// <summary>
        /// Предприятие в котором проводиться наблюдение
        /// </summary>
        public string Enterprise { get; set; }

        /// <summary>
        /// События которые входят в наблюдения
        /// </summary>
        ObservableCollection<JobEntryClock> JobEntries { get; set; }
    }
}
