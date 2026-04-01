using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNU.Models;
using TNU.Models.Enum;

namespace TNU.Repository
{
    static public class FinishedEntriesRepository
    {
        /// <summary>
        /// Коллекция для хранения завершенных записей
        /// </summary>
        public static ObservableCollection<JobEntry> FinishedEntries { get; set; } = new();

    }
}
