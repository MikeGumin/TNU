using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TNU.Models;
using TNU.Services.FileDialog;

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
    /// <param name="fileDialogService"></param>
    public Task<OperationResult<string>> ExportEntryAsync(ObservableCollection<JobEntry> entryList, IFileDialogService fileDialogService);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entryList"></param>
    public OperationResult ExportDiagrammaGanta(ObservableCollection<JobEntry> entryList);
}