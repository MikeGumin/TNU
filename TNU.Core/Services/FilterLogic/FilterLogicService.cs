using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TNU.Core.Models;
using TNU.Core.Repository;

namespace TNU.Core.Services.FilterLogic;

public class FilterLogicService : IFilterLogicService
{
    /// <inheritdoc/> 
    public List<FrdModel> FilterAllFrdList(string field, string command, string value)
    {
        return field switch
        {
            "Id" => FilterById(command, value),
            "Наименование файла" => FilterByFileName(command, value),
            "Время окн." => FilterByCreatedAt(command, value),
            "ФИО эксперта" => FilterByAuthor(command, value),
            "Предприятие" => FilterByEnterprise(command, value),
        };
    }

    /// <inheritdoc/> 
    public void CleaningAllFrdList()
    {
        FrdRepository.FinishedFrd.Clear();

        var frdId = 1;
        
        foreach (var item in FrdRepository.FinishedFrdReserve)
        {
            FrdRepository.FinishedFrd.Add(new FrdModel()
            {
                Id = frdId++,
                FileName = item.FileName,
                Author = item.Author,
                CreatedAt = item.CreatedAt,
                Enterprise = item.Enterprise
            });
        }
    }

    private List<FrdModel> FilterById(string command, string value)
    {
        if (!int.TryParse(value, out int id))
        {
            return new();
        }

        return (command) switch
        {
            "=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Id == id).ToList(),
            "!=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Id != id).ToList(),
            ">" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Id > id).ToList(),
            "<" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Id < id).ToList(),
            ">=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Id >= id).ToList(),
            "<=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Id <= id).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
    }
    
    private List<FrdModel>  FilterByFileName(string command, string value)
    {
        return (command) switch
        {
            "=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.FileName == value).ToList(),
            "!=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.FileName != value).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
    }
    
    // todo: Пока думаю что сделать. В планах сделать все операторы на год, месяц, день 
    private List<FrdModel> FilterByCreatedAt(string command, string value)
    {
        switch (command)
        {
            case "=":
                break;
            case ">":
                break;
            case "<":
                break;
            case ">=":
                break;
            case "<=":
                break;
            case "!=":
                break;
        }

        return new();
    }
    
    private List<FrdModel> FilterByAuthor(string command, string value)
    {
        return (command) switch
        {
            "=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Author == value).ToList(),
            "!=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Author != value).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
    }
    
    private List<FrdModel> FilterByEnterprise(string command, string value)
    {
        return (command) switch
        {
            "=" => FrdRepository.FinishedFrdReserve.Where(elem => elem.Enterprise == value).ToList(),
            "!=" =>FrdRepository.FinishedFrdReserve.Where(elem => elem.Enterprise != value).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
    }
}