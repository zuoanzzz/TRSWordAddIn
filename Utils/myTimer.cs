using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TRSWordAddIn.Utils
{
    public class myTimer
    {
        private Timer timer;
        public myTimer(BaseRibbon b)
        {
            //TimerCallback callback = new TimerCallback(b.CheckTaskNumber);
            //timer = new Timer(callback,"test",0,1500);
        }
        public void Stop()
        {
            //timer.Dispose();
        }
    }
}
