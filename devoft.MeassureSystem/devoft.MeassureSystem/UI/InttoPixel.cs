namespace devoft.MeassureSystem
{
    public static class IntToPixel
    {
        public static Pixel px(this int number)
            => new Pixel(number);
    }
}
