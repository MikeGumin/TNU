using TNU.Core.Services.ClockAction;

namespace TNU.Core.Models
{
    internal class JobEntryClock
    {
        JobEntry JobEntry { get; set; } = new JobEntry();
        ClockActionService clockActionService { get; set; } = new ClockActionService();
    }
}
