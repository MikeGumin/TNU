using TNU.Models.Enum;

namespace TNU.Models;

/// <summary>
/// Модель респондента
/// </summary>
public class Respondent
{
    /// <summary>
    /// Номер респондента 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ФИО респондента
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Должность респондента
    /// </summary>
    JobPositionEnum JobPosition { get; set; }
}
