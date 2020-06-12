namespace devoft.MeassureSystem
{
    public static class IntToTimeExtensions
    {
        /// <summary>
        /// Creates a Time corresponding to <paramref name="value"/> minutes
        /// </summary>
        /// <param name="value">The amount of minutes</param>
        /// <returns>Time representation</returns>
        public static Time min(this int value)
            => new Time(value, "min");

        /// <summary>
        /// Creates a Time corresponding to <paramref name="value"/> seconds
        /// </summary>
        /// <param name="value">The amount of seconds</param>
        /// <returns>Time representation</returns>
        public static Time s(this int value)
            => new Time(value, "s");

        /// <summary>
        /// Creates a Time corresponding to <paramref name="value"/> hours
        /// </summary>
        /// <param name="value">The amount of hours</param>
        /// <returns>Time representation</returns>
        public static Time h(this int value)
            => new Time(value, "h");

        /// <summary>
        /// Creates a Time corresponding to <paramref name="value"/> milliseconds
        /// </summary>
        /// <param name="value">The amount of milliseconds</param>
        /// <returns>Time representation</returns>
        public static Time ms(this int value)
            => new Time(value, "ms");

        /// <summary>
        /// Creates a Time corresponding to <paramref name="value"/> days
        /// </summary>
        /// <param name="value">The amount of days</param>
        /// <returns>Time representation</returns>
        public static Time d(this int value)
            => new Time(value, "d");

    }
}
