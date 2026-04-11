using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using TNU.Core.Models;
using TNU.Core.Models.Enum;
using TNU.Core.Services.EntryExport.Model;
using TNU.Core.Services.FileDialog;

namespace TNU.Core.Services.EntryExport;

/// <inheritdoc />
public class EntryExportService: IEntryExportService
{
    /// <inheritdoc />
    public async Task<OperationResult<string>> CsvEntryAsync(
        ObservableCollection<JobEntry> entryList,
        IFileDialogService fileDialogService)
    {
        if (!entryList.Any())
        {
            return OperationResult<string>.Fail("У вас нет завершенных записей.");
        }
        
        var stream = await fileDialogService.SaveFileAsync($"output{DateTime.Now:yyyy-MM-dd_HH-mm-ss}");
        if (stream is null)
        {
            return OperationResult<string>.Fail("Ошибка при сохранение файла. Попробуйте еще раз.");
        }

        using (var writer = new StreamWriter(stream))
        {
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
        
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(exportList); // Автоматически записывает заголовки и данные
            }
        }

        return OperationResult<string>.Ok();
    }

    /// <inheritdoc />
    // public async Task<OperationResult<string>> ExportDiagrammaGanta(
    //     ObservableCollection<JobEntry> entryList,
    //     IFileDialogService fileDialogService)
    // {
    //     if (!entryList.Any())
    //     {
    //         return OperationResult<string>.Fail("У вас нет завершённых записей");
    //     }
    //     
    //     var stream = await fileDialogService.SaveFileAsync($"gantt-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx");
    //     
    //     if (stream is null)
    //     {
    //         return OperationResult<string>.Fail("Ошибка при сохранение файла. Попробуйте еще раз.");
    //     }
    //
    //     var startMin = entryList.Min(e => ParseToMinutes(e.StartTime));
    //     var endMin = entryList.Max(e => ParseToMinutes(e.EndTime));
    //     var duration = endMin - startMin; // количество минутных колонок
    //
    //     using var wb = new XLWorkbook();
    //     var ws = wb.AddWorksheet("Диаграмма Ганта");
    //
    //     FillTimelineHeader(ws, startMin, duration);
    //     FillJobRows(ws, entryList, startMin);
    //     ApplyFormatting(ws, duration);
    //     
    //     await using (stream)
    //     {
    //         wb.SaveAs(stream);
    //     }
    //     
    //    
    //
    //     return OperationResult<string>.Ok();
    // }

    /// <summary>
    /// Заполнение тайлайн
    /// </summary>
    /// <param name="ws">Лист Excel</param>
    /// <param name="startMin">Время начала</param>
    /// <param name="duration">Всего минут</param>
    // private void FillTimelineHeader(IXLWorksheet ws, int startMin, int duration)
    // {
    //     var baseTime = TimeSpan.FromMinutes(startMin);
    //
    //     for (int i = 0; i < duration+1; i++)
    //     {
    //         var time = baseTime + TimeSpan.FromMinutes(i);
    //         ws.Cell(SystemConst.HeaderRow, SystemConst.TimelineStartColumn + i).Value = $"{time.Hours}:{time.Minutes:D2}";
    //     }
    // }

    /// <summary>
    /// Заполнение работ и их строк
    /// </summary>
    /// <param name="ws">Лист Excel</param>
    /// <param name="entryList">Списко записей</param>
    /// <param name="startMin">Время начала</param>
    // private void FillJobRows(IXLWorksheet ws, IEnumerable<JobEntry> entryList, int startMin)
    // {
    //     int row = 2;
    //
    //     foreach (var entry in entryList.OrderBy(e => e.StartTime))
    //     {
    //         ws.Cell(row, SystemConst.JobNameColumn).Value = entry.JobName;
    //
    //         int colStart = ParseToMinutes(entry.StartTime) - startMin + SystemConst.TimelineStartColumn;
    //         int colEnd = ParseToMinutes(entry.EndTime) - startMin + SystemConst.TimelineStartColumn;
    //
    //         for (int col = colStart; col <= colEnd; col++)
    //         {
    //             ws.Cell(row, col).Value = entry.DifficultyFactor;
    //         }
    //         
    //         row++;
    //     }
    // }

    /// <summary>
    /// Применение форматирования для ячеек
    /// </summary>
    /// <param name="ws">Лист Excel</param>
    /// <param name="duration">Общее число минут</param>
    // private void ApplyFormatting(IXLWorksheet ws, int duration)
    // {
    //     ws.Column(SystemConst.JobNameColumn).Width = 25;
    //
    //     for (int col = SystemConst.TimelineStartColumn; col < SystemConst.TimelineStartColumn + duration; col++)
    //         ws.Column(col).Width = 5;
    //
    //     ws.SheetView.FreezeColumns(1);
    // }

    /// <summary>
    /// Разбирает строку времени и возвращает количество минут от начала суток.
    /// </summary>
    // private int ParseToMinutes(string timeString)
    // {
    //     return TimeSpan.TryParse(timeString, out var t) ? (int)t.TotalMinutes : 0;
    // }
}
