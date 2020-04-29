using devoft.System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace devoft.MeassureSystem.Volume
{

    public struct Meter3
    {
        public static Regex m2Reg = new Regex(@"([0-9]+(?:[.|,][0-9]+)?)(?:\s)*(m3|km3|hm3|dam3|dm3|cm3|mm3)$");

        public decimal Value { get; }
        public string OriginalUnit { get; }

        public Meter3(decimal value)
        {
            Value = value;
            OriginalUnit = "m3";
        }

        public Meter3(decimal value, string unit)
        {
            if (unit?.In("mm3", "cm3", "dm3", "m3", "dam3", "hm3", "km3") != true)
                throw new ArgumentException($"unit mut be a valid cubit meter unit");
            OriginalUnit = unit;
            Value = value * unit switch
            {
                "mm3"   => 0.000000001m,
                "cm3"   => 0.000001m,
                "dm3"   => 0.001m,
                "m3"    => 1m,
                "dam3"  => 1000m,
                "hm3"   => 1000000m,
                "km3"   => 1000000000m,
                _       => 0m
            };
        }
    }
}
