using System;

namespace devoft.MeassureSystem
{

    public struct Meter
    {
        public decimal Value { get; }

        /// <summary>
        /// Milimeters
        /// </summary>
        public decimal mm => Value * 1000;
        /// <summary>
        /// Centimeters
        /// </summary>
        public decimal cm => Value * 100;
        /// <summary>
        /// Decimeters
        /// </summary>
        public decimal dm => Value * 10;
        /// <summary>
        /// Meter
        /// </summary>
        public decimal m => Value ;
        /// <summary>
        /// Hectometer
        /// </summary>
        public decimal hm => Value / 100;
        /// <summary>
        /// Decameter
        /// </summary>
        public decimal dam => Value / 10;
        /// <summary>
        /// Kilometers
        /// </summary>
        public decimal km => Value / 1000;
        /// <summary>
        /// Yards
        /// </summary>
        public decimal yd => Value * 1.093613m;
        /// <summary>
        /// Inches
        /// </summary>
        public decimal inch => Value * 39.37008m;
        /// <summary>
        /// Feet
        /// </summary>
        public decimal feet => Value * 3.28084m;

        public Meter(decimal value) 
            => Value = value;
        

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
