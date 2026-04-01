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
    public void DeleteEntry()
    {
        FinishedEntriesRepository.FinishedEntries.Clear();
    }
    
    /// <inheritdoc />
    public JobEntry EditEntry(JobEntry editEntry, JobEntry entry)
    {
        return new  JobEntry()
        {
            Id =  entry.Id,
            JobDate = entry.JobDate,
            JobName = editEntry.JobName,
            JobWorker =  editEntry.JobWorker,
            RecordStatus =  entry.RecordStatus,
            JobSample =  entry.JobSample,
        };
    }
}