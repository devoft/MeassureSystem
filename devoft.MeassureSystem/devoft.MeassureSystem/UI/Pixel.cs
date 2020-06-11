using devoft.MeassureSystem;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    [TypeConverter(typeof(PixelConverter)) ]
    public struct Pixel : IComparable<Pixel>, IEquatable<Pixel>
    {
        public static Regex mReg = new Regex(@"([0-9]+)(?:\s)*(px)$");
        public int Value { get; }
        
        #region Unit properties

        /// <summary>
        /// Value in pixel
        /// </summary>
        public int Px => Value;
        /// <summary>
        /// Value in millimeter
        /// </summary>
        public decimal Mm => (Value * 3.779528m);

        #endregion

        public Pixel(int number)
        {
            Value = number;
        }

        public Pixel px() => this;

        public override string ToString() 
            => $"{Value}px";

        #region Operators

        public static implicit operator Pixel(int d) => new Pixel(d);

        public static explicit operator int(Pixel m) => m.Value;

        public static explicit operator Pixel(string s) => Parse(s);

        public static Pixel Parse(string s)
            =>    TryParse(s, out var m)
                    ? m.Value
                    : throw new FormatException($"Invalid format");

        public static bool TryParse(string s, out Pixel? m)
        {
            if (mReg.Match(s)?.Groups is GroupCollection gc && gc.Count > 2)
            {
                m = new Pixel(int.Parse(gc[1].Value));
                return true;
            }
            m = null;
            return false;
        }

        public static implicit operator Pixel(Length m)
            => new Pixel(Convert.ToInt32(m.Inch * 96));

        public static explicit operator Length(Pixel p)
            => (p.Value / 96).inch();

        public static Pixel operator +(Pixel p1, Pixel p2)
            => new Pixel(p1.Value + p2.Value);

        public static Pixel operator -(Pixel p1, Pixel p2)
            => new Pixel(p1.Value - p2.Value);

        public static Pixel operator *(Pixel p, decimal d)
            => new Pixel(p.Value * (int)d);
        public static Pixel operator *(decimal d, Pixel p)
            => new Pixel(p.Value * (int)d);

        public static decimal operator /(Pixel p1, Pixel p2)
            => p1.Value / p2.Value;
        public static Pixel operator /(Pixel p, decimal d)
            => new Pixel(p.Value / (int)d);

        public static bool operator ==(Pixel p1, Pixel p2)
            => p1.Value == p2.Value;
        public static bool operator !=(Pixel p1, Pixel p2)
            => p1.Value != p2.Value;
        public static bool operator >(Pixel p1, Pixel p2)
            => p1.Value > p2.Value;
        public static bool operator <(Pixel p1, Pixel p2)
            => p1.Value > p2.Value;

        public override bool Equals(object obj)
            => Equals((Pixel)obj);

        public override int GetHashCode()
            => Value.GetHashCode();

        public int CompareTo(Pixel other)
            => Math.Sign(Px - other.Px);

        public bool Equals(Pixel other)
            => Value == other.Value;

        #endregion Operators
    }
}
