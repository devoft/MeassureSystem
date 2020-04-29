using devoft.MeassureSystem.Length;
using devoft.MeassureSystem.Surface;
using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Volume
{

    public struct Meter3
    {
        public static Regex m2Reg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(mm3|cm3|dm3|m3|dam3|hm3|km3)$");

        public decimal Value { get; }
        public string OriginalUnit { get; }

        #region Unit properties

        /// <summary>
        /// Value in cubic milimeters
        /// </summary>
        public decimal Mm3 => Value * 1000000000;
        /// <summary>
        /// Value in cubic centimeters
        /// </summary>
        public decimal Cm3 => Value * 1000000;
        /// <summary>
        /// Value in cubic decimeters
        /// </summary>
        public decimal Dm3 => Value * 1000;
        /// <summary>
        /// Value in cubic meter
        /// </summary>
        public decimal M3 => Value;
        /// <summary>
        ///  Value in cubic decameter
        /// </summary>
        public decimal Dam3 => Value / 1000;
        /// <summary>
        /// Value in cubic hectometer
        /// </summary>
        public decimal Hm3 => Value / 1000000;
        /// <summary>
        /// Value in cubic kilometers
        /// </summary>
        public decimal Km3 => Value / 1000000000;

        #endregion Unit properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public Meter3(decimal value)
        {
            Value = value;
            OriginalUnit = "m3";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Original unit of measure</param>
        public Meter3(decimal value, string unit)
        {
            if (unit?.In("mm3", "cm3", "dm3", "m3", "dam3", "hm3", "km3") != true)
                throw new ArgumentException($"unit mut be a valid cubit meter unit");
            OriginalUnit = unit;
            Value = value * unit switch
                            {
                                "mm3"   => 0.000000001m,
                                "cm3"   => 0.000001m,
                                "dm3"   => 0.001m,
                                "m3"    => 1m,
                                "dam3"  => 1000m,
                                "hm3"   => 1000000m,
                                "km3"   => 1000000000m,
                                _       => 0m
                            };
        }

        public override string ToString()
        {
            var v = this.Value * OriginalUnit switch
                                 {
                                     "mm3"   => 1000000m,
                                     "cm3"   => 10000m,
                                     "dm3"   => 100m,
                                     "m3"    => 1m,
                                     "dam3"  => 0.01m,
                                     "hm3"   => 0.0001m,
                                     "km3"   => 0.000001m,
                                     _       => 0m
                                 };
            return $"{v:0.####################}{OriginalUnit}";
        }

        #region Operators

        public static implicit operator Meter3(decimal d)
            => new Meter3(d);

        public static explicit operator decimal(Meter3 m)
            => m.Value;

        public static explicit operator Meter3(string s)
            => Parse(s);

        public static Meter3 Parse(string s)
        {
            if (TryParse(s, out var m))
                return m.Value;
            else
                throw new FormatException($"Invalid format");
        }

        public static bool TryParse(string s, out Meter3? m)
        {
            if (m2Reg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                m = new Meter3(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            m = null;
            return false;
        }

        public static Meter3 operator +(Meter3 m1, Meter3 m2)
           => new Meter3(m1.Value + m2.Value);

        public static Meter3 operator -(Meter3 m1, Meter3 m2)
            => new Meter3(m1.Value - m2.Value);

        public static Meter3 operator *(decimal d, Meter3 m)
          => new Meter3(m.Value * d);
        public static Meter3 operator *(Meter3 m, decimal d)
          => new Meter3(m.Value * d);

        public static decimal operator /(Meter3 m1, Meter3 m2)
          => m1.Value / m2.Value;
        public static Meter3 operator /(Meter3 m, decimal d)
          => new Meter3(m.Value / d);
        public static Meter2 operator /(Meter3 m2, Meter m)
            => new Meter2(m2.Value / m.Value);

        public static Meter operator /(Meter3 m2, Meter2 m)
            => new Meter(m2.Value / m.Value);


        public static bool operator ==(Meter3 m1, Meter3 m2)
          => m1.Value == m2.Value;

        public static bool operator !=(Meter3 m1, Meter3 m2)
          => m1.Value != m2.Value;

        public static bool operator >(Meter3 m1, Meter3 m2)
          => m1.Value > m2.Value;

        public static bool operator <(Meter3 m1, Meter3 m2)
          => m1.Value > m2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Value == ((Meter3)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
