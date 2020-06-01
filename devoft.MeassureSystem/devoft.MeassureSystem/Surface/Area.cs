﻿using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    public struct Area
    {
        public static Regex m2Reg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(mm2|cm2|dm2|m2|dam2|hm2|km2)$");

        internal decimal Value { get; }
        public string OriginalUnit { get; private set; }

        #region Unit properties

        /// <summary>
        /// Value in square milimeters
        /// </summary>
        public decimal Mm2 => Value * 1000000;
        /// <summary>
        /// Value in square centimeters
        /// </summary>
        public decimal Cm2 => Value * 10000;
        /// <summary>
        /// Value in square decimeters
        /// </summary>
        public decimal Dm2 => Value * 100;
        /// <summary>
        /// Value in square meter
        /// </summary>
        public decimal M2 => Value;
        /// <summary>
        ///  Value in square decameter
        /// </summary>
        public decimal Dam2 => Value / 100;
        /// <summary>
        /// Value in square hectometer
        /// </summary>
        public decimal Hm2 => Value / 10000;
        /// <summary>
        /// Value in square kilometers
        /// </summary>
        public decimal Km2 => Value / 1000000;

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

        public Area mm2() => new Area(Value) { OriginalUnit = "mm" };
        public Area cm2() => new Area(Value) { OriginalUnit = "cm" };
        public Area dm2() => new Area(Value) { OriginalUnit = "dm" };
        public Area m2() => new Area(Value) { OriginalUnit = "m" };
        public Area dam2() => new Area(Value) { OriginalUnit = "dam" };
        public Area hm2() => new Area(Value) { OriginalUnit = "hm" };
        public Area km2() => new Area(Value) { OriginalUnit = "km" };

        #region Operators

        public static implicit operator Area(decimal d)
            => new Area(d);

        public static explicit operator decimal(Area m)
            => m.Value;

        public static explicit operator Area(string s)
            => Parse(s);

        public static Area Parse(string s)
        {
            if (TryParse(s, out var m))
                return m.Value;
            else
                throw new FormatException($"Invalid format");
        }

        public static bool TryParse(string s, out Area? m)
        {
            if (m2Reg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                m = new Area(decimal.Parse(gc[1].Value), gc[2].Value);
                return true;
            }
            m = null;
            return false;
        }

        public static Area operator + (Area m1, Area m2)
           => new Area(m1.Value + m2.Value);

        public static Area operator - (Area m1, Area m2)
            => new Area(m1.Value - m2.Value);

        public static Area operator *(decimal d, Area m2)
          => new Area(m2.Value * d);
        public static Area operator *(Area m2, decimal d)
          => new Area(m2.Value * d);
        public static Volume operator *(Area m1, Length m2)
          => new Volume(m1.Value * m2.Value);
        public static Volume operator *(Length m1, Area m2)
          => new Volume(m1.Value * m2.Value);

        public static decimal operator /(Area m1, Area m2)
          => m1.Value / m2.Value;
        public static Area operator /(Area m, decimal d)
          => new Area(m.Value / d);
        public static Length operator /(Area m2, Length m)
          => new Length(m2.Value / m.Value);

        public static bool operator ==(Area m1, Area m2)
          => m1.Value == m2.Value;

        public static bool operator !=(Area m1, Area m2)
          => m1.Value != m2.Value;

        public static bool operator >(Area m1, Area m2)
          => m1.Value > m2.Value;

        public static bool operator <(Area m1, Area m2)
          => m1.Value > m2.Value;

        #endregion Operators

        public override bool Equals(object obj)
            => Value == ((Area)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}