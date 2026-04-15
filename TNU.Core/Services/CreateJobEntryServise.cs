using System;
using TNU.Core.Models;

namespace TNU.Core.Services
{
    internal class CreateJobEntryServise
    {
        static int JobId;
        public JobEntry CreateJobEntry()
        {
            var obj = new JobEntry();
            obj.Id = JobId++;
            obj.JobDate = DateTime.Now;

            return obj;
        }
    }
}
