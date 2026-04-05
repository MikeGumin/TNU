using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TNU.Models;

/// <summary>
/// Модель записи работы
/// </summary>
public partial class JobEntry : INotifyPropertyChanged
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
    private string jobName;
    public string JobName
    {
        get => jobName;
        set
        {
            jobName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Время выполнения задачи
    /// </summary>
    private string jobSample;
    public string JobSample
    {
        get => jobSample;
        set
        {
            jobSample = value;

            int[] timePath = (JobSample.Split(':').Select(s => int.Parse(s))).ToArray();

            TimeSpan duration = new TimeSpan(timePath[0], timePath[1], timePath[2]);
            TimeSpan strtTimer = TimeSpan.Parse(StartTime);

            endTime = (strtTimer + duration).ToString();

            OnPropertyChanged();
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
            startTime = value;
            OnPropertyChanged();
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
            endTime = value;
            OnPropertyChanged();
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
        StartTime = value;

        TimeSpan strtTimer = TimeSpan.Parse(StartTime);
        TimeSpan endTimer = TimeSpan.Parse(EndTime);

        JobSample = (endTimer - strtTimer).ToString();
    }

    public void ChangeEndTime(string value)
    {
        EndTime = value;

        TimeSpan startTimer = TimeSpan.Parse(StartTime);
        TimeSpan endTimer = TimeSpan.Parse(EndTime);

        //this.RaiseAndSetIfChanged(ref jobSample, (endTimer - startTimer).ToString());

        JobSample = (endTimer - startTimer).ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}