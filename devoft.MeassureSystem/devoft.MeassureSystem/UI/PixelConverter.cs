using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;

namespace devoft.MeassureSystem
{
    public class PixelConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType.Name.In("String", "int");

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
            => destinationType.Name.In("String", "int");

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
            => value switch
               {
                   string str => Pixel.Parse(str),
                   int i      => new Pixel(i),
                   _          => throw new NotSupportedException(),
               };

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return destinationType.Name switch
            {
                "String" => value.ToString(),
                "Int32"  => Convert.ToInt32((Pixel)value),
                _        => throw new NotSupportedException(),
            };
        }
    }
}