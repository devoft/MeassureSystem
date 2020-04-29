using devoft.MeassureSystem.Length;
using devoft.MeassureSystem.Volume;
using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Surface
{
    public struct Meter2
    {
        public static Regex m2Reg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(m2|km2|hm2|dam2|dm2|cm2|mm2)$");

        public decimal Value { get; }
        public string OriginalUnit { get; }

        #region Unit properties
        public decimal mm2 => Value * 1000000;
        public decimal cm2 => Value * 10000;
        public decimal dm2 => Value * 100;
        public decimal m2 => Value;
        public decimal dam2 => Value / 100;
        public decimal hm2 => Value / 10000;
        public decimal km2 => Value / 1000000;

        #endregion Unit properties

        public Meter2(decimal value)
        {
            Value = value;
            OriginalUnit = "m2";
        }

        public Meter2(decimal value, string unit)
        {
            if (unit?.In("mm2", "cm2", "dm2", "m2", "dam2", "hm2", "km2") != true)
                throw new ArgumentException($"unit mut be a valid square meter unit");
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "mm2"   => 0.000001m,
                "cm2"   => 0.0001m,
                "dm2"   => 0.01m,
                "m2"    => 1m,
                "dam2"  => 100m,
                "hm2"   => 10000m,
                "km2"   => 1000000m,
                _       => 0m
            };
        }

        public override string ToString()
        {
            var v = this.Value * OriginalUnit switch
                                 {
                                     "km2" => 0.000001m,
                                     "hm2" => 0.0001m,
                                     "dam2" => 0.01m,
                                     "m2" => 1m,
                                     "dm2" => 100m,
                                     "cm2" => 10000m,
                                     "mm2" => 1000000m,
                                     _ => 0m
                                 };
            return $"{v:0.####################}{OriginalUnit}";
        }

        #region Operators

        public static implicit operator Meter2(decimal d)
            => new Meter2(d);

        public static explicit operator decimal(Meter2 m)
            => m.Value;

        public static explicit operator Meter2(string s)
            => Parse(s);

        public static Meter2 Parse(string s)
        {
            if (TryParse(s, out var m))
                return m.Value;
            else
                throw new FormatException($"Invalid format");
        }

        public static bool TryParse(string s, out Meter2? m)
        {
            if (m2Reg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                m = new Meter2(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            m = null;
            return false;
        }

        public static Meter2 operator + (Meter2 m1, Meter2 m2)
           => new Meter2(m1.Value + m2.Value);


        public static Meter2 operator - (Meter2 m1, Meter2 m2)
            => new Meter2(m1.Value - m2.Value);


        public static Meter2 operator *(decimal d, Meter2 m2)
          => new Meter2(m2.Value * d);
        public static Meter2 operator *(Meter2 m2, decimal d)
          => new Meter2(m2.Value * d);
        public static Meter3 operator *(Meter2 m1, Meter m2)
          => new Meter3(m1.Value * m2.Value);
        public static Meter3 operator *(Meter m1, Meter2 m2)
          => new Meter3(m1.Value * m2.Value);

        public static decimal operator /(Meter2 m1, Meter2 m2)
          => m1.Value / m2.Value;
        public static Meter2 operator /(Meter2 m, decimal d)
          => new Meter2(m.Value / d);
        public static Meter operator /(Meter2 m2, Meter m)
          => new Meter(m2.Value / m.Value);

        public static bool operator ==(Meter2 m1, Meter2 m2)
          => m1.Value == m2.Value;

        public static bool operator !=(Meter2 m1, Meter2 m2)
          => m1.Value != m2.Value;

        public static bool operator >(Meter2 m1, Meter2 m2)
          => m1.Value > m2.Value;

        public static bool operator <(Meter2 m1, Meter2 m2)
          => m1.Value > m2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Value == ((Meter2)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();

    }


}
