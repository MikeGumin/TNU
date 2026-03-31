using System.Collections.Generic;

namespace TNU.Models
{
    /// <summary>
    /// Класс для создания экземпляров возможных работ
    /// </summary>
    internal class JobTitleEnum
    {
        /// <summary>
        /// Наименование работы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Для каких должностей данная работа будет использоваться
        /// </summary>
        public List<JobPositionEnum> Enums { get; set; } = new List<JobPositionEnum>() { JobPositionEnum.Defolt };

        public JobTitleEnum(string jobName) 
        {
            Name = jobName;
        }
    }
}
