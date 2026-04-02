using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNU.Models.Enum;

namespace TNU.Models
{
    /// <summary>
    /// Модель наблюдателя
    /// </summary>
    internal class Inspector
    {
        /// <summary>
        /// Номер Наблюдателя 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО Наблюдателя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Должность Наблюдателя
        /// </summary>
        JobPositionEnum JobPosition { get; set; }
    }
}
