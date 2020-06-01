using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    /// <summary>
    /// Represents Weight values. They can be parsed from string and converts them back to string in
    /// a usual weight representations.<br/>
    /// Eg. "34kg", "8.5oz", "9lb"<br/>
    /// Weight values have arithmetics and logic operators overloaded: +, -, *, /, >, <, == and !=
    /// to operates with them easier.
    /// </summary>
    public struct Weight
    {
        static readonly Regex gReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(g|kg|hg|dag|dg|cg|mg|oz|lb)$");

        /// <summary>
        /// Construct a new Weight value from a number (assumed in Grams)
        /// </summary>
        /// <param name="value"></param>
        public Weight(decimal value)
        {
            Value = value;
            OriginalUnit = "g";
        }

        /// <summary>
        /// Construct a new Weight from a number and a unit.
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

        /// <summary>
        /// Try to convert the string representation to Weight value.<br>
        /// Eg. "2kg", "5.01oz", "10lb", "12.5dag"
        /// </summary>
        /// <param name="str">string representation of weight</param>
        /// <param name="w">The resulting Weight value</param>
        /// <returns>Whether conversion succeded</returns>
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


        /// <summary>
        /// Convert the string representation to Weight value.<br>
        /// Eg. "2kg", "5.01oz", "10lb", "12.5dag"
        /// </summary>
        /// <param name="str">string representation of weight</param>
        /// <returns>The resulting Weight value</returns>
        /// <exception cref="FormatException">If <paramref name="str"/> is not in a correct format</exception>
        public static Weight Parse(string str)
            => TryParse(str, out var gram) 
                    ? gram.Value 
                    : throw new FormatException($"{nameof(str)} has invalid format");

        #region [ operators ]

        /// <summary>
        /// Implicitly converts decimals to Weight (as Gram);<br/>
        /// Eg. Weight w = 3.5m; // 3.5m.g()
        /// </summary>
        /// <param name="d">numeric value (assumed in grams)</param>
        public static implicit operator Weight(decimal d)
            => new Weight(d);

        /// <summary>
        /// Explicit convertion from Weight to decimal (the Gram value);<br/>
        /// Eg. decimal d = 3.kg(); // 3000m
        /// </summary>
        /// <param name="w">Weight value</param>
        public static explicit operator decimal (Weight w)
            => w.Value;

        /// <summary>
        /// Explictly converts string to Weight <br/>
        /// var w = (Weight) "3kg" is the same as: var w = Weight.Parse("3kg")
        /// </summary>
        /// <param name="s">string representation of Weight</param>
        public static explicit operator Weight (string s)
            => Parse(s);

        /// <summary>
        /// Implicitly converts Weight to string <br/>
        /// string w = 3.kg() is the same as: string w = 3.kg().ToString()
        /// </summary>
        /// <param name="s">Weight value</param>
        public static implicit operator string(Weight w)
            => w.ToString();

        /// <summary>
        /// Sums to Weights
        /// </summary>
        public static Weight operator +(Weight w1, Weight w2)
            => new Weight(w1.Value + w2.Value);

        /// <summary>
        /// Substract one Weight from another
        /// </summary>
        public static Weight operator -(Weight w1, Weight w2)
            => new Weight(w1.Value - w2.Value);

        /// <summary>
        /// Multiplies a number by a Weight, the result is the weight scaled <paramref name="scalar"/> times
        /// </summary>
        public static Weight operator * (decimal scalar, Weight g)
            => new Weight(scalar * g.Value);

        /// <summary>
        /// Multiplies a Weight by a number, the result is the weight scaled <paramref name="scalar"/> times
        /// </summary>
        public static Weight operator *(Weight g, decimal scalar)
            => new Weight(scalar * g.Value);

        /// <summary>
        /// Devides a Weight by a number, the result is the weight scaled 1/<paramref name="scalar"/> times
        /// </summary>
        public static Weight operator / (Weight w, decimal scalar)
            => new Weight(w.Value / scalar);

        /// <summary>
        /// Devides a Weight by another, the result is a number representing the relation between them.
        /// </summary>
        public static decimal operator /(Weight w1, Weight w2)
            => w1.Value / w2.Value;

        /// <summary>
        /// Unary opposite of the Weight value
        /// </summary>
        public static Weight operator -(Weight w)
            => new Weight(-w.Value) { OriginalUnit = w.OriginalUnit };

        /// <summary>
        /// Compares if first Weight is greater than the second
        /// </summary>
        public static bool operator >(Weight w1, Weight w2)
            => w1.Value > w2.Value;

        /// <summary>
        /// Compares if first Weight is lesser than the second
        /// </summary>
        public static bool operator <(Weight w1, Weight w2)
            => w1.Value < w2.Value;

        /// <summary>
        /// Compares if weights are equals
        /// </summary>
        public static bool operator == (Weight w1, Weight w2)
            => w1.Value == w2.Value;

        /// <summary>
        /// Compares if weights are differents
        /// </summary>
        public static bool operator !=(Weight w1, Weight w2)
            => w1.Value != w2.Value;


        #endregion [ operators ]

        #region [ Unit properties ]

        /// <summary>
        /// Value ​in grams
        /// </summary>
        public decimal Grams => Value;
        /// <summary>
        /// Value ​​in kilograms
        /// </summary>
        public decimal Kilograms => Value * 0.001m;
        /// <summary>
        /// Value in hectograms
        /// </summary>
        public decimal Hectograms => Value * 0.01m;
        /// <summary>
        /// Value in decagrams
        /// </summary>
        public decimal Decagrams => Value * 0.1m;
        /// <summary>
        /// Value en decigrams
        /// </summary>
        public decimal Decigrams => Value * 10m;
        /// <summary>
        /// Value in centigrams
        /// </summary>
        public decimal Centigrams => Value * 100m;
        /// <summary>
        /// Value in milligrams
        /// </summary>
        public decimal Milligrams => Value * 1000m;
        /// <summary>
        /// Value in ounces
        /// </summary>
        public decimal Ounces => Value * 0.035274m;
        /// <summary>
        /// Value in pounds
        /// </summary>
        public decimal Pounds => Value * 0.00220462m;

        #endregion [ Unit properties ]

        /// <summary>
        /// The original unit used to create this Weight value
        /// </summary>
        public string OriginalUnit { get; private set; }

        /// <summary>
        /// The Weight value always in Gram
        /// </summary>
        internal decimal Value { get; }

        /// <summary>
        /// String representation of Weight according with the <see cref="OriginalUnit"/>.<br/>
        /// Eg. 3.kg().ToString() == "3kg"
        /// </summary>
        /// <returns>string representation of Weight value</returns>
        public override string ToString() 
            => OriginalUnit switch
               {
                   "g"   => $"{Value:0.####################}g",
                   "kg"  => $"{Kilograms:0.####################}kg",
                   "hg"  => $"{Hectograms:0.####################}hg",
                   "dag" => $"{Decagrams:0.####################}dag",
                   "dg"  => $"{Decigrams:0.####################}dg",
                   "cg"  => $"{Centigrams:0.####################}cg",
                   "mg"  => $"{Milligrams:0.####################}mg",
                   "oz"  => $"{Ounces:0.####################}oz",
                   "lb"  => $"{Pounds:0.####################}lb",
                   _ => null
               };

        /// <summary>
        /// String representation of Weight according with the <see cref="OriginalUnit"/>, using <paramref name="format"/> for value.<br/>
        /// Eg. 16325.612.kg().ToString("0,0.00") == "16,325.61kg"
        /// </summary>
        /// <returns>string representation of Weight value</returns>
        public string ToString(string format)
            => OriginalUnit switch
            {
                "g"   => $"{Value.ToString(format)}g",
                "kg"  => $"{Kilograms.ToString(format)}kg",
                "hg"  => $"{Hectograms.ToString(format)}hg",
                "dag" => $"{Decagrams.ToString(format)}dag",
                "dg"  => $"{Decigrams.ToString(format)}dg",
                "cg"  => $"{Centigrams.ToString(format)}cg",
                "mg"  => $"{Milligrams.ToString(format)}mg",
                "oz"  => $"{Ounces.ToString(format)}oz",
                "lb"  => $"{Pounds.ToString(format)}lb",
                _ => null
            };

        public override bool Equals(object obj) 
            => Value == ((Weight)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
