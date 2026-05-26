using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using TNU.Core.Models;


namespace TNU.Core.ViewModels.MainWindow
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<object> PaneItems { get; set; }

        private object _selectedPageItem;
        public object SelectedPageItem
        {
            get => _selectedPageItem;
            set
            {
                _selectedPageItem = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(Observation obs)
        {
            MainPageView main = new MainPageView();
            Page.MainPage.MainPageViewModel maintPage = App.Services.GetRequiredService<Page.MainPage.MainPageViewModel>();
            maintPage.MainObservation = obs;
            main.DataContext = maintPage;

            FrdPageView frd = new FrdPageView();
            FrdPageViewModel frdPage = App.Services.GetRequiredService<FrdPageViewModel>();
            frdPage.ObservationElement = obs;
            frd.DataContext = frdPage;


            PaneItems = new ObservableCollection<object>([
                main,
                frd
                ]);

            //SelectedPageItem = new MainPageView();
            SelectedPageItem = PaneItems[0];

        }


        [RelayCommand]
        public void ShowMainPage() => SelectedPageItem = PaneItems[0];

        [RelayCommand]
        public void ShowFrdPage() => SelectedPageItem = PaneItems[1];

    }
}
