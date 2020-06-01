namespace devoft.MeassureSystem
{
    public static class DecimalToLengthExtension
    {
        public static Length mm(this decimal number)
            => new Length(number, "mm");
        public static Length cm(this decimal number)
            => new Length(number, "cm");
        public static Length dm(this decimal number)
            => new Length(number, "dm");
        public static Length m(this decimal number)
            => new Length(number, "m");
        public static Length dam(this decimal number)
            => new Length(number, "dam");
        public static Length hm(this decimal number)
            => new Length(number, "hm");
        public static Length km(this decimal number)
            => new Length(number, "km");
        public static Length yd(this decimal number)
            => new Length(number, "yd");// / 1.093613m);
        public static Length inch(this decimal number)
            => new Length(number, "in"); // / 39.37008m);
        public static Length ft(this decimal number)
            => new Length(number, "ft"); // / 3.28084m);
        

        public static Area mm2(this decimal number)
            => new Area(number, "mm2");
        public static Area cm2(this decimal number)
            => new Area(number, "cm2");
        public static Area dm2(this decimal number)
            => new Area(number, "dm2");
        public static Area m2(this decimal number)
            => new Area(number, "m2");
        public static Area dam2(this decimal number)
            => new Area(number, "dam2");
        public static Area hm2(this decimal number)
            => new Area(number, "hm2");
        public static Area km2(this decimal number)
            => new Area(number, "km2");


        public static Volume mm3(this decimal number)
            => new Volume(number, "mm3");
        public static Volume cm3(this decimal number)
            => new Volume(number, "cm3");
        public static Volume dm3(this decimal number)
            => new Volume(number, "dm3");
        public static Volume m3(this decimal number)
            => new Volume(number, "m3");
        public static Volume dam3(this decimal number)
            => new Volume(number, "dam3");
        public static Volume hm3(this decimal number)
            => new Volume(number, "hm3");
        public static Volume km3(this decimal number)
            => new Volume(number, "km3");
    }
}
