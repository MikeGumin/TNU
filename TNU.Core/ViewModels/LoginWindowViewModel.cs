using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.FileDialog;
using TNU.Core.Services.FileOpener;
using TNU.Core.ViewModels.MainWindow;
using TNU.Core.Views.DialogViews;

namespace TNU.Core.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        private bool _isActivityListExport = false;
        private readonly IWindowService _windowService;
        private readonly IFileDialogService _fileDialogService;
        private readonly IFileOpenerService _fileOpenerService;
        public Observation ObservationElement { get; set; } = new Observation();

        public LoginWindowViewModel()
        {
        }
        public LoginWindowViewModel(
            IWindowService windowService,
            IFileOpenerService fileOpenerService,
            IFileDialogService fileDialogService)
        {
            _windowService = windowService;
            _fileOpenerService = fileOpenerService;
            _fileDialogService = fileDialogService;
        }

        [RelayCommand]
        public void Login()
        {
            // Проверка введены ли данные в окна. Работает, но пока убрал 
            if (IsCompleted())
                OnLoginSuccess();
        }

        [RelayCommand]
        public void OpenFile()
        {
            _fileOpenerService.OpenFile(SystemConst.JobNameFilePath);
        }

        [RelayCommand]
        public void OpenSecondFile()
        {
            _fileOpenerService.OpenFile(SystemConst.StartingPreparationsFilePath);
        }

        private void OnLoginSuccess()
        {
            if (!_isActivityListExport)
            {
                JobNameRepository.FillJobNameList();
            }

            var mainWindow = new Core.Views.MainWindow
            {
                DataContext = new MainWindowViewModel(ObservationElement)
            };

            mainWindow.Show();

            _windowService.CloseCurrentWindow(mainWindow);
        }

        /// <summary>
        /// Открыть список всех ФРД
        /// </summary>
        [RelayCommand]
        private void OpenFrdList()
        {
            var files = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "output*", SearchOption.AllDirectories);
            FrdRepository.FinishedFrd.Clear();

            var frdId = 1;

            foreach (var file in files)
            {
                FrdRepository.FinishedFrd.Add(new FrdModel()
                {
                    Id = frdId++,
                    FileName = Path.GetFileName(file),
                    CreatedAt = File.GetCreationTime(file),
                });
            }

            var frdWindow = new Views.AllFrdWindow()
            {
                DataContext = App.Services.GetRequiredService<AllFrdWindowViewModel>()
            };

            if (frdWindow.DataContext is AllFrdWindowViewModel a)
            {
                a.MainObservation = ObservationElement;
                a.ViewModel = this;
            }

            frdWindow.Show();

            _windowService.CloseCurrentWindow(frdWindow);
        }

        /// <summary>
        /// Загрузка перечня наименования задач
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task<OperationResult<string>> ImportActivityList()
        {
            await _fileDialogService.OpenFileAsync();

            return OperationResult<string>.Ok();
        }

        private bool IsCompleted()
        {
            return (!string.IsNullOrWhiteSpace(ObservationElement.RespondentId) && !string.IsNullOrWhiteSpace(ObservationElement.InspectorName));
        }
    }
}
