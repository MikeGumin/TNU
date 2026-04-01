using System.Collections.Generic;
using TNU.Models;

namespace TNU.Services.FinishedEntry;

/// <summary>
/// Методы для сахранения записей о работе
/// </summary>
public interface IFinishedEntryService
{
    // todo: Возможно стоит сделать асинхронным
    /// <summary>
    /// Метод сохранения всех завершенных записей о работе
    /// </summary>
    /// <param name="entryList">Массив записей о работе</param>
    public void SaveEntry(IEnumerable<JobEntry> entryList);
    
    /// <summary>
    /// Метод удаления всех завершенных записей о работе
    /// </summary>
    public void DeleteEntries();
    
    /// <summary>
    /// Метод удаления завершенной записи о работе
    /// </summary>
    public void DeleteEntry(JobEntry entry);

    /// <summary>
    /// Метод редактирования завершенной записи о работе
    /// </summary>
    /// <param name="editEntry">Отредактированная запись</param>
    /// <param name="entry">Запись о работе</param>
    public JobEntry EditEntry(JobEntry editEntry, JobEntry entry);
}