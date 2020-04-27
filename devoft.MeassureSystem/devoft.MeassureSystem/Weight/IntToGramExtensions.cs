namespace devoft.MeassureSystem.Weight
{
    public static class IntToGramExtensions
    {
        public static Gram kg(this int value)
            => new Gram(value * 1000m);
        public static Gram dg(this int value)
            => new Gram(value * 0.1m);
        public static Gram cg(this int value)
            => new Gram(value * 0.01m);
        public static Gram mg(this int value)
            => new Gram(value * 0.001m);
        public static Gram g(this int value)
            => new Gram(value);
        public static Gram oz(this int value)
            => new Gram(value * 28.34952m);
        public static Gram lb(this int value)
            => new Gram(value * 453.5924m);
    }
}
