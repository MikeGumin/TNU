using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNU.Models
{
    /// <summary>
    /// Модель Наблюдения
    /// </summary>
    internal class Observation
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
        public string City { get; set; }

        /// <summary>
        /// Предприятие в котором проводиться наблюдение
        /// </summary>
        public string Enterprise { get; set; }


        ObservableCollection<JobEntry> JobEntries { get; set; }
    }
}
