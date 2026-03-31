using System;
using MiniExcelLibs.Attributes;

namespace TNU.Services.EntryExport.Model;

/// <summary>
/// Модель экспорта записей
/// </summary>
public class EntryExportResponse
{
    /// <summary>
    /// ФИО сотрудника
    /// </summary>
    [ExcelColumnName("ФИО сотрудника")]
    public required string FullName { get; set; }
    
    /// <summary>
    /// Дата, когда проводилась работа
    /// </summary>
    [ExcelColumnName("Дата проведения работы")]
    public DateTimeOffset JobDate { get; set; }
    
    /// <summary>
    /// Род деятельности
    /// </summary>
    [ExcelColumnName("Род деятельности")]
    public required string JobTitle { get; set; }
    
    /// <summary>
    /// Время работы
    /// </summary>
    [ExcelColumnName("Замер")]
    public required string JobTime { get; set; }
}