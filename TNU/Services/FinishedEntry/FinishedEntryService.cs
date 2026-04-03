using System.Collections.Generic;
using TNU.Models;
using TNU.Repository;

namespace TNU.Services.FinishedEntry;

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
    public void DeleteEntries()
    {
        FinishedEntriesRepository.FinishedEntries.Clear();
    }
    
    /// <inheritdoc />
    public void DeleteEntry(JobEntry entry)
    {
        FinishedEntriesRepository.FinishedEntries.Remove(entry);
    }
    
    /// <inheritdoc />
    public void EditEntry(JobEntry editEntry, JobEntry entry)
    {
        entry.JobWorker = editEntry.JobWorker;
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