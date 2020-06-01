using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    public struct Weight
    {
        static readonly Regex gReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(g|kg|hg|dag|dg|cg|mg|oz|lb)$");

        public Weight(decimal value)
        {
            Value = value;
            OriginalUnit = "g";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Original unit of measure</param>
        public Weight(decimal value, string unit)
        {
            if (unit?.In("g", "kg", "hg", "dag", "dg", "cg", "mg", "oz", "lb") != true)
                throw new ArgumentException("unit mut be a valid gram unit", nameof(unit));
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "g"   => 1m,
                "kg"  => 1000m,
                "hg"  => 100m,
                "dag" => 10m,
                "dg"  => 0.1m,
                "cg"  => 0.01m,
                "mg"  => 0.001m,
                "oz"  => 28.34952m,
                "lb"  => 453.5924m,
                _     => 0m
            };
        }

        public static bool TryParse(string str, out Weight? w)
        {
            if (gReg.Match(str)?.Groups is GroupCollection g && g.Count > 2)
            {
                w = new Weight(decimal.Parse(g[1].Value), g[2].Value);
                return true;
            }
            w = null;
            return false;
        }

        public static Weight Parse(string str)
            => TryParse(str, out var gram) 
                    ? gram.Value 
                    : throw new FormatException($"{nameof(str)} has invalid format");

        #region [ operators ]

        public static implicit operator Weight(decimal d)
            => new Weight(d);

        public static explicit operator decimal (Weight w)
            => w.Value;

        public static explicit operator Weight (string g)
            => Parse(g);

        public static implicit operator string(Weight w)
            => w.ToString();

        public static Weight operator +(Weight w1, Weight w2)
            => new Weight(w1.Value + w2.Value);

        public static Weight operator -(Weight w1, Weight w2)
            => new Weight(w1.Value - w2.Value);

        public static Weight operator * (decimal scalar, Weight g)
            => new Weight(scalar * g.Value);

        public static Weight operator *(Weight g, decimal scalar)
            => new Weight(scalar * g.Value);

        public static Weight operator / (Weight g, decimal scalar)
            => new Weight(g.Value / scalar);

        public static decimal operator /(Weight g1, Weight g2)
            => g1.Value / g2.Value;

        public static Weight operator -(Weight g)
            => new Weight(-g.Value) { OriginalUnit = g.OriginalUnit };

        public static bool operator >(Weight g1, Weight g2)
            => g1.Value > g2.Value;

        public static bool operator <(Weight g1, Weight g2)
            => g1.Value < g2.Value;

        public static bool operator == (Weight g1, Weight g2)
            => g1.Value == g2.Value;

        public static bool operator !=(Weight g1, Weight g2)
            => g1.Value != g2.Value;


        #endregion [ operators ]

        #region [ Unit properties ]

        /// <summary>
        /// Value ​in grams
        /// </summary>
        public decimal g => Value;
        /// <summary>
        /// Value ​​in kilograms
        /// </summary>
        public decimal kg => Value * 0.001m;
        /// <summary>
        /// Value in hectograms
        /// </summary>
        public decimal hg => Value * 0.01m;
        /// <summary>
        /// Value in decagrams
        /// </summary>
        public decimal dag => Value * 0.1m;
        /// <summary>
        /// Value en decigrams
        /// </summary>
        public decimal dg => Value * 10m;
        /// <summary>
        /// Value in centigrams
        /// </summary>
        public decimal cg => Value * 100m;
        /// <summary>
        /// Malie in miligrams
        /// </summary>
        public decimal mg => Value * 1000m;
        /// <summary>
        /// Value in ounces
        /// </summary>
        public decimal oz => Value * 0.035274m;
        /// <summary>
        /// Value in pounds
        /// </summary>
        public decimal lb => Value * 0.00220462m;

        #endregion [ Unit properties ]

        public string OriginalUnit { get; private set; }
        internal decimal Value { get; }

        public override string ToString() 
            => OriginalUnit switch
               {
                   "g"   => $"{Value:0.####################}g",
                   "kg"  => $"{kg:0.####################}kg",
                   "hg"  => $"{hg:0.####################}hg",
                   "dag" => $"{dag:0.####################}dag",
                   "dg"  => $"{dg:0.####################}dg",
                   "cg"  => $"{cg:0.####################}cg",
                   "mg"  => $"{mg:0.####################}mg",
                   "oz"  => $"{oz:0.####################}oz",
                   "lb"  => $"{lb:0.####################}lb",
                   _ => null
               };

        public string ToString(string format)
            => OriginalUnit switch
            {
                "g"   => $"{Value.ToString(format)}g",
                "kg"  => $"{kg.ToString(format)}kg",
                "hg"  => $"{hg.ToString(format)}hg",
                "dag" => $"{dag.ToString(format)}dag",
                "dg"  => $"{dg.ToString(format)}dg",
                "cg"  => $"{cg.ToString(format)}cg",
                "mg"  => $"{mg.ToString(format)}mg",
                "oz"  => $"{oz.ToString(format)}oz",
                "lb"  => $"{lb.ToString(format)}lb",
                _ => null
            };

        public override bool Equals(object obj) 
            => Value == ((Weight)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
