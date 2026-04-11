using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

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
}