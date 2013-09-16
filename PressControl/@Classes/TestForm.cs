using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressControl
{
    // http://en.wikipedia.org/wiki/Waveform

    public class DataForm
    {
        private float phase = 0f;
        private float amplitude = 1f;
        private float invert = 1; // Yes=-1, No=1
        private float offset = 0f;

        public int Frequency { get; set; }
        public int Interval { get; set; }
        public SignalType Type { get; set; }

        public float GetValue(float time)
        {
            float value = 0f;
            float t = Frequency * time + phase;
            switch (Type)
            {
                // sign( sin( 2 * pi * t ) )
                case SignalType.Square:                     
                    value = Math.Sign(Math.Sin(2f * Math.PI * t));
                    break;
                // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                case SignalType.Triangle:
                    value = 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                    break;
                // 2 * ( t/a - floor( t/a + 1/2 ) )
                case SignalType.Saw:
                    value = 2f * (t - (float)Math.Floor(t + 0.5f));
                    break;
            }
            return (invert * amplitude * value + offset);
        }
    }

    public enum SignalType
    {
        Saw = 1,
        Triangle = 2,
        Square = 3,
    }
}
