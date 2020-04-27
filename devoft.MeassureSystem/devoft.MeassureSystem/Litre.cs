namespace devoft.MeassureSystem
{
    public struct Litre
    {
        public decimal Value { get; }
        public Litre(decimal value)
            => Value = value;
    }
}
