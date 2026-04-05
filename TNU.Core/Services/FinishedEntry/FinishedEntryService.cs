using System;
using System.Collections.Generic;
using TNU.Core.Models;
using TNU.Core.Models.Enum;
using TNU.Core.Repository;

namespace TNU.Core.Services.FinishedEntry;

/// <inheritdoc />
public class FinishedEntryService : IFinishedEntryService
{
    /// <inheritdoc />
    public void SaveEntry(IEnumerable<JobEntry> entryList)
    {
        foreach (var entry in entryList)
        {
            if (entry.RecordStatus is RecordStatusEnum.Finish)
            {
                FinishedEntriesRepository.FinishedEntries.Add(entry);
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
            FinishedEntriesRepository.FinishedEntries.Remove(entry);
            
            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }
    
    /// <inheritdoc />
    public void EditEntry(JobEntry editEntry, JobEntry entry)
    {
        entry.JobName = editEntry.JobName;
        entry.ChangeEndTime(editEntry.EndTime);
        entry.ChangeStartTime(editEntry.StartTime);

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