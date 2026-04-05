using System;

namespace TNU.Core.Services.ClockAction
{
    internal interface IClockActionService
    {
        public void StartTimer();
        public void StopTimer();
        public void ReDrowTimer(object? sender, EventArgs e);
    }
}
