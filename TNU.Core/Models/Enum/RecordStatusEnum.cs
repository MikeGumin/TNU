namespace TNU.Core.Models.Enum;

/// <summary>
/// Перечеслиение статусов таймера задачи
/// </summary>
public enum RecordStatusEnum
{
    /// <summary>
    /// Таймер у задачи идет
    /// </summary>
    Start,
    
    /// <summary>
    /// Таймер задачи остановлен
    /// </summary>
    Stop,
    
    /// <summary>
    /// Таймер задачи завершен
    /// </summary>
    Finish
}