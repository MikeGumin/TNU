using System;
using System.Linq;

namespace TNU.Models;

/// <summary>
/// Модель записи работы
/// </summary>
public partial class JobEntry
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

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
    /// Время начала задачи
    /// </summary>
    public string StartTime { get; set; } = DateTime.Now.ToString("HH:mm:ss");

    /// <summary>
    /// Время конца задачи
    /// </summary>
    public string EndTime
    {
        get
        {
            if (JobSample != null)
            {
                int[] a = (JobSample.Split(':').Select(s => int.Parse(s))).ToArray();
                TimeSpan duration = new TimeSpan(0, a[0], a[1]);
                TimeSpan strtTime = TimeSpan.Parse(StartTime);

                return (strtTime+ duration).ToString();
            }

            return "";
        }
    }

    /// <summary>
    /// Описание выполняемой работы
    /// </summary>
    public string JobDescription { get; set; } = string.Empty;

    /// <summary>
    /// Статус записи
    /// </summary>
    public RecordStatusEnum RecordStatus { get; set; }
}