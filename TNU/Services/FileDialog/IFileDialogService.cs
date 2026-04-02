using System.IO;
using System.Threading.Tasks;

namespace TNU.Services.FileDialog;

/// <summary>
/// Сервис для взаимодействия с файловой системой 
/// </summary>
public interface IFileDialogService
{
    /// <summary>
    /// Сохранение файла в выбранную папку
    /// </summary>
    /// <param name="fileName">Название файла</param>
    Task<Stream?> SaveFileAsync(string fileName);
}