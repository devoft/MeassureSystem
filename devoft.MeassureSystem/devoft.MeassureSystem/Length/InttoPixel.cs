using devoft.MeassureSystem.UI;

namespace devoft.MeassureSystem.UI
{
    public static class IntToPixel
    {
        public static Pixel px(this int number)
            => new Pixel(number, "px");
        //public static Pixel mm(this int number)
        //    => new Pixel(number, "mm");

    }
}
