using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using TNU.Core.Models;
using TNU.Core.Services.EntryExport.Model;

namespace TNU.Core;

public static class GetMetaDataHelper
{
    /// <summary>
    /// Получение метадаты из комментариев CSV-файла ФРД 
    /// </summary>
    /// <param name="filePath">Полный путь до файла ФРД</param>
    public static CsvMetaData GetMetaData(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            Delimiter = ";",
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);
        
        csv.Read();
        csv.ReadHeader();
        
        csv.Read();

        var author = csv.GetField("ФИО наблюдателя");
        var enterprise = csv.GetField("Предприятие");

        return new CsvMetaData()
        {
            Author = string.IsNullOrWhiteSpace(author) ? "Не указано" : author,
            Enterprise = string.IsNullOrWhiteSpace(enterprise) ? "Не указано" : enterprise,
        };
    }
}