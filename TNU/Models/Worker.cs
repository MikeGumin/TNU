using TNU.Models.Enum;

namespace TNU.Models;

/// <summary>
/// Модель работника
/// </summary>
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
