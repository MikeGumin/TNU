using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MiniExcelLibs;
using TNU.Models;
using TNU.Services.EntryExport.Model;

namespace TNU.Services.EntryExport;

public class EntryExportService: IEntryExportService
{
    public void ExportEntry(ObservableCollection<JobEntry> entryList)
    {
        if (entryList.Count == 0)
        {
            return; // todo: Нужно придумать какую-то систему уведомлений. Например - "У вас нет завершенных записей"
        }
        
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

        MiniExcel.SaveAs($"output{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx", exportList);
    }
}