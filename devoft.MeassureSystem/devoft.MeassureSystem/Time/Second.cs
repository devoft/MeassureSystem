using devoft.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Time
{
    public struct Seconds
    {
        static readonly Regex gReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(s|min|h|ms)$");

        public Seconds(double s)
        {
            Value = s;
            OriginalUnit = "s";
        }

        public Seconds(double value, string unit)
        {
            Value = value;
            if (unit?.In("h", "min", "s", "ms") != true)
                throw new ArgumentException("unit mut be a valid gram unit", nameof(unit));
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "s"     => 1.0,
                "h"     => 3600.0,
                "min"   => 60.0,
                "ms"    => 0.001,
                _       => 0.0
            };
        }

        public double s => Value;
        public double h => Value / 3600.0;
        public double min => Value / 60.0;
        public double ms => Value * 1000.0;
        public double d => Value / (3600.0 * 24.0);

        public string OriginalUnit { get; }
        public double Value { get; }

        #region [ operators ]

        public static explicit operator double(Seconds seconds)
            => seconds.Value;

        public static implicit operator Seconds(double seconds)
            => new Seconds(seconds);

        public static explicit operator Seconds (TimeSpan time) 
            => new Seconds(time.TotalSeconds);

        public static implicit operator TimeSpan(Seconds seconds)
            => TimeSpan.FromSeconds(seconds.Value);

        public static explicit operator Seconds(string time)
            => Seconds.Parse(time);

        public static implicit operator string(Seconds seconds)
            => seconds.ToString();

        public static Seconds operator +(Seconds g1, Seconds g2)
            => new Seconds(g1.Value + g2.Value);

        public static Seconds operator -(Seconds g1, Seconds g2)
            => new Seconds(g1.Value - g2.Value);

        public static Seconds operator *(double scalar, Seconds g)
            => new Seconds(scalar * g.Value);

        public static Seconds operator *(Seconds g, double scalar)
            => new Seconds(scalar * g.Value);

        public static Seconds operator /(Seconds g, double scalar)
            => new Seconds(g.Value / scalar);

        public static double operator /(Seconds g1, Seconds g2)
            => g1.Value / g2.Value;

        public static Seconds operator -(Seconds g)
            => new Seconds(-g.Value, g.OriginalUnit);

        public static bool operator >(Seconds g1, Seconds g2)
            => g1.Value > g2.Value;

        public static bool operator <(Seconds g1, Seconds g2)
            => g1.Value < g2.Value;

        public static bool operator ==(Seconds g1, Seconds g2)
            => g1.Value == g2.Value;

        public static bool operator !=(Seconds g1, Seconds g2)
            => g1.Value != g2.Value;


        #endregion [ operators ]

        private static Seconds Parse(string time) 
            => TryParse(time, out var value)
                  ? value.Value
                  : throw new FormatException($"Invalid time format {time}");

        private static bool TryParse(string str, out Seconds? seconds)
        {
            if (gReg.Match(str)?.Groups is GroupCollection g && g.Count > 2)
            {
                seconds = new Seconds(double.Parse(g[1].Value), g[2].Value);
                return true;
            }
            seconds = null;
            return false;
        }

        public override bool Equals(object obj)
            => ((Seconds)obj).Value == Value;

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
    }
}
