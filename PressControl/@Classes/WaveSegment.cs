using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressControl
{
    /// <summary>
    /// A wavesegment 
    /// </summary>
    public class WaveSegment
    {
        public List<double> Data { get; set; }

        /// <summary>
        /// Create a signal-segment with start-end parameters and seconds.
        /// The constructor will create data using these three parameters.
        /// </summary>
        /// <param name="start">the start value of the sequence</param>
        /// <param name="end">the end value of the sequence</param>
        /// <param name="seconds">how many seconds will it take from one value to another</param>
        public WaveSegment(int start, int end, int seconds)
        {
            var result = new List<double>();

            // the length = 50 values per second X seconds
            var bufferLength = seconds * 50;
            
            // the change per seconds
            var intervalSec = Math.Abs((start - end) / seconds);

            // the change per ticks
            double intervalTicks = intervalSec / 50.0;

            // the direction of the change
            if (start > end)
            {
                intervalTicks = -1 * intervalTicks;
            }

            double cursor = start;

            // create the data array
            for (var j = 0; j < bufferLength; j++)
            {
                result.Add(cursor);
                cursor = cursor + intervalTicks;
            }

            Data = result;
        }

    }
}
