﻿namespace devoft.MeassureSystem
{
    public struct Meter3
    {
        public Meter3(decimal value)
            => Value = value;

        public decimal Value { get; }
    }
}
