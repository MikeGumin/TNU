using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using TNU.Core.ViewModels;

namespace TNU.Core.Views;

public partial class AllFrdWindow : Window
{
    private AllFrdWindowViewModel ViewModel => (AllFrdWindowViewModel)DataContext!;
    
    private readonly Dictionary<string, string[]> operatorsBox = new()
    {
        {"Id", ["=", "!=", ">", "<", ">=", "<="]},
        {"Наименование файла", ["=", "!="]},
        {"Время окн.", ["=", "!=", ">", "<", ">=", "<="]},
        {"ФИО эксперта", ["=", "!="]},
        {"Предприятие", ["=", "!="]},
        {"", []}
    };
    
    public AllFrdWindow()
    {
        InitializeComponent();
        
    }
    
    private void FieldComboBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
    {
        if (this.DataContext is not AllFrdWindowViewModel viewModel)
        {
            return;
        }

        if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            var selectedText = selectedItem.Content?.ToString() ?? "";
        
            if (operatorsBox.TryGetValue(selectedText, out var operators))
            {
                viewModel.ListOperators.Clear();
            
                foreach (var op in operators)
                {
                    viewModel.ListOperators.Add(op);
                }
            }
        }
    }
}