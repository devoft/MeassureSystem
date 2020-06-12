using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;

namespace devoft.MeassureSystem
{
    internal class VolumeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType.Name.In("String", "Decimal", "Double", "Int32");

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
            => destinationType.Name.In("String", "Decimal", "Double", "Int32");

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
            => value switch
            {
                string str  => Volume.Parse(str),
                double d    => new Volume(Convert.ToDecimal(d)),
                int i       => new Volume(i),
                decimal dec => new Volume(dec),
                _           => throw new NotSupportedException()
            };

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
            => destinationType.Name switch
            {
                "String"    => value.ToString(),
                "Double"    => Convert.ToDouble((decimal)(Volume)value),
                "Decimal"   => (decimal)(Volume)value,
                "Int32"     => Convert.ToInt32((decimal)(Volume)value),
                _           => throw new NotSupportedException()
            };
    }
}