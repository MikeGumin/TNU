using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TNU.Core.Models;
using TNU.Core.Services.FileDialog;

namespace TNU.Core.Services.EntryExport;

/// <summary>
/// Метода взаимодействия с записями работы
/// </summary>
public interface IEntryExportService
{
    /// <summary>
    /// Метод для экспорта записей
    /// </summary>
    /// <param name="entryList">Массив записей</param>
    /// <param name="fileDialogService">Сервис для работы с файловой системой</param>
    public Task<OperationResult<string>> CsvEntryAsync(ObservableCollection<JobEntry> entryList, IFileDialogService fileDialogService);

    /// <summary>
    /// Метод для экспорта записей в виде диаграммы ганта
    /// </summary>
    /// <param name="entryList">Массив записей</param>
    /// <param name="fileDialogService">Сервис для работы с файловой системой</param>
    // public Task<OperationResult<string>> ExportDiagrammaGanta(ObservableCollection<JobEntry> entryList, IFileDialogService fileDialogService);
}