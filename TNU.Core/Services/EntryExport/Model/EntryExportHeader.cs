using CsvHelper.Configuration.Attributes;

namespace TNU.Core.Services.EntryExport.Model;
/// <summary>
/// Модель контракта заголовка у экспортируемого файла
/// </summary>
public class EntryExportHeader
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    [Name("ФИО наблюдателя")]
    public required string ExpertFullName { get; set; }
    
    /// <summary>
    /// Род деятельности
    /// </summary>
    // [ExcelColumnName("Род деятельности")]
    [Name("Дата")]
    public required string EntryDate { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Name("Код респондента")]
    public required string RespondentCode { get; set; }
    
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    [Name("ФИО респондента")]
    public string? RespondentFullName { get; set; }
    
    /// <summary>
    /// Род деятельности
    /// </summary>
    [Name("Предприятие")]
    public string? Enterprise { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Name("Город")]
    public string? City { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Name("Начало наблюдения")]
    public required string StartObservation { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Name("Конец наблюдения")]
    public required string EndObservation { get; set; }
}