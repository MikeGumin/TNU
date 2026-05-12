using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using TNU.Core.Models.Enum;
using TNU.Core.Repository;

namespace TNU.Core.Models;

/// <summary>
/// Модель записи работы
/// </summary>
public partial class JobEntry : NotifyChangedModel
{

    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Работник для которого делаеться запись
    /// </summary>
    public Respondent JobWorker { get; set; }

    /// <summary>
    /// Дата записи
    /// </summary>
    public DateTime JobDate { get; set; } = DateTime.Now;

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
            
            AutoFillJobCode();
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

            int[] a = (JobSample.Split(':').Select(s => int.Parse(s))).ToArray();
            TimeSpan duration = new TimeSpan(a[0], a[1], a[2]);
            TimeSpan strtTimer = TimeSpan.Parse(StartTime);
            endTime = (strtTimer + duration).ToString();

            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Время начала задачи
    /// </summary>
    private string startTime = SystemStatic.GeneralStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
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
    public string Description { get; set; } = "";
    /// <summary>

    /// Код работы
    /// </summary>
    private string _jobCode;
    public string JobCode
    {
        get => _jobCode;
        set
        {
            _jobCode = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Статус записи
    /// </summary>
    public RecordStatusEnum RecordStatus { get; set; }
    
    /// <summary>
    /// ЧекБок указывающий, нужно ли уточнить время задачи 
    /// </summary>
    public bool IsTimedCorrectly { get; set; } = false;

    /// <summary>
    /// Коэффициент сложности
    /// </summary>
    public double DifficultyFactor { get; set; } = 1;

    public OperationResult ChangeStartTime(string value)
    {
        try
        {
            TimeSpan strtTimer = TimeSpan.Parse(value);
            TimeSpan endTimer = TimeSpan.Parse(EndTime);

            JobSample = (endTimer - strtTimer).ToString();

            StartTime = value;

            return OperationResult.Ok();
        }
        catch (Exception e)
        {
            return OperationResult.Fail($"Ошибка перевода времени старта, некоректное значение - {value}");
        }
    }

    public OperationResult ChangeEndTime(string value)
    {
        try
        {
            TimeSpan startTimer = TimeSpan.Parse(StartTime);
            TimeSpan endTimer = TimeSpan.Parse(value);

            //this.RaiseAndSetIfChanged(ref jobSample, (endTimer - startTimer).ToString());

            JobSample = (endTimer - startTimer).ToString();

            EndTime = value;

            return OperationResult.Ok();
        }
        catch (Exception e)
        {
            return OperationResult.Fail($"Ошибка перевода времени окончания, некоректное значение - {value}");
        }
    }
    
    private void AutoFillJobCode()
    {
        if (!string.IsNullOrWhiteSpace(JobName) && 
            string.IsNullOrWhiteSpace(JobCode))
        {
            if (JobNameRepository.JobNameCodeList.TryGetValue(JobName, out var code) && 
                !string.IsNullOrWhiteSpace(code))
            {
                JobCode = code;
            }
        }
    }
}