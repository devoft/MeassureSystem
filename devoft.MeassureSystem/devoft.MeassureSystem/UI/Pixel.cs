using devoft.MeassureSystem;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.UI
{
    public struct Pixel
    {
        public static Regex mReg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(px)$");
        public int Value { get; }
        public string OriginalUnit { get; private set; }

        /// <summary>
        /// Value in pixel
        /// </summary>
        public int Px => Value;

        /// <summary>
        /// Value in millimemeter
        /// </summary>
        public decimal Mm => (Value * 3.779528m);

        public Pixel(int number)
        {
            Value = number;
            OriginalUnit = "px";
        }

        public Pixel(int number, string unit)
        {
            Value = number;
            OriginalUnit = unit;
        }

        public Pixel px() => new Pixel(Value) { OriginalUnit = "px" };

        public override string ToString()
        {
            var val = Value * OriginalUnit switch
                              {
                                  "px" => 1u,
                                  "mm" => 3.779528m,
                                  _ => 0m
                              };
            return $"{val:0.####################}{OriginalUnit}";
        }

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
                m = new Pixel(int.Parse(gc[1].Value), gc[2].Value);
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

        #endregion Operators

        public override bool Equals(object obj)
            => Value == ((Pixel)obj).Value;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
