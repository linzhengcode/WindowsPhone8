using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenDemo
{
    class NumberTask
    {
        public delegate void MyDelegate(int ArgValue);
        public MyDelegate GetNumber;

        public delegate void StateChanged(NumberTask sender, NumberEventArgs args);
        public event StateChanged StateChangedEvent;
        public string TaskString = "测试";

        public void OnStateChanged(NumberEventArgs args)
        {
            if (StateChangedEvent != null)
            {
                StateChangedEvent(this, args);
            }
        }

        public void StartNumberTask(int num)
        {
            Random random = new Random();
            int number = random.Next(0, 1000);
            //if (GetNumber != null)
            //{
            //    GetNumber(number);
            //}

            OnStateChanged(new NumberEventArgs(number));
        }
    }
}
