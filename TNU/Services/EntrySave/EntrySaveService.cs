using System.Collections.Generic;
using TNU.Models;
using TNU.Repository;
using TNU.ViewModels;

namespace TNU.Services.EntrySave;

/// <inheritdoc />
public class EntrySaveService : IEntrySaveService
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
}