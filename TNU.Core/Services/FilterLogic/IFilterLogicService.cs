using System.Collections.Generic;
using TNU.Core.Models;

namespace TNU.Core.Services.FilterLogic;

public interface IFilterLogicService
{
    /// <summary>
    /// Логика фильтрации всех записей из списка ФРД
    /// </summary>
    /// <param name="field">Поле</param>
    /// <param name="command">Выражение</param>
    /// <param name="value">Значение</param>
    public List<FrdModel> FilterAllFrdList(string field, string command, string value);
    
    /// <summary>
    /// Очистка пфильтров
    /// </summary>
    public void CleaningAllFrdList();
}