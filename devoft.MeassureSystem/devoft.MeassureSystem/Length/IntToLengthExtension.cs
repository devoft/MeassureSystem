namespace devoft.MeassureSystem
{
    public static class IntToLengthExtension
    {
        public static Length mm(this int number)
            => new Length(number, "mm");
        public static Length cm(this int number)
            => new Length(number,"cm");
        public static Length dm(this int number)
            => new Length(number, "dm");
        public static Length m(this int number)
            => new Length(number, "m");
        public static Length dam(this int number)
            => new Length(number, "dam");
        public static Length hm(this int number)
            => new Length(number, "hm");
        public static Length km(this int number)
            => new Length(number, "km");
        public static Length yd(this int number)
            => new Length(number, "yd");// / 1.093613m);
        public static Length inch(this int number)
            => new Length(number, "in"); // / 39.37008m);
        public static Length ft(this int number)
            => new Length(number, "ft"); // / 3.28084m);
        

        public static Area mm2(this int number)
            => new Area(number, "mm2");
        public static Area cm2(this int number)
            => new Area(number, "cm2");
        public static Area dm2(this int number)
            => new Area(number, "dm2");
        public static Area m2(this int number)
            => new Area(number, "m2");
        public static Area dam2(this int number)
            => new Area(number, "dam2");
        public static Area hm2(this int number)
            => new Area(number, "hm2");
        public static Area km2(this int number)
            => new Area(number, "km2");


        public static Volume mm3(this int number)
            => new Volume(number, "mm3");
        public static Volume cm3(this int number)
            => new Volume(number, "cm3");
        public static Volume dm3(this int number)
            => new Volume(number, "dm3");
        public static Volume m3(this int number)
            => new Volume(number, "m3");
        public static Volume dam3(this int number)
            => new Volume(number, "dam3");
        public static Volume hm3(this int number)
            => new Volume(number, "hm3");
        public static Volume km3(this int number)
            => new Volume(number, "km3");
        public static Volume ml(this int number)
            => new Volume(number, "ml");
        public static Volume l(this int number)
            => new Volume(number, "l");

    }
}
