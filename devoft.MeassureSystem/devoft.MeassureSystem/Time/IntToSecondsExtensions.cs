namespace devoft.MeassureSystem.Time
{
    public static class IntToSecondsExtensions
    {
        public static Seconds min(this int value)
            => new Seconds(value * 60.0);
        public static Seconds s(this int value)
            => new Seconds(value);
        public static Seconds h(this int value)
            => new Seconds(value * 3600.0);
        public static Seconds ms(this int value)
            => new Seconds(value / 1000.0);
        public static Seconds days(this int value)
            => new Seconds(value * 24.0 * 3600.0);

    }
}
