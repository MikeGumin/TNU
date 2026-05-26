using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CsvHelper.Configuration;
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
        
        var csvOnly = new FilePickerFileType("CSV Files")
        {
            Patterns = ["*.csv"],
            MimeTypes = ["text/csv"]
        };

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Открыть файл",
            AllowMultiple = false, // Только один файл
            FileTypeFilter = [ csvOnly ]
        });
        
        if (files.Count >= 1)
        {
            string relativePath = SystemConst.JobNameFilePath;
            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)
            };
            
            await using (StreamWriter writer = new StreamWriter(fullPath, false, config.Encoding))
            {
                await using var stream = await files[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream, config.Encoding);

                string? line;

                while ((line = await streamReader.ReadLineAsync()) is not null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    writer.WriteLine(line);
                
                    // var columns = line.Split(':');
                    //
                    // var jobName = columns[0].Trim();
                    // JobNameRepository.JobNameList.Add(new JobTitleEnum(jobName));
                    //
                    // if (columns.Length > 1)
                    // {
                    //     var code = columns[1].Trim();
                    //     if (!string.IsNullOrWhiteSpace(code))
                    //     {
                    //         JobNameRepository.JobNameCodeList[jobName] = code;    
                    //     }  
                    // }
                }
            }
            
        }
        
        // foreach (var file in JobNameRepository.JobNameCodeList) Console.WriteLine(file.Key + "-" + file.Value);
        
    }
}