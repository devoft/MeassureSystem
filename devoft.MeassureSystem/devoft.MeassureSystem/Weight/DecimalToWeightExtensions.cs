namespace devoft.MeassureSystem
{
    public static class DecimalToWeightExtensions
    {
        public static Weight kg(this decimal value)
            => new Weight(value * 1000m);
        public static Weight dg(this decimal value)
            => new Weight(value * 0.1m);
        public static Weight cg(this decimal value)
            => new Weight(value * 0.01m);
        public static Weight mg(this decimal value)
            => new Weight(value * 0.001m);
        public static Weight g(this decimal value)
            => new Weight(value);
        public static Weight oz(this decimal value)
            => new Weight(value * 28.34952m);
        public static Weight lb(this decimal value)
            => new Weight(value * 453.5924m);
    }
}
