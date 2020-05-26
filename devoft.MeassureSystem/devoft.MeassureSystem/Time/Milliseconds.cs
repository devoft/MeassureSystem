﻿using devoft.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Time
{
    public struct Milliseconds
    {
        static readonly Regex gReg = new Regex(@"((([0-9]+)(?:\s)*d)\:?)?((([0-9]+)(?:\s)*h)\:?)?((([0-9]+)(?:\s)*min)\:?)?((([0-9]+)(?:\s)*s)\:?)?((([0-9]+)(?:\s)*ms)\:?)?$");
        static readonly Regex partialReg = new Regex(@"([0-9]+)(?:\s)*(s|min|h|ms|d)$");

        /// <summary>
        /// Creates a new Milliseconds object according <paramref name="ms"/> value.
        /// </summary>
        /// <param name="ms"></param>
        public Milliseconds(int ms)
        {
            Value = ms;
            OriginalUnit = "ms";
        }

        /// <summary>
        /// Creates new Milliseconds with <paramref name="value"/> and <paramref name="unit"/>.
        /// The <paramref name="value"/> is related to the <paramref name="unit"/>
        /// Eg. new Milliseconds(4, "min");
        /// </summary>
        /// <param name="value">numeric value</param>
        /// <param name="unit">time unit</param>
        public Milliseconds(int value, string unit)
        {
            Value = value;
            if (unit?.In("h", "min", "s", "ms", "d") != true)
                throw new ArgumentException("unit mut be a valid gram unit", nameof(unit));
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "s"     => 1_000,
                "h"     => 3_600_000,
                "min"   => 60_000,
                "ms"    => 1,
                "d"     => 24 * 3_600_000,
                _       => 0
            };
        }

        /// <summary>
        /// The amount of seconds included in this time. Eg. 1500.ms().s == 1; 500.ms().s == 0
        /// </summary>
        public int s => Value / 1000;
        /// <summary>
        /// The amount of hours included in this time. Eg. 50.min().h == 0; 62.min().h == 1
        /// </summary>
        public int h => Value / 3_600_000;
        /// <summary>
        /// The amount of minutes included in this time. Eg. 50.s().min == 0; 62.s().min == 1
        /// </summary>
        public int min => Value / 60_000;
        /// <summary>
        /// The same as: <see cref="Value"/>
        /// </summary>
        public int ms => Value;
        /// <summary>
        /// The amount of days included in this time. Eg. 23.h().d == 0; 25.h().day == 1
        /// </summary>
        public int d => Value / (3_600_000 * 24);

        /// <summary>
        /// The unit used in the constructor to create the object
        /// </summary>
        public string OriginalUnit { get; }

        /// <summary>
        /// The amount of milliseconds corresponding with this time. 
        /// This property is always in milliseconds, even if the object was created with another unit.
        /// Eg. new Milliseconds(2, "s").Value == 2000
        /// </summary>
        public int Value { get; }

        #region [ operators ]

        /// <summary>
        /// Same as: <paramref name="seconds"/>.<see cref="Value"/>
        /// </summary>
        /// <param name="seconds"></param>
        public static explicit operator double(Milliseconds seconds)
            => seconds.Value;

        /// <summary>
        /// Same as: new Milliseconds(<paramref name="ms"/>)
        /// </summary>
        /// <param name="ms"></param>
        public static implicit operator Milliseconds(int ms)
            => new Milliseconds(ms);

        /// <summary>
        /// Creates a Milliseconds from <paramref name="time"/>.TotalMilliseconds
        /// </summary>
        /// <param name="time"></param>
        public static explicit operator Milliseconds (TimeSpan time) 
            => new Milliseconds(Convert.ToInt32(time.TotalMilliseconds));

        /// <summary>
        /// Creates a Timespan from <paramref name="seconds"/>.<see cref="Value"/>
        /// </summary>
        /// <param name="seconds"></param>
        public static implicit operator TimeSpan(Milliseconds seconds)
            => TimeSpan.FromMilliseconds(seconds.Value);

        /// <summary>
        /// Same as: Milliseconds.Parse(<paramref name="time"/>)
        /// </summary>
        /// <param name="time"></param>
        public static explicit operator Milliseconds(string time)
            => Parse(time);

        /// <summary>
        /// Applies <see cref="ToString"/>
        /// </summary>
        /// <param name="seconds"></param>
        public static implicit operator string(Milliseconds seconds)
            => seconds.ToString();

        /// <summary>
        /// Sums two milliseconds
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static Milliseconds operator +(Milliseconds ms1, Milliseconds ms2)
            => new Milliseconds(ms1.Value + ms2.Value);

        /// <summary>
        /// Substract <paramref name="ms2"/> from <paramref name="ms1"/>
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static Milliseconds operator -(Milliseconds ms1, Milliseconds ms2)
            => new Milliseconds(ms1.Value - ms2.Value);

        /// <summary>
        /// Scalar (prepend) product <paramref name="scalar"/> by <paramref name="ms"/>
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Milliseconds operator *(int scalar, Milliseconds ms)
            => new Milliseconds(scalar * ms.Value);

        /// <summary>
        /// Scalar (append) product <paramref name="ms"/> by <paramref name="scalar"/> 
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Milliseconds operator *(Milliseconds ms, int scalar)
            => new Milliseconds(scalar * ms.Value);

        /// <summary>
        /// Scalar division <paramref name="ms"/> by <paramref name="scalar"/>
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Milliseconds operator /(Milliseconds ms, int scalar)
            => new Milliseconds(ms.Value / scalar);

        /// <summary>
        /// Divide <paramref name="ms1"/> by <paramref name="ms2"/> resulting in a double value
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static double operator /(Milliseconds ms1, Milliseconds ms2)
            => ms1.Value / (double) ms2.Value;

        /// <summary>
        /// Unary minus operation that creates the opposite value of <paramref name="ms"/>
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Milliseconds operator -(Milliseconds ms)
            => new Milliseconds(-ms.Value, ms.OriginalUnit);

        /// <summary>
        /// Compares if <paramref name="ms1"/> is greater than <paramref name="ms2"/>
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator >(Milliseconds ms1, Milliseconds ms2)
            => ms1.Value > ms2.Value;

        /// <summary>
        /// Compares if <paramref name="ms1"/> is lesser than <paramref name="ms2"/>/// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator <(Milliseconds ms1, Milliseconds ms2)
            => ms1.Value < ms2.Value;

        /// <summary>
        /// Compares if <paramref name="ms1"/> is equals to <paramref name="ms2"/>/// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator ==(Milliseconds ms1, Milliseconds ms2)
            => ms1.Value == ms2.Value;

        /// <summary>
        /// Compares if <paramref name="ms1"/> is different from <paramref name="ms2"/>/// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator !=(Milliseconds ms1, Milliseconds ms2)
            => ms1.Value != ms2.Value;


        #endregion [ operators ]

        /// <summary>
        /// Converts the string representation of time to its Milliseconds value. 
        /// 4d<br/>2h<br/>2min<br/>3s<br/>5ms<br/>
        /// 2d:5h:15min:20s:100ms <br/>
        /// 2.5:15:20.100 (<see cref="TimeSpan.TryParse(string, out TimeSpan)"/>)
        /// </summary>
        /// <param name="time">string representation of type</param>
        /// <returns>A Milisenconds value</returns>
        public static Milliseconds Parse(string time) 
            => TryParse(time, out var value)
                  ? value.Value
                  : throw new FormatException($"Invalid time format {time}");

        /// <summary>
        /// Try to convert the string representation of time to its Milliseconds value; either in the following formats: <br/>
        /// 4d<br/>2h<br/>2min<br/>3s<br/>5ms<br/>
        /// 2d:5h:15min:20s:100ms <br/>
        /// 2.5:15:20.100 (<see cref="TimeSpan.TryParse(string, out TimeSpan)"/>)
        /// </summary>
        /// <param name="time">string representation of type</param>
        /// <param name="seconds">A Milisenconds value</param>
        /// <returns>Whether the parsing success</returns>
        public static bool TryParse(string str, out Milliseconds? seconds)
        {
            if (TimeSpan.TryParse(str, out var ts))
            {
                seconds = (Milliseconds) ts;
                return true;
            }
            if (gReg.Match(str)?.Value == str)
            {
                seconds = str.Split(":")
                             .Select(t => partialReg.Match(t).Groups)
                             .Select(g => new Milliseconds(int.Parse(g[1].Value), g[2].Value))
                             .Aggregate(0.ms(), (ac, t) => ac + t);
                return true;
            }
            seconds = null;
            return false;
        }

        public override bool Equals(object obj)
            => ((Milliseconds)obj).Value == Value;

        public override int GetHashCode()
            => Value.GetHashCode();

        /// <summary>
        /// String representation of the Milliseconds expressed using a conversion to 
        /// the <see cref="OriginalUnit"/> it was used in the constructor.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override string ToString()
            => OriginalUnit switch
            {
                "s"   => $"{Value}s",
                "h"   => $"{h}h",
                "min" => $"{min}min",
                "ms"  => $"{ms}ms",
                "d"   => $"{d}day",
                _     => null
            };

        /// <summary>
        /// Uses <see cref="TimeSpan.ToString(string)"/> method to create formatted 
        /// string representation of this Milliseconds object.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
            => ((TimeSpan)this).ToString(format);

        /// <summary>
        /// Deconstructs the object back to tuple: 
        /// (<paramref name="day"/>, <paramref name="hour"/>, <paramref name="minutes"/>, <paramref name="seconds"/>, <paramref name="milliseconds"/>)
        /// </summary>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="milliseconds"></param>
        public void Deconstruct(out int day, out int hour, out int minutes, out int seconds, out int milliseconds)
        {
            TimeSpan ts = this;
            (day, hour, minutes, seconds, milliseconds) = (ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
