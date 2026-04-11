using System.Diagnostics;

namespace TNU.Core
{
    static public class SystemConst
    {
        public const string JobNameFilePath = "ActivityList.csv";

        static public Stopwatch GeneralStopwatch { get; set; } = new Stopwatch();

        /// <summary>
        /// Индекс верхней строки дата
        /// </summary>
        public const int HeaderRow = 1;
        
        /// <summary>
        /// Индекс столбца с наименованием работ
        /// </summary>
        public const int JobNameColumn = 1;
        
        /// <summary>
        /// Индекс старта отсчета времени 
        /// </summary>
        public const int TimelineStartColumn = 2;
    }
}
