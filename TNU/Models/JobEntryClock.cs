using TNU.Services.ClockAction;

namespace TNU.Models
{
    internal class JobEntryClock
    {
        JobEntry JobEntry { get; set; } = new JobEntry();
        ClockActionService clockActionService { get; set; } = new ClockActionService();
    }
}
