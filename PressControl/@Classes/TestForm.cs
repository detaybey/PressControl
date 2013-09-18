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
        public int Interval { get; set; }
        public int Amplitude { get; set; }

        public float Frequency
        {
            get
            {
                return 0.001f * (this.Interval); 
            }
        }

        public SignalType Type { get; set; }

        public float GetValue(float time)
        {

            float t = Frequency * time;
            float value = 0f;
            switch (Type)
            {
                case SignalType.Sine: // sin( 2 * pi * t )
                    value = (float)Math.Sin(2f * Math.PI * t);
                    break;

                case SignalType.Square: // sign( sin( 2 * pi * t ) )
                    value = Math.Sign(Math.Sin(2f * Math.PI * t));
                    break;

                case SignalType.Triangle: // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                    value = 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                    break;

                case SignalType.Sawtooth: // 2 * ( t/a - floor( t/a + 1/2 ) )
                    value = 2f * (t - (float)Math.Floor(t + 0.5f));
                    break;
            }
            return value * Amplitude;
        }
    }

    public enum SignalType
    {
        Sine = 0,
        Sawtooth = 1,
        Triangle = 2,
        Square = 3,
    }
}
