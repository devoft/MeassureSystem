using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{

    /// <summary>
    /// Represents meassure unit values like cubic centimeter, cubic meter, cubic kilometer, etc.
    /// </summary>
    [TypeConverter (typeof(VolumeConverter))]
    public struct Volume : IComparable<Volume>, IEquatable<Volume>
    {
        public static Regex m2Reg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(mm3|cm3|dm3|m3|dam3|hm3|km3|ml|l)$");

        internal decimal Value { get; }
        public string OriginalUnit { get; private set; }

        #region Unit properties

        /// <summary>
        /// Value in cubic millimeters
        /// </summary>
        public decimal Millimeter3 => Value * 1000000000;
        /// <summary>
        /// Value in cubic centimeters
        /// </summary>
        public decimal Centimeter3 => Value * 1000000;
        /// <summary>
        /// Value in cubic decimeters
        /// </summary>
        public decimal Decimeter3 => Value * 1000;
        /// <summary>
        /// Value in cubic meter
        /// </summary>
        public decimal Meter3 => Value;
        /// <summary>
        ///  Value in cubic decameter
        /// </summary>
        public decimal Decameter3 => Value / 1000;
        /// <summary>
        /// Value in cubic hectometer
        /// </summary>
        public decimal Hectometer3 => Value / 1000000;
        /// <summary>
        /// Value in cubic kilometers
        /// </summary>
        public decimal Kilometer3 => Value / 1000000000;
        /// <summary>
        /// Value in Milliliter
        /// </summary>
        public decimal Milliliter => Value * 1000000;
        /// <summary>
        /// Value in Liter
        /// </summary>
        public decimal Liter => Value * 1000;
     
        #endregion Unit properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public Volume(decimal value)
        {
            Value = value;
            OriginalUnit = "m3";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Original unit of measure</param>
        public Volume(decimal value, string unit)
        {
            if (unit?.In("mm3", "cm3", "dm3", "m3", "dam3", "hm3", "km3", "ml", "l") != true)
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
                                "ml"    => 0.000001m,
                                "l"     => 0.001m,
                                _       => 0m
                            };
        }

        public override string ToString()
        {
            var v = this.Value * OriginalUnit switch
                                 {
                                     "mm3"   => 1000000000m,
                                     "cm3"   => 1000000m,
                                     "dm3"   => 1000m,
                                     "m3"    => 1m,
                                     "dam3"  => 0.001m,
                                     "hm3"   => 0.000001m,
                                     "km3"   => 0.000000001m,
                                     "ml"    => 1000000m,
                                     "l"     => 1000m,
                                     _       => 0m
                                 };
            return $"{v:0.####################}{OriginalUnit}";
        }

        public Volume cm3()  => new Volume(Value) { OriginalUnit = "cm3" };
        public Volume dm3()  => new Volume(Value) { OriginalUnit = "dm3" };
        public Volume mm3()  => new Volume(Value) { OriginalUnit = "mm3" };
        public Volume m3()   => new Volume(Value) { OriginalUnit = "m3" };
        public Volume dam3() => new Volume(Value) { OriginalUnit = "dam3" };
        public Volume hm3()  => new Volume(Value) { OriginalUnit = "hm3" };
        public Volume km3()  => new Volume(Value) { OriginalUnit = "km3" };
        public Volume ml()   => new Volume(Value) { OriginalUnit = "ml" };
        public Volume l()    => new Volume(Value) { OriginalUnit = "l" };

        #region Operators

        public static implicit operator Volume(decimal d)
            => new Volume(d);

        public static explicit operator decimal(Volume v)
            => v.Value;

        public static explicit operator Volume(string s)
            => Parse(s);

        public static Volume Parse(string s)
            => TryParse(s, out var v)
                    ? v.Value
                    : throw new FormatException($"Invalid format");

        public static bool TryParse(string s, out Volume? v)
        {
            if (m2Reg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                v = new Volume(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            v = null;
            return false;
        }

        public static Volume operator +(Volume v1, Volume v2)
           => new Volume(v1.Value + v2.Value);

        public static Volume operator -(Volume v1, Volume v2)
            => new Volume(v1.Value - v2.Value);
        public static Volume operator *(decimal d, Volume v)
            => new Volume(v.Value * d);
        public static Volume operator *(Volume v, decimal d)
            => new Volume(v.Value * d);
        public static decimal operator /(Volume v1, Volume v2)
            => v1.Value / v2.Value;
        public static Volume operator /(Volume v, decimal d)
            => new Volume(v.Value / d);
        public static Area operator /(Volume v2, Length l)
            => new Area(v2.Value / l.Value);
        public static Length operator /(Volume v2, Area area)
            => new Length(v2.Value / area.Value);
        public static Volume operator -(Volume v)
           => new Volume(-v.Meter3) { OriginalUnit = v.OriginalUnit };
        public static bool operator ==(Volume v1, Volume v2)
            => v1.Value == v2.Value;
        public static bool operator !=(Volume v1, Volume v2)
            => v1.Value != v2.Value;
        public static bool operator >(Volume v1, Volume v2)
            => v1.Value > v2.Value;
        public static bool operator <(Volume v1, Volume v2)
            => v1.Value > v2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Equals((Volume)obj);
        public override int GetHashCode()
            => Value.GetHashCode();
        public int CompareTo(Volume other) 
            => Math.Sign(Meter3 - other.Value);
        public bool Equals(Volume other) 
            => Value == other.Value;
    }
}
