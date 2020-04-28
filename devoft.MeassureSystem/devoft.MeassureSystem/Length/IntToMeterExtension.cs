using devoft.MeassureSystem.Surface;
using System;
using System.Collections.Generic;
using System.Text;

namespace devoft.MeassureSystem.Length
{
    public static class IntToMeterExtension
    {
        public static Meter mm(this int number)
            => new Meter(number / 1000m);
        public static Meter cm(this int number) 
            => new Meter(number / 100m);
        public static Meter dm(this int number)
            => new Meter(number / 10m);
        public static Meter m(this int number)
            => new Meter(number);
        public static Meter dam(this int number)
            => new Meter(number * 10);
        public static Meter hm(this int number)
            => new Meter(number * 100);
        public static Meter km(this int number)
            => new Meter(number * 1000);
        public static Meter yd(this int number)
            => new Meter(number / 1.093613m);
        public static Meter inch(this int number)
            => new Meter(number / 39.37008m);
        public static Meter ft(this int number)
            => new Meter(number / 3.28084m);


        public static Meter2 cm2(this int number)
            => new Meter2(number / 100m);

        public static Meter2 dm2(this int number)
            => new Meter2(number / 10m);
    }
}
