using System;

namespace TNU.Models
{
    /// <summary>
    /// Модель записи работы
    /// </summary>
    internal class JobEntry
    {   
        Worker Worker { get; set; }
        DateTimeOffset JobDate {  get; set; }
        decimal JobTimer { get; set; }
        string JobName {  get; set; }
        string JobDescription { get; set; }
    }
}
