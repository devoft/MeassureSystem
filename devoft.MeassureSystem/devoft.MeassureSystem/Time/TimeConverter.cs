using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;

namespace devoft.MeassureSystem
{
    /// <summary>
    /// Converts Time to and from <see cref="String"/>, <see cref="Int32"/> or <see cref="TimeSpan"/>
    /// </summary>
    public class TimeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType.Name.In("String", "Int32", "TimeSpan");

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            => destinationType.Name switch
               {
                   "String"     => ((Time) value).ToString(),
                   "TimeSpan"   => (TimeSpan) (Time) value,
                   "Int32"      => ((Time) value).Value,
                   _            => throw new NotSupportedException()
               };

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType.Name.In("String", "Int32", "TimeSpan");

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            => value switch
               {
                   string str   => Time.Parse(str),
                   int ms       => new Time(ms),
                   TimeSpan ts  => new Time(ts),
                   _            => throw new NotSupportedException()
               };
    }
}