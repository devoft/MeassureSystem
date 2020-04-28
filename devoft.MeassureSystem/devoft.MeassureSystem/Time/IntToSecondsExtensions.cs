namespace devoft.MeassureSystem.Time
{
    public static class IntToSecondsExtensions
    {
        public static Milliseconds min(this int value)
            => new Milliseconds(value * 60_000);
        public static Milliseconds s(this int value)
            => new Milliseconds(value * 1000);
        public static Milliseconds h(this int value)
            => new Milliseconds(value * 3600_000);
        public static Milliseconds ms(this int value)
            => new Milliseconds(value);
        public static Milliseconds days(this int value)
            => new Milliseconds(value * 24 * 3600_000);

    }
}
