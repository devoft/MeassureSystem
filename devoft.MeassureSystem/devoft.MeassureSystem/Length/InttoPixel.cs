namespace devoft.MeassureSystem.UI
{
    public static class IntToPixel
    {
        public static Pixel px(this int number)
            => new Pixel(number, "px");
    }
}
