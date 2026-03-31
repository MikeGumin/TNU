using System.Collections.Generic;

namespace TNU.Models
{
    internal class JobTitleEnum
    {
        public string Name { get; set; }
        public List<Role> Enums { get; set; } = new List<Role>() { Role.Defolt };

        public JobTitleEnum(string jobName) 
        {
            Name = jobName;
        }
    }
}
