using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenDemo
{
    public  class NumberEventArgs: EventArgs
    {
        public int State;
        public DateTime TimesTemp;
        public NumberEventArgs(int state)
        {
            State = state;
            TimesTemp = DateTime.Now;
        }
    }
}
