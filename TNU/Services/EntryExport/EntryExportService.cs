using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MiniExcelLibs;
using TNU.Models;
using TNU.Services.EntryExport.Model;
using TNU.Services.FileDialog;
using ClosedXML.Excel;

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

    /// <inheritdoc />
    public void ExportDiagrammaGanta(ObservableCollection<JobEntry> entryList)
    {
        if (!entryList.Any())
        {
            return; // todo: система уведомлений — "У вас нет завершённых записей"
        }

        var startMin = entryList.Min(e => ParseToMinutes(e.StartTime));
        var endMin = entryList.Max(e => ParseToMinutes(e.EndTime));
        var duration = endMin - startMin; // количество минутных колонок

        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Диаграмма Ганта");

        FillTimelineHeader(ws, startMin, duration);
        FillJobRows(ws, entryList, startMin);
        ApplyFormatting(ws, duration);

        wb.SaveAs($"gantt-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx");
    }

    /// <summary>
    /// Заполнение тайлайн
    /// </summary>
    /// <param name="ws">Лист Excel</param>
    /// <param name="startMin">Время начала</param>
    /// <param name="duration">Всего минут</param>
    private void FillTimelineHeader(IXLWorksheet ws, int startMin, int duration)
    {
        var baseTime = TimeSpan.FromMinutes(startMin);

        for (int i = 0; i < duration+1; i++)
        {
            var time = baseTime + TimeSpan.FromMinutes(i);
            ws.Cell(SystemConst.HeaderRow, SystemConst.TimelineStartColumn + i).Value = $"{time.Hours}:{time.Minutes:D2}";
        }
    }

    /// <summary>
    /// Заполнение работ и их строк
    /// </summary>
    /// <param name="ws">Лист Excel</param>
    /// <param name="entryList">Списко записей</param>
    /// <param name="startMin">Время начала</param>
    private void FillJobRows(IXLWorksheet ws, IEnumerable<JobEntry> entryList, int startMin)
    {
        int row = 2;

        foreach (var entry in entryList.OrderBy(e => e.StartTime))
        {
            ws.Cell(row, SystemConst.JobNameColumn).Value = entry.JobName;

            int colStart = ParseToMinutes(entry.StartTime) - startMin + SystemConst.TimelineStartColumn;
            int colEnd = ParseToMinutes(entry.EndTime) - startMin + SystemConst.TimelineStartColumn;

            for (int col = colStart; col <= colEnd; col++)
                ws.Cell(row, col).Value = 1;

            row++;
        }
    }

    /// <summary>
    /// Применение форматирования для ячеек
    /// </summary>
    /// <param name="ws">Лист Excel</param>
    /// <param name="duration">Общее число минут</param>
    private void ApplyFormatting(IXLWorksheet ws, int duration)
    {
        ws.Column(SystemConst.JobNameColumn).Width = 25;

        for (int col = SystemConst.TimelineStartColumn; col < SystemConst.TimelineStartColumn + duration; col++)
            ws.Column(col).Width = 5;

        ws.SheetView.FreezeColumns(1);
    }

    /// <summary>
    /// Разбирает строку времени и возвращает количество минут от начала суток.
    /// </summary>
    private int ParseToMinutes(string timeString)
    {
        return TimeSpan.TryParse(timeString, out var t) ? (int)t.TotalMinutes : 0;
    }
}
