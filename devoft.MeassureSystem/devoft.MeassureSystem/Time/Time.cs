using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem
{
    /// <summary>
    /// Representation of Time <br/>
    /// e.g. Time time = 2.d() + 12.h() + 90.s();
    /// </summary>
    /// <example>
    /// <code>Time time = 4.min();</code>
    /// <code>var (d,h,m,s,ms) = 2.d() + 12.h() + 90.s();</code>
    /// </example>
    /// <seealso cref="TimeSpan"/>
    [TypeConverter(typeof(TimeConverter))]
    public struct Time : IComparable<Time>, IComparable, IEquatable<Time>, IFormattable
    {
        static readonly Regex gReg = new Regex(@"((([0-9]+)(?:\s)*d)\:?)?((([0-9]+)(?:\s)*h)\:?)?((([0-9]+)(?:\s)*min)\:?)?((([0-9]+)(?:\s)*s)\:?)?((([0-9]+)(?:\s)*ms)\:?)?$");
        static readonly Regex partialReg = new Regex(@"([0-9]+)(?:\s)*(s|min|h|ms|d)$");

        /// <summary>
        /// The amount of Milliseconds corresponding with this time. 
        /// This property is always in milliseconds, even if the object was created with another unit.
        /// Eg. new Time(2, "s").Value == 2000
        /// </summary>
        internal int Value { get; }

        /// <summary>
        /// The unit used in the constructor to create the object
        /// </summary>
        public string OriginalUnit { get; }

        /// <summary>
        /// Creates a new Time object according <paramref name="ms"/> value.
        /// </summary>
        /// <param name="ms"></param>
        public Time(int ms)
        {
            Value = ms;
            OriginalUnit = "ms";
        }

        /// <summary>
        /// Creates new Time with <paramref name="value"/> and <paramref name="unit"/>.
        /// The <paramref name="value"/> is related to the <paramref name="unit"/>
        /// Eg. new Time(4, "min");
        /// </summary>
        /// <param name="value">numeric value</param>
        /// <param name="unit">time unit</param>
        public Time(int value, string unit)
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
        /// Creates a new Time from TimeSpan <paramref name="timeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The timespan to create a Time from</param>
        public Time(TimeSpan timeSpan) 
            : this(Convert.ToInt32(timeSpan.TotalMilliseconds))
        {
        }

        /// <summary>
        /// The amount of seconds included in this time. Eg. 1500.ms().s == 1; 500.ms().s == 0
        /// </summary>
        public int Seconds => Value / 1000;
        /// <summary>
        /// The amount of hours included in this time. Eg. 50.min().h == 0; 62.min().h == 1
        /// </summary>
        public int Hours => Value / 3_600_000;
        /// <summary>
        /// The amount of minutes included in this time. Eg. 50.s().min == 0; 62.s().min == 1
        /// </summary>
        public int Minutes => Value / 60_000;
        /// <summary>
        /// The same as: <see cref="Value"/>
        /// </summary>
        public int Milliseconds => Value;
        /// <summary>
        /// The amount of days included in this time. Eg. 23.h().d == 0; 25.h().day == 1
        /// </summary>
        public int Days => Value / (3_600_000 * 24);

        #region [ operators ]

        /// <summary>
        /// Same as: <paramref name="seconds"/>.<see cref="Value"/>
        /// </summary>
        /// <param name="seconds"></param>
        public static explicit operator double(Time seconds)
            => seconds.Value;

        /// <summary>
        /// Same as: new Time(<paramref name="ms"/>)
        /// </summary>
        /// <param name="ms"></param>
        public static implicit operator Time(int ms)
            => new Time(ms);

        /// <summary>
        /// Creates a Time from <paramref name="time"/>.TotalMilliseconds
        /// </summary>
        /// <param name="time"></param>
        public static explicit operator Time (TimeSpan time) 
            => new Time(Convert.ToInt32(time.TotalMilliseconds));

        /// <summary>
        /// Creates a Timespan from <paramref name="seconds"/>.<see cref="Value"/>
        /// </summary>
        /// <param name="seconds"></param>
        public static implicit operator TimeSpan(Time seconds)
            => TimeSpan.FromMilliseconds(seconds.Value);

        /// <summary>
        /// Same as: Time.Parse(<paramref name="time"/>)
        /// </summary>
        /// <param name="time"></param>
        public static explicit operator Time(string time)
            => Parse(time);

        /// <summary>
        /// Applies <see cref="ToString"/>
        /// </summary>
        /// <param name="seconds"></param>
        public static implicit operator string(Time seconds)
            => seconds.ToString();

        /// <summary>
        /// Sums two Time
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static Time operator +(Time ms1, Time ms2)
            => new Time(ms1.Value + ms2.Value);

        /// <summary>
        /// Substract <paramref name="ms2"/> from <paramref name="ms1"/>
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static Time operator -(Time ms1, Time ms2)
            => new Time(ms1.Value - ms2.Value);

        /// <summary>
        /// Scalar (prepend) product <paramref name="scalar"/> by <paramref name="ms"/>
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Time operator *(int scalar, Time ms)
            => new Time(scalar * ms.Value);

        /// <summary>
        /// Scalar (append) product <paramref name="ms"/> by <paramref name="scalar"/> 
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Time operator *(Time ms, int scalar)
            => new Time(scalar * ms.Value);

        /// <summary>
        /// Scalar division <paramref name="ms"/> by <paramref name="scalar"/>
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Time operator /(Time ms, int scalar)
            => new Time(ms.Value / scalar);

        /// <summary>
        /// Divide <paramref name="ms1"/> by <paramref name="ms2"/> resulting in a double value
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static double operator /(Time ms1, Time ms2)
            => ms1.Value / (double) ms2.Value;

        /// <summary>
        /// Unary minus operation that creates the opposite value of <paramref name="ms"/>
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Time operator -(Time ms)
            => new Time(-ms.Value, ms.OriginalUnit);

        /// <summary>
        /// Compares if <paramref name="ms1"/> is greater than <paramref name="ms2"/>
        /// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator >(Time ms1, Time ms2)
            => ms1.Value > ms2.Value;

        /// <summary>
        /// Compares if <paramref name="ms1"/> is lesser than <paramref name="ms2"/>/// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator <(Time ms1, Time ms2)
            => ms1.Value < ms2.Value;

        /// <summary>
        /// Compares if <paramref name="ms1"/> is equals to <paramref name="ms2"/>/// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator ==(Time ms1, Time ms2)
            => ms1.Value == ms2.Value;

        /// <summary>
        /// Compares if <paramref name="ms1"/> is different from <paramref name="ms2"/>/// </summary>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool operator !=(Time ms1, Time ms2)
            => ms1.Value != ms2.Value;


        #endregion [ operators ]

        /// <summary>
        /// Converts the string representation of time to its Time value. 
        /// 4d<br/>2h<br/>2min<br/>3s<br/>5ms<br/>
        /// 2d:5h:15min:20s:100ms <br/>
        /// 2.5:15:20.100 (<see cref="TimeSpan.TryParse(string, out TimeSpan)"/>)
        /// </summary>
        /// <param name="time">string representation of type</param>
        /// <returns>A Time value</returns>
        /// <exception cref="FormatException">If <paramref name="str"/> is not in a correct format</exception>
        public static Time Parse(string time) 
            => TryParse(time, out var value)
                  ? value.Value
                  : throw new FormatException($"Invalid time format {time}");

        /// <summary>
        /// Try to convert the string representation of time to its Time value; either in the following formats: <br/>
        /// 4d<br/>2h<br/>2min<br/>3s<br/>5ms<br/>
        /// 2d:5h:15min:20s:100ms <br/>
        /// 2.5:15:20.100 (<see cref="TimeSpan.TryParse(string, out TimeSpan)"/>)
        /// </summary>
        /// <param name="time">string representation of time</param>
        /// <param name="seconds">A Time value</param>
        /// <returns>Whether the parsing succeded</returns>
        public static bool TryParse(string str, out Time? seconds)
        {
            if (TimeSpan.TryParse(str, out var ts))
            {
                seconds = (Time) ts;
                return true;
            }
            if (gReg.Match(str)?.Value == str)
            {
                seconds = str.Split(":")
                             .Select(t => partialReg.Match(t).Groups)
                             .Select(g => new Time(int.Parse(g[1].Value), g[2].Value))
                             .Aggregate(0.ms(), (ac, t) => ac + t);
                return true;
            }
            seconds = null;
            return false;
        }

        public override bool Equals(object obj)
            => Equals((Time)obj);

        public override int GetHashCode()
            => Value.GetHashCode();

        /// <summary>
        /// String representation of the Time expressed using a conversion to 
        /// the <see cref="OriginalUnit"/> it was used in the constructor.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override string ToString()
            => OriginalUnit switch
            {
                "s"   => $"{Value}s",
                "h"   => $"{Hours}h",
                "min" => $"{Minutes}min",
                "ms"  => $"{Milliseconds}ms",
                "d"   => $"{Days}day",
                _     => null
            };

        /// <summary>
        /// Uses <see cref="TimeSpan.ToString(string)"/> method to create formatted 
        /// string representation of this Time object.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
            => ((TimeSpan)this).ToString(format);

        public string ToString(string format, IFormatProvider formatProvider)
            => ((TimeSpan)this).ToString(format, formatProvider);

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

        /// <inheritdoc cref="IComparable{T}"/>
        public int CompareTo(Time other)
            => Math.Sign(Value - other.Value);

        /// <inheritdoc cref="IComparable"/>
        public int CompareTo(object obj)
            => this.CompareTo((Time)obj);

        /// <inheritdoc cref="IEquatable{T}"/>
        public bool Equals(Time other)
            => other.Value == Value;

    }
}
