using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace CustomEaseDemo
{
    class CustomEase: EasingFunctionBase
    {
        public CustomEase()
            : base()
        {

        }

        protected override double EaseInCore(double normalizedTime)
        {
            return Math.Sqrt(normalizedTime)*0.9;
        }
    }
}
