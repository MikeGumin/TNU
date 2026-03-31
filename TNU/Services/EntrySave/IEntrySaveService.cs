using System.Collections.Generic;
using TNU.Models;

namespace TNU.Services.EntrySave;

/// <summary>
/// Методы для сахранения записей о работе
/// </summary>
public interface IEntrySaveService
{
    // todo: Возможно стоит сделать асинхронным
    /// <summary>
    /// Метод сохранения всех завершенных записей о работе
    /// </summary>
    /// <param name="entryList">Массив записей о работе</param>
    public void SaveEntry(IEnumerable<JobEntry> entryList);
}