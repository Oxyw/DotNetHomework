using System;

namespace Alarm
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            DateTime at = new DateTime();
            at = DateTime.Now.AddSeconds(7);//设定要响的时间
            clock.NowTime = DateTime.Now; //设定当前时间
            clock.StartAlarm(at);
        }
    }
}
