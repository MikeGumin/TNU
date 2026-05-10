using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using TNU.Core.Models.Enum;
using TNU.Core.Repository;

namespace TNU.Core.Services.FileDialog;

/// <inheritdoc />
public class FileDialogService : IFileDialogService
{
    private readonly Func<TopLevel?> _getTopLevel;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="getTopLevel">Функция получения корневого UI у окна</param>
    public FileDialogService(Func<TopLevel?> getTopLevel)
    {
        _getTopLevel = getTopLevel;
    }

    /// <inheritdoc />
    public async Task<Stream?> SaveFileAsync(string fileName)
    {
        var topLevel = _getTopLevel();
        if (topLevel is null) return null;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Сохранить отчёт",
            SuggestedFileName = fileName,
            DefaultExtension = "csv",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("CSV файл") { Patterns = new[] { "*.csv" } }
            }
        });

        return file is null ? null : await file.OpenWriteAsync();
    }
    
    /// <inheritdoc />
    public async Task OpenFileAsync()
    {
        var topLevel = _getTopLevel();
        if (topLevel is null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Открыть файл",
            AllowMultiple = false, // Только один файл
            FileTypeFilter = new[] 
            { 
                new FilePickerFileType("Текстовые файлы") { Patterns = new[] { "*.txt" } },
                new FilePickerFileType("Все файлы") { Patterns = new[] { "*" } }
            }
        });
        
        if (files.Count >= 1)
        {
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);

            string? line;

            while ((line = await streamReader.ReadLineAsync()) is not null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                var columns = line.Split(':');
                
                var jobName = columns[0].Trim();
                JobNameRepository.JobNameList.Add(new JobTitleEnum(jobName));

                if (columns.Length > 1)
                {
                    var code = columns[1].Trim();
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        JobNameRepository.JobNameCodeList[jobName] = code;    
                    }  
                }
            }
        }
    }
}