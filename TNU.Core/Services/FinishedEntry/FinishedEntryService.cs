using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TNU.Core.Models;
using TNU.Core.Models.Enum;
using TNU.Core.Repository;

namespace TNU.Core.Services.FinishedEntry;

/// <inheritdoc />
public class FinishedEntryService : IFinishedEntryService
{

    public FinishedEntryService()
    {    
    }

    /// <inheritdoc />
    public void SaveEntry(IEnumerable<JobEntry> entryList)
    {
        foreach (var entry in entryList)
        {
            if (entry.RecordStatus is RecordStatusEnum.Finish)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    FinishedEntriesRepository.FinishedEntries.Add(entry);
                });
            }
        }
    }

    /// <inheritdoc />
    public OperationResult DeleteEntries()
    {
        try
        {
            FinishedEntriesRepository.FinishedEntries.Clear();

            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }

    /// <inheritdoc />
    public OperationResult DeleteEntry(JobEntry entry)
    {
        try
        {
            for (int i = 0; i < FinishedEntriesRepository.FinishedEntries.Count; i++)
            {
                if (FinishedEntriesRepository.FinishedEntries[i].Id == entry.Id)
                {
                    FinishedEntriesRepository.FinishedEntries.RemoveAt(i);
                }
            }

            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }

    /// <inheritdoc />
    public OperationResult EditEntry(JobEntry editEntry, JobEntry entry)
    {
        try
        {
            entry.JobName = editEntry.JobName;
            var changeResult = entry.ChangeEndTime(editEntry.EndTime);

            if (changeResult.IsFailed)
            {
                return OperationResult.Fail(changeResult.ErrorMessage);
            }

            changeResult = entry.ChangeStartTime(editEntry.StartTime);

            if (changeResult.IsFailed)
            {
                return OperationResult.Fail(changeResult.ErrorMessage);
            }

            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
        //return new  JobEntry()
        //{
        //    Id =  entry.Id,
        //    JobDate = entry.JobDate,
        //    JobName = editEntry.JobName,
        //    JobWorker =  editEntry.JobWorker,
        //    RecordStatus =  entry.RecordStatus,
        //    JobSample =  entry.JobSample,
        //};
    }
}