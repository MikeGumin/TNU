using System;
using TNU.Core.Models;

namespace TNU.Core.Services
{
    internal class CreateJobEntryServise
    {
        static int JobId = 1;
        static public JobEntryClock CreateJobEntry(string jobName)
        {
            var obj = new JobEntryClock();
            //obj.Entry.Id = JobId++;
            obj.Entry.JobDate = DateTime.Now;

            obj.Entry.JobName = jobName;

            GeneralUpdateTimer.AddEvent(obj);

            return obj;
        }
    }
}
