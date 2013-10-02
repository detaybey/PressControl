using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressControl
{
    /*   -10      90
     *   diff= -10 - (+90)  = -100 => abs() => 100 
     *   interval/sec = 100/3  = 33.333
     *   interval/tick         = 33.3333/50 => 0.666666                        
     * 
     */

    public class WaveSegment
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Seconds { get; set; }

        public List<double> Data { get; set; }

        public WaveSegment(int start, int end, int seconds)
        {
            this.Start = start;
            this.End = end;
            this.Seconds = seconds;

            var result = new List<double>();
            var bufferLength = this.Seconds*50;

            var intervalSec = Math.Abs((this.Start - this.End) / this.Seconds);
            double intervalTicks = intervalSec / 50.0;

            if (start > end)
            {
                intervalTicks = -1 * intervalTicks;
            }

            double cursor = this.Start;


            for (var j = 0; j < bufferLength; j++)
            {
                result.Add(cursor);
                cursor = cursor + intervalTicks;
            }

            Data = result;
        }

    }
}
