namespace devoft.MeassureSystem
{
    public static class DecimalExtension
    {
        public static Meter cm(this decimal number)
            => new Meter(number / 100m);
    }
}
