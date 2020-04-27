using System;

namespace devoft.MeassureSystem
{
    public struct Litre
    {
        public decimal Value { get; }
        public Litre(decimal value)
            => Value = value;

        
    }

    public struct Meter
    {
        public decimal Value { get; }

        public Meter(decimal value) 
            => Value = value;
        
        public decimal cm
            => Value * 100;
       


        public static Meter operator + (Meter m1, Meter m2)
            => new Meter(m1.Value + m2.Value);



        public static Meter operator -(Meter m1, Meter m2)
            => new Meter(m1.Value - m2.Value);



        public static Meter operator *(Meter m, decimal d)
           => new Meter(m.Value * d);
        public static Meter operator *(decimal d, Meter m)
           => new Meter(m.Value * d);
        public static Meter2 operator *(Meter m1, Meter m2)
           => new Meter2(m1.Value * m2.Value);
        public static Meter3 operator *(Meter2 m1, Meter m2)
          => new Meter3(m1.Value * m2.Value);
        public static Meter3 operator *(Meter m1, Meter2 m2)
          => new Meter3(m1.Value * m2.Value);
        


        public static decimal operator /(Meter m1, Meter m2)
           => m1.Value / m2.Value;
        public static Meter operator /(Meter m, decimal d)
          => new Meter(m.Value / d);




    }
}
