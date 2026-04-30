namespace TNU.Core.Services.FileOpener;

public interface IFileOpenerService
{
    public void OpenFile();
    
    public void OpenFrdFile(string fileName);

    public void OpenExploier();
}