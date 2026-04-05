using System.Collections.ObjectModel;
using TNU.Core.Models;

namespace TNU.Core.Repository
{
    static public class FinishedEntriesRepository
    {
        /// <summary>
        /// Коллекция для хранения завершенных записей
        /// </summary>
        public static ObservableCollection<JobEntry> FinishedEntries { get; set; } = new();

    }
}
