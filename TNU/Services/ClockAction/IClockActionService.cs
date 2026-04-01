using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNU.Services.ClockAction
{
    internal interface IClockActionService
    {
        public void StartTimer();
        public void StopTimer();
        public void ReDrowTimer(object? sender, EventArgs e);
    }
}
