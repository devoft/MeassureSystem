using devoft.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Weight
{
    public struct Gram
    {
        static readonly Regex gReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(g|kg|hg|dag|dg|cg|mg|oz|lb)$");

        public Gram(decimal value)
        {
            Value = value;
            OriginalUnit = "g";
        }

        public Gram(decimal value, string unit)
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

        public static bool TryParse(string str, out Gram? gram)
        {
            if (gReg.Match(str)?.Groups is GroupCollection g && g.Count > 2)
            {
                gram = new Gram(decimal.Parse(g[1].Value), g[2].Value);
                return true;
            }
            gram = null;
            return false;
        }

        public static Gram Parse(string str)
            => TryParse(str, out var gram) 
                    ? gram.Value 
                    : throw new FormatException($"{nameof(str)} has invalid format");

        #region [ operators ]

        public static implicit operator Gram(decimal d)
            => new Gram(d);

        public static explicit operator decimal (Gram g)
            => g.Value;

        public static explicit operator Gram (string g)
            => Parse(g);

        public static implicit operator string(Gram g)
            => g.ToString();

        public static Gram operator +(Gram g1, Gram g2)
            => new Gram(g1.Value + g2.Value);

        public static Gram operator -(Gram g1, Gram g2)
            => new Gram(g1.Value - g2.Value);

        public static Gram operator * (decimal scalar, Gram g)
            => new Gram(scalar * g.Value);

        public static Gram operator *(Gram g, decimal scalar)
            => new Gram(scalar * g.Value);

        public static Gram operator / (Gram g, decimal scalar)
            => new Gram(g.Value / scalar);

        public static decimal operator /(Gram g1, Gram g2)
            => g1.Value / g2.Value;

        public static Gram operator -(Gram g)
            => new Gram(-g.Value, g.OriginalUnit);

        public static bool operator >(Gram g1, Gram g2)
            => g1.Value > g2.Value;

        public static bool operator <(Gram g1, Gram g2)
            => g1.Value < g2.Value;

        public static bool operator == (Gram g1, Gram g2)
            => g1.Value == g2.Value;

        public static bool operator !=(Gram g1, Gram g2)
            => g1.Value != g2.Value;


        #endregion [ operators ]

        #region [ Unit properties ]

        public decimal g => Value;
        public decimal kg => Value * 0.001m;
        public decimal hg => Value * 0.01m;
        public decimal dag => Value * 0.1m;
        public decimal dg => Value * 10m;
        public decimal cg => Value * 100m;
        public decimal mg => Value * 1000m;
        public decimal oz => Value * 0.035274m;
        public decimal lb => Value * 0.00220462m;

        #endregion [ Unit properties ]

        public string OriginalUnit { get; }
        public decimal Value { get; }

        public override string ToString() 
            => OriginalUnit switch
               {
                   "g"   => $"{Value}g",
                   "kg"  => $"{kg}kg",
                   "hg"  => $"{hg}hg",
                   "dag" => $"{dag}dag",
                   "dg"  => $"{dg}dg",
                   "cg"  => $"{cg}cg",
                   "mg"  => $"{mg}mg",
                   "oz"  => $"{oz}oz",
                   "lb"  => $"{lb}lb",
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
            => Value == ((Gram)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
