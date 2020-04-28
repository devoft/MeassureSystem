namespace devoft.MeassureSystem.Surface
{
    public struct Meter2
    {
        public Meter2(decimal value)
            => Value = value;

        public decimal Value { get; }


        public static Meter2 operator + (Meter2 m1, Meter2 m2)
           => new Meter2(m1.Value + m2.Value);



        public static Meter2 operator - (Meter2 m1, Meter2 m2)
            => new Meter2(m1.Value - m2.Value);


        public static Meter2 operator *(decimal d, Meter2 m2)
          => new Meter2(m2.Value * d);
        public static Meter2 operator *(Meter2 m2, decimal d)
          => new Meter2(m2.Value * d);

        public static decimal operator /(Meter2 m1, Meter2 m2)
          => m1.Value / m2.Value;
        public static Meter2 operator /(Meter2 m, decimal d)
          => new Meter2(m.Value / d);

    }


}
