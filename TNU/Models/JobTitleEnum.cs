using System.Collections.Generic;

namespace TNU.Models
{
    /// <summary>
    /// Класс для создания экземпляров возможных работ
    /// </summary>
    public class JobTitleEnum
    {
        /// <summary>
        /// Наименование работы
        /// </summary>
        public string Name { get; set; }
        
        public override string ToString() => Name;

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
