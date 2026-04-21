using System.Collections.Generic;

namespace TNU.Core.Models.Enum;

/// <summary>
/// Класс для создания экземпляров возможных работ
/// </summary>
public class JobTitleEnum
{
    /// <summary>
    /// Наименование работы
    /// </summary>
    public string Name { get; set; }
    
    public override string ToString() => Name;

    public JobTitleEnum(string jobName) 
    {
        Name = jobName;
    }
}
