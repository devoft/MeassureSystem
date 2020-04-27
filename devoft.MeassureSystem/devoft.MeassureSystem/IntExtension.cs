using System;
using System.Collections.Generic;
using System.Text;

namespace devoft.MeassureSystem
{
    public static class IntExtension
    {
        public static Meter cm(this int number) 
            => new Meter(number / 100m);

        public static Meter dm(this int number)
            => new Meter(number / 10m);

        public static Meter2 cm2(this int number)
            => new Meter2(number / 100m);

        public static Meter2 dm2(this int number)
            => new Meter2(number / 10m);
    }
}
