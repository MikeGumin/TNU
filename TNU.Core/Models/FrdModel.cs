using System;

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
    
    /// <summary>
    /// Дата создания фрд
    /// </summary>
    public required DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Автор ФРД
    /// </summary>
    public required string Author { get; set; }
    
    /// <summary>
    /// Предприятие, на котором был сделано ФРД
    /// </summary>
    public required string Enterprise { get; set; }
}