using devoft.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Time
{
    public struct Milliseconds
    {
        static readonly Regex gReg = new Regex(@"([0-9]+)(?:\s)*(s|min|h|ms|d)$");

        public Milliseconds(int s)
        {
            Value = s;
            OriginalUnit = "s";
        }

        public Milliseconds(int value, string unit)
        {
            Value = value;
            if (unit?.In("h", "min", "s", "ms", "d") != true)
                throw new ArgumentException("unit mut be a valid gram unit", nameof(unit));
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "s"     => 1_000,
                "h"     => 3_600_000,
                "min"   => 60_000,
                "ms"    => 1,
                "d"     => 24 * 3_600_000,
                _       => 0
            };
        }

        public int s => Value / 1000;
        public int h => Value / 3_600_000;
        public int min => Value / 60_000;
        public int ms => Value;
        public int d => Value / (3_600_000 * 24);

        public string OriginalUnit { get; }
        public int Value { get; }

        #region [ operators ]

        public static explicit operator double(Milliseconds seconds)
            => seconds.Value;

        public static implicit operator Milliseconds(int seconds)
            => new Milliseconds(seconds);

        public static explicit operator Milliseconds (TimeSpan time) 
            => new Milliseconds(Convert.ToInt32(time.TotalMilliseconds));

        public static implicit operator TimeSpan(Milliseconds seconds)
            => TimeSpan.FromMilliseconds(seconds.Value);

        public static explicit operator Milliseconds(string time)
            => Milliseconds.Parse(time);

        public static implicit operator string(Milliseconds seconds)
            => seconds.ToString();

        public static Milliseconds operator +(Milliseconds g1, Milliseconds g2)
            => new Milliseconds(g1.Value + g2.Value);

        public static Milliseconds operator -(Milliseconds g1, Milliseconds g2)
            => new Milliseconds(g1.Value - g2.Value);

        public static Milliseconds operator *(int scalar, Milliseconds g)
            => new Milliseconds(scalar * g.Value);

        public static Milliseconds operator *(Milliseconds g, int scalar)
            => new Milliseconds(scalar * g.Value);

        public static Milliseconds operator /(Milliseconds g, int scalar)
            => new Milliseconds(g.Value / scalar);

        public static int operator /(Milliseconds g1, Milliseconds g2)
            => g1.Value / g2.Value;

        public static Milliseconds operator -(Milliseconds g)
            => new Milliseconds(-g.Value, g.OriginalUnit);

        public static bool operator >(Milliseconds g1, Milliseconds g2)
            => g1.Value > g2.Value;

        public static bool operator <(Milliseconds g1, Milliseconds g2)
            => g1.Value < g2.Value;

        public static bool operator ==(Milliseconds g1, Milliseconds g2)
            => g1.Value == g2.Value;

        public static bool operator !=(Milliseconds g1, Milliseconds g2)
            => g1.Value != g2.Value;


        #endregion [ operators ]

        private static Milliseconds Parse(string time) 
            => TryParse(time, out var value)
                  ? value.Value
                  : throw new FormatException($"Invalid time format {time}");

        private static bool TryParse(string str, out Milliseconds? seconds)
        {
            if (gReg.Match(str)?.Groups is GroupCollection g && g.Count > 2)
            {
                seconds = new Milliseconds(int.Parse(g[1].Value), g[2].Value);
                return true;
            }
            seconds = null;
            return false;
        }

        public override bool Equals(object obj)
            => ((Milliseconds)obj).Value == Value;

        public override int GetHashCode()
            => Value.GetHashCode();


        public override string ToString()
            => OriginalUnit switch
            {
                "s"   => $"{Value}s",
                "h"   => $"{h}h",
                "min" => $"{min}min",
                "ms"  => $"{ms}ms",
                "d"   => $"{d}day",
                _     => null
            };

        public string ToString(string format)
            => ((TimeSpan)this).ToString(format);

        public void Deconstruct(out int hour, out int minutes, out int seconds, out int milliseconds)
        {
            TimeSpan ts = this;
            (hour, minutes, seconds, milliseconds) = (ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
