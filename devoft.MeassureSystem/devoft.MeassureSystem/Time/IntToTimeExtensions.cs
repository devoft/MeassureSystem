namespace devoft.MeassureSystem
{
    public static class IntToTimeExtensions
    {
        public static Time min(this int value)
            => new Time(value * 60_000);
        public static Time s(this int value)
            => new Time(value * 1000);
        public static Time h(this int value)
            => new Time(value * 3600_000);
        public static Time ms(this int value)
            => new Time(value);
        public static Time d(this int value)
            => new Time(value * 24 * 3600_000);

    }
}
