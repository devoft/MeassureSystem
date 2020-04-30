using devoft.MeassureSystem.Surface;
using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Length
{
    public struct Meter
    {
        public static Regex mReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(mm|cm|dm|m|dam|hm|km|yd|in|ft|px)$");

        public decimal Value { get; }
        public string OriginalUnit { get; private set; }

        #region Unit properties

        /// <summary>
        /// Value in milimeters
        /// </summary>
        public decimal Mm => Value * 1000;
        /// <summary>
        /// Value in centimeters
        /// </summary>
        public decimal Cm => Value * 100;
        /// <summary>
        /// Value in decimeters
        /// </summary>
        public decimal Dm => Value * 10;
        /// <summary>
        /// Value in meter
        /// </summary>
        public decimal M => Value ;
        /// <summary>
        ///  Value in decameter
        /// </summary>
        public decimal Dam => Value / 10;
        /// <summary>
        /// Value in hectometer
        /// </summary>
        public decimal Hm => Value / 100;
        /// <summary>
        /// Value in kilometers
        /// </summary>
        public decimal Km => Value / 1000;
        /// <summary>
        /// Value in yards
        /// </summary>
        public decimal Yd => Value * 1.093613m;
        /// <summary>
        /// Value in inches
        /// </summary>
        public decimal Inch => Value * 39.37008m;
        /// <summary>
        /// Value in feet
        /// </summary>
        public decimal Ft => Value * 3.28084m;
        /// <summary>
        /// Value in pixel
        /// </summary>
        public decimal Px => Value * 3779.57517575m;

        #endregion Unit properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public Meter(decimal value)
        {
            Value = value;
            OriginalUnit = "m";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Original unit of measure</param>
        public Meter(decimal value, string unit)
        {
            if (unit?.In("mm", "cm", "dm", "m", "dam", "hm", "km", "yd", "in","ft", "px") != true)
                throw new ArgumentException($"unit mut be a valid meter unit");
            OriginalUnit = unit;
            Value = value * unit switch
                            {
                                "mm"    => 0.001m,
                                "cm"    => 0.01m,
                                "dm"    => 0.1m,
                                "m"     => 1m,
                                "dam"   => 10m,
                                "hm"    => 100m,
                                "km"    => 1000m,
                                "yd"    => 0.9144m,//1.093613m,
                                "in"    => 0.0254m, //39.37008m,
                                "ft"    => 0.3048m,//3.28084m,
                                "px"    => 0.00026458m,
                                _       => 0m
                            };
        }

        public override string ToString()
        {
            var val = Value * OriginalUnit switch
                              {
                                  "mm"      => 1000m,
                                  "cm"      => 100m,
                                  "dm"      => 10m,
                                  "m"       => 1m,
                                  "dam"     => 0.1m,
                                  "hm"      => 0.01m,
                                  "km"      => 0.001m,
                                  "yd"      => 1.093613m,
                                  "in"      => 39.37008m,
                                  "ft"      => 3.28084m,
                                  "px"      => 3779.57517575m,
                                  _         => 0m
                              };
            return $"{val:0.####################}{OriginalUnit}";
        }


        public Meter mm() => new Meter(Value) { OriginalUnit = "mm" };
        public Meter cm() => new Meter(Value) { OriginalUnit = "cm" };
        public Meter dm() => new Meter(Value) { OriginalUnit = "dm" };
        public Meter m() => new Meter(Value) { OriginalUnit = "m" };
        public Meter dam() => new Meter(Value) { OriginalUnit = "dam" };
        public Meter hm() => new Meter(Value) { OriginalUnit = "hm" };
        public Meter km() => new Meter(Value) { OriginalUnit = "km" };
        public Meter yd() => new Meter(Value) { OriginalUnit = "yd" };
        public Meter inch() => new Meter(Value) { OriginalUnit = "in" };
        public Meter ft() => new Meter(Value) { OriginalUnit = "ft" };
        public Meter px => new Meter(Value) { OriginalUnit = "px" };

        #region Operators

        public static implicit operator Meter(decimal d) => new Meter(d);
        
        public static explicit operator decimal (Meter m) => m.Value;

        public static explicit operator Meter(string s) => Parse(s);


        public static Meter Parse(string s)
            => TryParse(s, out var m) 
                    ? m.Value
                    : throw new FormatException($"Invalid format");

        public static bool TryParse(string s, out Meter? m)
        {
            if (mReg.Match(s)?.Groups is GroupCollection gc && gc.Count >2)
            {
                m = new Meter(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            m = null;
            return false;
        }

        public static Meter operator + (Meter m1, Meter m2)
            => new Meter(m1.Value + m2.Value);
        
        public static Meter operator -(Meter m1, Meter m2)
            => new Meter(m1.Value - m2.Value);

        public static Meter operator *(Meter m, decimal d)
           => new Meter(m.Value * d);
        public static Meter operator *(decimal d, Meter m)
           => new Meter(m.Value * d);
        public static Meter2 operator *(Meter m1, Meter m2)
           => new Meter2(m1.Value * m2.Value);

        public static decimal operator /(Meter m1, Meter m2)
           => m1.Value / m2.Value;
        public static Meter operator /(Meter m, decimal d)
          => new Meter(m.Value / d);

        public static bool operator == (Meter m1, Meter m2)
          => m1.Value == m2.Value;
        public static bool operator !=(Meter m1, Meter m2)
          => m1.Value != m2.Value;
        public static bool operator > (Meter m1, Meter m2)
          => m1.Value > m2.Value;
        public static bool operator < (Meter m1, Meter m2)
          => m1.Value > m2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Value == ((Meter)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
