using System;

namespace TNU.Models;

/// <summary>
/// Модель записи работы
/// </summary>
public partial class JobEntry
{
    /// <summary>
    /// Работник для которого делаеться запись
    /// </summary>
    public Worker JobWorker { get; set; }

    /// <summary>
    /// Дата записи
    /// </summary>
    public DateTimeOffset JobDate { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Наименование выполняемой работы
    /// </summary>
    public string JobName { get; set; }

    /// <summary>
    /// Время выполнения задачи
    /// </summary>
    public string JobSample { get; set; }

    /// <summary>
    /// Описание выполняемой работы
    /// </summary>
    public string JobDescription { get; set; } = string.Empty;

    /// <summary>
    /// Статус записи
    /// </summary>
    public RecordStatusEnum RecordStatus { get; set; }
}