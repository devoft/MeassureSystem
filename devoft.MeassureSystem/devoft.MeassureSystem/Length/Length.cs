using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    public struct Length
    {
        public static Regex mReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(mm|cm|dm|m|dam|hm|km|yd|in|ft)$");
        internal decimal Value { get; }
        public string OriginalUnit { get; private set; }

        #region Unit properties

        /// <summary>
        /// Value in millimeters
        /// </summary>
        public decimal Millimeter => Value * 1000;
        /// <summary>
        /// Value in centimeters
        /// </summary>
        public decimal Centimeter => Value * 100;
        /// <summary>
        /// Value in decimeters
        /// </summary>
        public decimal Decimeter => Value * 10;
        /// <summary>
        /// Value in meters
        /// </summary>
        public decimal Meter => Value ;
        /// <summary>
        ///  Value in decameters
        /// </summary>
        public decimal Decameter => Value / 10;
        /// <summary>
        /// Value in hectometers
        /// </summary>
        public decimal Hectometer => Value / 100;
        /// <summary>
        /// Value in kilometers
        /// </summary>
        public decimal Kilometer => Value / 1000;
        /// <summary>
        /// Value in yards
        /// </summary>
        public decimal Yard => Value * 1.093613m;
        /// <summary>
        /// Value in inches
        /// </summary>
        public decimal Inch => Value * 39.37008m;
        /// <summary>
        /// Value in feet
        /// </summary>
        public decimal Feet => Value * 3.28084m;

        #endregion Unit properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public Length(decimal value)
        {
            Value = value;
            OriginalUnit = "m";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Original unit of measure</param>
        public Length(decimal value, string unit)
        {
            if (unit?.In("mm", "cm", "dm", "m", "dam", "hm", "km", "yd", "in","ft") != true)
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
                                _       => 0m
                            };
        }

        public static Length Parse(string s)
            => TryParse(s, out var m)
                    ? m.Value
                    : throw new FormatException($"Invalid format");

        public static bool TryParse(string s, out Length? m)
        {
            if (mReg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                m = new Length(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            m = null;
            return false;
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
                                  _         => 0m
                              };
            
            return $"{val:0.####################}{OriginalUnit}";
        }

        public string ToString(string format)
          => OriginalUnit switch
            {
                "m" => $"{Value.ToString(format)}m",
                "mm" => $"{Millimeter.ToString(format)}mm",
                "cm" => $"{Centimeter.ToString(format)}cm",
                "dm" => $"{Decimeter.ToString(format)}dm",
                "dam" => $"{Decameter.ToString(format)}dam",
                "hm" => $"{Hectometer.ToString(format)}hm",
                "km" => $"{Kilometer.ToString(format)}km",
                "yd" => $"{Yard.ToString(format)}yd",
                "in" => $"{Inch.ToString(format)}in",
                "ft" => $"{Feet.ToString(format)}ft",
                _ => null
            };
        
        public Length mm() => new Length(Value) { OriginalUnit = "mm" };
        public Length cm() => new Length(Value) { OriginalUnit = "cm" };
        public Length dm() => new Length(Value) { OriginalUnit = "dm" };
        public Length m() => new Length(Value) { OriginalUnit = "m" };
        public Length dam() => new Length(Value) { OriginalUnit = "dam" };
        public Length hm() => new Length(Value) { OriginalUnit = "hm" };
        public Length km() => new Length(Value) { OriginalUnit = "km" };
        public Length yd() => new Length(Value) { OriginalUnit = "yd" };
        public Length inch() => new Length(Value) { OriginalUnit = "in" };
        public Length ft() => new Length(Value) { OriginalUnit = "ft" };

        #region Operators

        public static implicit operator Length(decimal d) => new Length(d);
        
        public static explicit operator decimal (Length m) => m.Value;

        public static explicit operator Length(string s) => Parse(s);

        public static implicit operator string(Length m) => m.ToString();

        public static Length operator + (Length m1, Length m2)
            => new Length(m1.Value + m2.Value);
        
        public static Length operator -(Length m1, Length m2)
            => new Length(m1.Value - m2.Value);

        public static Length operator *(Length m, decimal d)
           => new Length(m.Value * d);
        public static Length operator *(decimal d, Length m)
           => new Length(m.Value * d);
        public static Area operator *(Length m1, Length m2)
           => new Area(m1.Value * m2.Value);

        public static decimal operator /(Length m1, Length m2)
           => m1.Value / m2.Value;
        public static Length operator /(Length m, decimal d)
          => new Length(m.Value / d);
        public static Length operator -(Length g)
            => new Length(-g.Value) { OriginalUnit = g.OriginalUnit };
        public static bool operator == (Length m1, Length m2)
          => m1.Value == m2.Value;
        public static bool operator !=(Length m1, Length m2)
          => m1.Value != m2.Value;
        public static bool operator > (Length m1, Length m2)
          => m1.Value > m2.Value;
        public static bool operator < (Length m1, Length m2)
          => m1.Value < m2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Value == ((Length)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
