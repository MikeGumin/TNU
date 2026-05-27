namespace TNU.Core.Models;

/// <summary>
/// Модель набора метаданных CSV-файла
/// </summary>
public class CsvMetaData
{
    /// <summary>
    /// Автор ФРД
    /// </summary>
    public required string Author { get; set; }

    /// <summary>
    /// Предприятие, на котором сделано ФРД
    /// </summary>
    public required string Enterprise { get; set; } = "Не указано";
}