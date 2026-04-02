using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MiniExcelLibs;
using TNU.Models;
using TNU.Services.EntryExport.Model;
using TNU.Services.FileDialog;

namespace TNU.Services.EntryExport;

/// <inheritdoc />
public class EntryExportService: IEntryExportService
{
    /// <inheritdoc />
    public async Task ExportEntryAsync(
        ObservableCollection<JobEntry> entryList,
        IFileDialogService fileDialogService)
    {
        if (!entryList.Any())
        {
            return; // todo: Нужно придумать какую-то систему уведомлений. Например - "У вас нет завершенных записей"
        }
        
        var stream = await fileDialogService.SaveFileAsync($"output{DateTime.Now:yyyy-MM-dd_HH-mm-ss}");
        if (stream is null) return;
        
        var exportList = new List<EntryExportResponse>();

        foreach (var entry in entryList)
        {
            if (entry.RecordStatus is RecordStatusEnum.Finish)
            {
                exportList.Add(new EntryExportResponse()
                {
                    FullName = entry.JobWorker.FullName,
                    JobTitle = entry.JobName,
                    JobTime = entry.JobSample,
                    JobDate =  entry.JobDate,
                });
            }
        }
        
        await using (stream)
        {
            MiniExcel.SaveAs(stream, exportList);
        }
        
    }
}