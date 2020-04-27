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
    }
}
