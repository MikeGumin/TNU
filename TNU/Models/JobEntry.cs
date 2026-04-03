using Avalonia;
using ReactiveUI;
using System;
using System.Linq;

namespace TNU.Models;

/// <summary>
/// Модель записи работы
/// </summary>
public partial class JobEntry : ReactiveObject
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Работник для которого делаеться запись
    /// </summary>
    public Respondent JobWorker { get; set; }

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
    private string jobSample;
    public string JobSample
    {
        get => jobSample;
        set
        {
            this.RaiseAndSetIfChanged(ref jobSample, value);

            int[] a = (JobSample.Split(':').Select(s => int.Parse(s))).ToArray();
            TimeSpan duration = new TimeSpan(a[0], a[1], a[2]);
            TimeSpan strtTimer = TimeSpan.Parse(StartTime);
            endTime = (strtTimer + duration).ToString();
        }
    }

    /// <summary>
    /// Время начала задачи
    /// </summary>
    private string startTime = DateTime.Now.ToString("HH:mm:ss");
    public string StartTime
    {
        get => startTime;
        set
        {
            
        }
    }

    /// <summary>
    /// Время конца задачи
    /// </summary>
    private string endTime;
    public string EndTime
    {
        get => endTime;
        set
        {
           
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

    /// <summary>
    /// Коэффициент сложности
    /// </summary>
    public double DifficultyFactor { get; set; } = 1;

    public void ChangeStartTime(string value)
    {
        this.RaiseAndSetIfChanged(ref startTime, value);

        TimeSpan strtTimer = TimeSpan.Parse(StartTime);
        TimeSpan endTimer = TimeSpan.Parse(EndTime);

        JobSample = (endTimer - strtTimer).ToString();
    }

    public void ChangeEndTime(string value)
    {
        this.RaiseAndSetIfChanged(ref endTime, value);

        TimeSpan strtTimer = TimeSpan.Parse(StartTime);
        TimeSpan endTimer = TimeSpan.Parse(EndTime);

        this.RaiseAndSetIfChanged(ref jobSample, (endTimer - strtTimer).ToString());

        JobSample = (endTimer - strtTimer).ToString();
    }
}