using System.Collections.Generic;
using System.Collections.ObjectModel;
using TNU.Models;

namespace TNU.Services.EntryExport;

/// <summary>
/// Метода взаимодействия с записями работы
/// </summary>
public interface IEntryExportService
{
    /// <summary>
    /// Метод для экспорта записей
    /// </summary>
    /// <param name="entryList">Массив записей</param>
    public void ExportEntry(IEnumerable<JobEntry> entryList);
}