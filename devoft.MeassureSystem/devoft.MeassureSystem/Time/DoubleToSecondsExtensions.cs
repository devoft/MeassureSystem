namespace devoft.MeassureSystem.Time
{
    public static class DoubleToSecondsExtensions
    {
        public static Seconds min(this double value)
            => new Seconds(value * 60.0);
        public static Seconds s(this double value)
            => new Seconds(value);
        public static Seconds h(this double value)
            => new Seconds(value * 3600.0);
        public static Seconds ms(this double value)
            => new Seconds(value / 1000.0);
        public static Seconds days(this double value)
            => new Seconds(value * 24.0 * 3600.0);

    }
}
