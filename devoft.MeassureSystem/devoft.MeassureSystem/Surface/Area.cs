using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    /// <summary>
    /// Represents meassure unit values like square cm, square meter, square km, etc.
    /// </summary>
    /// <seealso cref="Length"/>
    [TypeConverter (typeof(AreaConverter))]
    public struct Area : IComparable<Area>, IEquatable<Area>
    {
        public static Regex m2Reg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(mm2|cm2|dm2|m2|dam2|hm2|km2)$");

        internal decimal Value { get; }
        public string OriginalUnit { get; private set; }

        #region Unit properties

        /// <summary>
        /// Value in square milimeters
        /// </summary>
        public decimal Millimeter2 => Value * 1000000;
        /// <summary>
        /// Value in square centimeters
        /// </summary>
        public decimal Centimeter2 => Value * 10000;
        /// <summary>
        /// Value in square decimeters
        /// </summary>
        public decimal Decimeter2 => Value * 100;
        /// <summary>
        /// Value in square meter
        /// </summary>
        public decimal Meter2 => Value;
        /// <summary>
        ///  Value in square decameter
        /// </summary>
        public decimal Decameter2 => Value / 100;
        /// <summary>
        /// Value in square hectometer
        /// </summary>
        public decimal Hectometer2 => Value / 10000;
        /// <summary>
        /// Value in square kilometers
        /// </summary>
        public decimal Kilometer2 => Value / 1000000;

        #endregion Unit properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public Area(decimal value)
        {
            Value = value;
            OriginalUnit = "m2";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Original unit of measure</param>
        public Area(decimal value, string unit)
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
                                     "mm2"  => 1000000m,
                                     "cm2"  => 10000m,
                                     "dm2"  => 100m,
                                     "m2"   => 1m,
                                     "dam2" => 0.01m,
                                     "hm2"  => 0.0001m,
                                     "km2"  => 0.000001m,
                                     _      => 0m
                                 };
            return $"{v:0.####################}{OriginalUnit}";
        }

        public Area mm2()   => new Area(Value) { OriginalUnit = "mm" };
        public Area cm2()   => new Area(Value) { OriginalUnit = "cm" };
        public Area dm2()   => new Area(Value) { OriginalUnit = "dm" };
        public Area m2()    => new Area(Value) { OriginalUnit = "m" };
        public Area dam2()  => new Area(Value) { OriginalUnit = "dam" };
        public Area hm2()   => new Area(Value) { OriginalUnit = "hm" };
        public Area km2()   => new Area(Value) { OriginalUnit = "km" };

        #region Operators

        public static implicit operator Area(decimal d)
            => new Area(d);

        public static explicit operator decimal(Area m)
            => m.Value;

        public static explicit operator Area(string s)
            => Parse(s);

        public static Area Parse(string s)
            => TryParse(s, out var m)
                    ? m.Value
                    : throw new FormatException($"Invalid format");

        public static bool TryParse(string s, out Area? area)
        {
            if (m2Reg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                area = new Area(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            area = null;
            return false;
        }

        public static Area operator + (Area a1, Area a2)
           => new Area(a1.Value + a2.Value);

        public static Area operator - (Area a1, Area a2)
            => new Area(a1.Value - a2.Value);
        public static Area operator *(decimal d, Area a2)
            => new Area(a2.Value * d);
        public static Area operator *(Area a2, decimal d)
            => new Area(a2.Value * d);
        public static Volume operator *(Area a1, Length a2)
            => new Volume(a1.Value * a2.Value);
        public static Volume operator *(Length a1, Area a2)
            => new Volume(a1.Value * a2.Value);
        public static decimal operator /(Area a1, Area a2)
            => a1.Value / a2.Value;
        public static Area operator /(Area a, decimal d)
            => new Area(a.Value / d);
        public static Length operator /(Area a2, Length l)
            => new Length(a2.Value / l.Value);
        public static bool operator ==(Area a1, Area a2)
            => a1.Value == a2.Value;
        public static bool operator !=(Area a1, Area a2)
            => a1.Value != a2.Value;
        public static bool operator >(Area a1, Area a2)
            => a1.Value > a2.Value;
        public static bool operator <(Area a1, Area a2)
            => a1.Value > a2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Equals((Area)obj);
        public override int GetHashCode()
            => Value.GetHashCode();

        public int CompareTo(Area other)
        {
            return Math.Sign(Value - other.Value);
        }

        public bool Equals(Area other) 
            => Value == other.Value;
    }
}
