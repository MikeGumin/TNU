
namespace TNU.Core
{
    static public class SystemConst
    {
        public const string JobNameFilePath = "ActivityList.csv";
        public const string StartingPreparationsFilePath = "StartingPreparations.csv";

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
