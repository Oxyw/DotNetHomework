using System;

public class Clock
{
    public DateTime alarmTime; //闹钟要响的时间
    public DateTime nowTime; //当前时间
    
    public DateTime AlarmTime
    {
        get => alarmTime;
        set => alarmTime = value;
    }
    public DateTime NowTime
    {
        get => nowTime;
        set => nowTime = value;
    }

    //定义委托
    public delegate void AlarmHandler(object sender, DateTime args);
    public delegate void TickHanlder(object sender, DateTime args);
    //定义事件
    public event AlarmHandler OnAlarm;
    public event TickHanlder Ticking;

    public Clock()
	{
        //添加处理方法
        OnAlarm += Alarm;
        Ticking += Tick;
	}

    public void Alarm(object sender, DateTime time)
    {
        Console.WriteLine("The alarm for " + time + " goes off.");
    }

    public void Tick(object sender, DateTime time)
    {
        Console.Write("Time is " + time + ".");
    }

    
    public void StartAlarm(DateTime t)
    {
        AlarmTime = t;
        Console.WriteLine("The alarm will go off at " + alarmTime + ".");
        while(true)
        {
            NowTime = DateTime.Now;
            Ticking(this, nowTime);
            if (nowTime.ToString() == alarmTime.ToString())
            {
                Console.Write("\n");
                OnAlarm(this, alarmTime);
            }
            System.Threading.Thread.Sleep(1000);
            Console.Write("\r");
        }
    }
}