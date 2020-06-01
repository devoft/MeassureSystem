namespace devoft.MeassureSystem
{
    public static class IntToWeightExtensions
    {
        public static Weight kg(this int value)
            => new Weight(value, "kg");
        public static Weight dg(this int value)
            => new Weight(value, "dg");
        public static Weight cg(this int value)
            => new Weight(value, "cg");
        public static Weight mg(this int value)
            => new Weight(value, "mg");
        public static Weight g(this int value)
            => new Weight(value);
        public static Weight oz(this int value)
            => new Weight(value, "oz");
        public static Weight lb(this int value)
            => new Weight(value, "lb");
    }
}
