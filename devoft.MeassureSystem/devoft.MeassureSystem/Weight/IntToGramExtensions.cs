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
    }
}
