using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;

namespace devoft.MeassureSystem
{
    public class LengthConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
            => destinationType.Name.In("String", "Decimal", "int", "Double");

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType.Name.In("String", "Decimal", "int", "Double");

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            => destinationType.Name switch 
               {
                   "String"  => value.ToString(),
                   "Decimal" => (decimal) (Length) value,
                   "Int32"   => Convert.ToInt32((decimal)(Length)value),
                   "Double"  => Convert.ToDouble((decimal)(Length)value),
                   _         => throw new NotSupportedException()
               };

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
            => value switch
            {
                string str   => Length.Parse(str),
                decimal dec  => new Length(dec),
                int i        => new Length(i),
                double d     => new Length(Convert.ToDecimal(d)),
                _            => throw new NotSupportedException()
            }; 
    }
}