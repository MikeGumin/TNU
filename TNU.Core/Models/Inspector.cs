using TNU.Core.Models.Enum;

namespace TNU.Core.Models
{
    /// <summary>
    /// Модель наблюдателя
    /// </summary>
    public class Inspector
    {
        /// <summary>
        /// Номер Наблюдателя 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО Наблюдателя
        /// </summary>
        public string FullName { get; set; } = "";

        /// <summary>
        /// Должность Наблюдателя
        /// </summary>
        JobPositionEnum JobPosition { get; set; }
    }
}
