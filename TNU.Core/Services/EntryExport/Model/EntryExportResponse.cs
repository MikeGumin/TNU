using System;
using CsvHelper.Configuration.Attributes;

namespace TNU.Core.Services.EntryExport.Model;

/// <summary>
/// Модель экспорта записей
/// </summary>
public class EntryExportResponse
{
    ///// <summary>
    ///// ФИО сотрудника
    ///// </summary>
    // [Name("ФИО сотрудника")]
    //public required string FullName { get; set; }
    
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    // [ExcelColumnName("Род деятельности")]
    [Name("Id")]
    public required int Id { get; set; }
    
    /// <summary>
    /// Род деятельности
    /// </summary>
    // [ExcelColumnName("Род деятельности")]
    [Name("Что наблюдается")]
    public required string JobTitle { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Name("Текущее время")]
    public required string DuringTime { get; set; }
    
    /// <summary>
    /// Продолжительность мин.
    /// </summary>
    [Name("Замер")]
    public required string JobTime { get; set; }
    
    /// <summary>
    /// Индекс/Код
    /// </summary>
    [Name("Индекс")]
    public required string JobCode { get; set; }
    
    /// <summary>
    /// Уточнить время записи
    /// </summary>
    [Name("Уточнить время")]
    public required string IsCorrectly { get; set; }
    
    /// <summary>
    /// Дата, когда проводилась работа
    /// </summary>
    [Name("Дата работы")]
    public required DateTime JobDate { get; set; }
    
    /// <summary>
    /// Дата, когда проводилась работа
    /// </summary>
    [Name("Время начала")]
    public required string JobDateStart { get; set; }
    
    /// <summary>
    /// Дата, когда проводилась работа
    /// </summary>
    [Name("Время окончания")]
    public required string JobDateEnd { get; set; }

    /// <summary>
    /// Дата, когда проводилась работа
    /// </summary>
    [Name("Дата проведения работы")]
    public string JobDate { get; set; }

    [Name("Дата проведения работы")]
    public string JobDateTime { get; set; }
}