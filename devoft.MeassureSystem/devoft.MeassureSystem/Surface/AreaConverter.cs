using devoft.System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;

namespace devoft.MeassureSystem
{
    internal class AreaConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType.Name.In("String", "Double", "Decimal", "Int32");

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
            => destinationType.Name.In("String", "Double", "Decimal", "Int32");

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
            => value switch
        {
            string str  => Area.Parse(str),
            double d    => new Area(Convert.ToDecimal(d)),
            int i       => new Area(i),
            decimal dec => new Area(dec),
            _           => throw new NotSupportedException()
        };

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            => destinationType.Name switch
            {
                "String"    => value.ToString(),
                "Int32"     => Convert.ToInt32((decimal)(Area)value),
                "Decimal"   => (decimal)(Area)value,
                "Double"    => Convert.ToDouble((decimal)(Area)value),
                _           => throw new NotSupportedException()
            };
    }
}