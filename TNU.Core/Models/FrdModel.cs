namespace TNU.Core.Models;

public class FrdModel
{
    /// <summary>
    /// Идентификатор файла ФРД
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Наименование файла
    /// </summary>
    public required string FileName { get; set; } 
}