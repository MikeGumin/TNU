namespace TNU.Models
{
    public class Worker
    {
        /// <summary>
        /// ФИО работника
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Должность работника
        /// </summary>
        JobPositionEnum JobPosition { get; set; }
    }

    /// <summary>
    /// Перечесление Должностей для работника
    /// </summary>
    public enum JobPositionEnum 
    {
        Defolt,
    }
}
