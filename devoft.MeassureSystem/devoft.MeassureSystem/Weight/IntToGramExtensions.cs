namespace devoft.MeassureSystem.Weight
{
    public static class IntToGramExtensions
    {
        public static Gram kg(this int value)
            => new Gram(value, "kg");
        public static Gram dg(this int value)
            => new Gram(value, "dg");
        public static Gram cg(this int value)
            => new Gram(value, "cg");
        public static Gram mg(this int value)
            => new Gram(value, "mg");
        public static Gram g(this int value)
            => new Gram(value);
        public static Gram oz(this int value)
            => new Gram(value, "oz");
        public static Gram lb(this int value)
            => new Gram(value, "lb");
    }
}
