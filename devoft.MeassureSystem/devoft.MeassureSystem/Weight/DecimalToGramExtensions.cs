namespace devoft.MeassureSystem.Weight
{
    public static class DecimalToGramExtensions
    {
        public static Gram kg(this decimal value)
            => new Gram(value * 1000m);
        public static Gram dg(this decimal value)
            => new Gram(value * 0.1m);
        public static Gram cg(this decimal value)
            => new Gram(value * 0.01m);
        public static Gram mg(this decimal value)
            => new Gram(value * 0.001m);
        public static Gram g(this decimal value)
            => new Gram(value);
        public static Gram oz(this decimal value)
            => new Gram(value * 28.34952m);
        public static Gram lb(this decimal value)
            => new Gram(value * 453.5924m);
    }
}
