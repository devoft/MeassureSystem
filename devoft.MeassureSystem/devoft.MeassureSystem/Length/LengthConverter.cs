using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;

namespace devoft.MeassureSystem
{
    internal class LengthConverter : TypeConverter
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
                   "int"     => Convert.ToInt32((decimal) (Length)value),
                   "Double"  => Convert.ToDouble((decimal) (Length)value),
                   _         => throw new NotSupportedException()
               };

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
            => value.GetType().Name switch
            {
                "String"    => Length.Parse((string)value),
                "Decimal"   => new Length(Convert.ToDecimal(value)),
                "int"       => new Length(Convert.ToDecimal(value)),
                "Double"    => new Length(Convert.ToDecimal(value)),
                _           => throw new NotSupportedException()
            }; 
    }
}