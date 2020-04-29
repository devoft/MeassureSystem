using devoft.MeassureSystem.Surface;
using devoft.MeassureSystem.Volume;

namespace devoft.MeassureSystem.Length
{
    public static class IntToMeterExtension
    {
        public static Meter mm(this int number)
            => new Meter(number, "mm");
        public static Meter cm(this int number)
            => new Meter(number,"cm");
        public static Meter dm(this int number)
            => new Meter(number, "dm");
        public static Meter m(this int number)
            => new Meter(number, "m");
        public static Meter dam(this int number)
            => new Meter(number, "dam");
        public static Meter hm(this int number)
            => new Meter(number, "hm");
        public static Meter km(this int number)
            => new Meter(number, "km");
        public static Meter yd(this int number)
            => new Meter(number, "yd");// / 1.093613m);
        public static Meter inch(this int number)
            => new Meter(number, "in"); // / 39.37008m);
        public static Meter ft(this int number)
            => new Meter(number, "ft"); // / 3.28084m);

        public static Meter2 mm2(this int number)
            => new Meter2(number, "mm2");
        public static Meter2 cm2(this int number)
            => new Meter2(number, "cm2");
        public static Meter2 dm2(this int number)
            => new Meter2(number, "dm2");
        public static Meter2 m2(this int number)
            => new Meter2(number, "m2");
        public static Meter2 dam2(this int number)
            => new Meter2(number, "dam2");
        public static Meter2 hm2(this int number)
            => new Meter2(number, "hm2");
        public static Meter2 km2(this int number)
            => new Meter2(number, "km2");

        public static Meter3 mm3(this int number)
            => new Meter3(number, "mm3");
        public static Meter3 cm3(this int number)
            => new Meter3(number, "cm3");
        public static Meter3 dm3(this int number)
            => new Meter3(number, "dm3");
        public static Meter3 m3(this int number)
            => new Meter3(number, "m3");
        public static Meter3 dam3(this int number)
            => new Meter3(number, "dam3");
        public static Meter3 hm3(this int number)
            => new Meter3(number, "hm3");
        public static Meter3 km3(this int number)
            => new Meter3(number, "km3");

    }
}
