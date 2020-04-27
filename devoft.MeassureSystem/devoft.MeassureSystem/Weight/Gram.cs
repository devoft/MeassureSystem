using devoft.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Weight
{
    public struct Gram
    {
        static readonly Regex gReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(g|kg|dg|cg|mg)$");

        public Gram(decimal value)
        {
            Value = value;
            OriginalUnit = "g";
        }

        public Gram(decimal value, string unit)
        {
            if (unit?.In("g", "kg", "dg", "cg", "mg") != true)
                throw new ArgumentException("unit mut be a valid gram unit", nameof(unit));
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "g"  => 1m,
                "kg" => 1000m,
                "dg" => 0.1m,
                "cg" => 0.01m,
                "mg" => 0.001m,
                _    => 0m
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
            => g1.Value > g2.Value;

        public static bool operator == (Gram g1, Gram g2)
            => g1.Value == g2.Value;

        public static bool operator !=(Gram g1, Gram g2)
            => g1.Value != g2.Value;


        #endregion [ operators ]

        #region [ Unit properties ]

        public decimal kg => Value * 0.001m;
        public decimal dg => Value * 10m;
        public decimal cg => Value * 100m;
        public decimal mg => Value * 1000m;

        #endregion [ Unit properties ]

        public string OriginalUnit { get; }
        public decimal Value { get; }

        public override string ToString() 
            => OriginalUnit switch
               {
                   "g"  => $"{Value}g",
                   "kg" => $"{Value * 0.001m}kg",
                   "dg" => $"{Value * 10m}dg",
                   "cg" => $"{Value * 100m}cg",
                   "mg" => $"{Value * 1000m}mg",
                   _    => null
               };

        public string ToString(string format)
            => OriginalUnit switch
            {
                "g"  => $"{Value.ToString(format)}g",
                "kg" => $"{(Value * 0.001m).ToString(format)}kg",
                "dg" => $"{(Value * 10m).ToString(format)}dg",
                "cg" => $"{(Value * 100m).ToString(format)}cg",
                "mg" => $"{(Value * 1000m).ToString(format)}mg",
                _    => null
            };
    }
}
