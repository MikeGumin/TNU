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
    /// Дата, когда проводилась работа
    /// </summary>
    [Name("Дата проведения работы")]
    public DateTimeOffset JobDate { get; set; }
    
    /// <summary>
    /// Род деятельности
    /// </summary>
    // [ExcelColumnName("Род деятельности")]
    [Name("Род деятельности")]
    public required string JobTitle { get; set; }
    
    /// <summary>
    /// Время работы
    /// </summary>
    [Name("Замер")]
    public required string JobTime { get; set; }
}