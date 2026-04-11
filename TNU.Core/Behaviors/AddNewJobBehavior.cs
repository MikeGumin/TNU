using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using TNU.Core.Models.Enum;
using TNU.Core.Repository;
using TNU.Core.Services.CsvFile;

namespace TNU.Core.Behaviors;

/// <summary>
/// Метод добавляюший поведения для тега AutoCompleteBox
/// </summary>
public class AddNewJobBehavior : Behavior<AutoCompleteBox>
{
    private bool _isDropDownOpen;
    private bool _ignoreNextLostFocus;
    
    /// <summary>
    /// Метод формирования задач, при нажатии поле с наименованием задач
    /// </summary>
    protected override void OnAttached()
    {
        base.OnAttached();
        if (AssociatedObject != null)
        {
            AssociatedObject.DropDownOpened += OnDropDownOpened;
            AssociatedObject.DropDownClosed += OnDropDownClosed;
            AssociatedObject.LostFocus += OnLostFocus;
            AssociatedObject.KeyDown += OnKeyDown;
        }
    }

    /// <summary>
    /// Метод удаления задач, при потере фокуса на поле наименование задачи
    /// </summary>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
        {
            AssociatedObject.DropDownOpened -= OnDropDownOpened;
            AssociatedObject.DropDownClosed -= OnDropDownClosed;
            AssociatedObject.LostFocus -= OnLostFocus;
            AssociatedObject.KeyDown -= OnKeyDown;
        }
    }
    
    /// <summary>
    /// Метод указывающий, что выпадающий список открыт
    /// </summary>
    /// <param name="sender">Объект</param>
    /// <param name="e">Ивент, действие</param>
    private void OnDropDownOpened(object? sender, EventArgs e)
    {
        _isDropDownOpen = true;
    }

    /// <summary>
    /// Метод указывающий, что выпадающий список закрыт
    /// </summary>
    /// <param name="sender">Объект</param>
    /// <param name="e">Ивент, действие</param>
    private void OnDropDownClosed(object? sender, EventArgs e)
    {
        _isDropDownOpen = false;
        // При закрытии выпадающего списка (выбором или кликом вне) блокируем следующую потерю фокуса,
        // чтобы дать AutoCompleteBox время обновить Text.
        _ignoreNextLostFocus = true;
    }


    /// <summary>
    /// Метод при нажатии enter в поле наименования работ
    /// </summary>
    /// <param name="sender">Объект</param>
    /// <param name="e">Ивент, действие</param>
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            TryAddNewJob();
            e.Handled = true;
        }
    }

    /// <summary>
    /// При потере фокуса у поля наименование задачи
    /// </summary>
    /// <param name="sender">Объект</param>
    /// <param name="e">Ивент, действие</param>
    private void OnLostFocus(object? sender, RoutedEventArgs e)
    {
        TryAddNewJob();
    }

    /// <summary>
    /// Метод добавления новой задачи в список задач
    /// </summary>
    private void TryAddNewJob()
    {
        if (AssociatedObject == null)
        {
            return;
        }

        string enteredText = AssociatedObject.Text?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(enteredText))
        {
            return;
        }
        
        if (AssociatedObject.SelectedItem != null)
        {
            return;
        }
        
        bool exists = JobNameRepository.JobNameList
            .Any(job => string.Equals(job.Name, enteredText, StringComparison.OrdinalIgnoreCase));

        if (!exists)
        {
            var newJob = new JobTitleEnum(enteredText);
            
            JobNameRepository.JobNameList.Add(newJob);
            
            ReadCsvFile.Write(enteredText);
        }
    }
}
