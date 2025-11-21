using UnityEngine;

namespace Snaplight
{
    /// <summary> Набор цветов от Гамки OwO </summary>
    public static class Colorful
    {
        /// <summary> Прозрачный </summary>
        public static Color32 Clear => new(0, 0, 0, 0);
        /// <summary> Белый </summary>
        public static Color32 White => new(255, 255, 255, 255);
        /// <summary> Серый </summary>
        public static Color32 Gray => new(127, 127, 127, 255);
        /// <summary> Черный </summary>
        public static Color32 Black => new(0, 0, 0, 255);
        /// <summary> Красный </summary>
        public static Color32 Red => new(255, 0, 0, 255);
        /// <summary> Зеленый </summary>
        public static Color32 Green => new(0, 255, 0, 255);
        /// <summary> Синий </summary>
        public static Color32 Blue => new(0, 0, 255, 255);
        /// <summary> Желтый </summary>
        public static Color32 Yellow => new(255, 255, 0, 255);
        /// <summary> Голубой </summary>
        public static Color32 Cyan => new(0, 255, 255, 255);
        /// <summary> Пурпурный </summary>
        public static Color32 Magenta => new(255, 0, 255, 255);
        /// <summary> Оранжевый </summary>
        public static Color32 Orange => new(255, 153, 0, 255);
        /// <summary> Розовый </summary>
        public static Color32 Pink => new(255, 170, 170, 255);
        /// <summary> Фиолетовый </summary>
        public static Color32 Purple => new(170, 0, 170, 255);
        /// <summary> Темно-Бирюзовый </summary>
        public static Color32 Teal => new(0, 170, 153, 255);
        /// <summary> Коричневый </summary>
        public static Color32 Brown => new(153, 102, 51, 255);
        /// <summary> Бежевый </summary>
        public static Color32 Tan => new(255, 204, 102, 255);
        /// <summary> Манго </summary>
        public static Color32 MangoMelody => new(249, 175, 31, 255);
        /// <summary> Темно-Малиновый </summary>
        public static Color32 RichRazzleberry => new(152, 60, 106, 255);
        /// <summary> Пурпурный </summary>
        public static Color32 PurplePosy => new(236, 223, 238, 255);
        /// <summary> Ярко-красный </summary>
        public static Color32 RedReal => new(201, 30, 69, 255);
        /// <summary> Старое масло </summary>
        public static Color32 OldOlive => new(154, 165, 68, 255);
        /// <summary> Ярко-коричневый </summary>
        public static Color32 TerracottaTile => new(228, 117, 100, 255);
        /// <summary> Ярко=желтый  </summary>
        public static Color32 DaffodilDelight => new(255, 220, 96, 255);
        /// <summary> Ярко-зеленый  </summary>
        public static Color32 GreenGrannyApple => new(155, 181, 49, 255);
        /// <summary> Сиреневый  </summary>
        public static Color32 HighlandHeather => new(173, 153, 200, 255);
        /// <summary> Небесо-голубой  </summary>
        public static Color32 BlueBalmy => new(169, 214, 235, 255);
        /// <summary> Малино-бордовый  </summary>
        public static Color32 CrushedCyrry => new(241, 184, 0, 255);
        /// <summary> Бледный Темно-синий </summary>
        public static Color32 PaleMazarine => new(102, 102, 153, 255);
        /// <summary> Бледно-малиновый </summary>
        public static Color32 PaleСrimson => new(204, 102, 153, 255);
        /// <summary> Земляной </summary>
        public static Color32 Earthy => new(204, 102, 153, 255);
        /// <summary> Аквамариновый </summary>
        public static Color32 Aguamarine => new(127, 255, 102, 255);
        /// <summary> Бледно-зеленый </summary>
        public static Color32 PaleGreen => new(153, 229, 153, 255);

        public static Color32 Mix(Color32 color, float percentageContrast, byte percentageAlpha)
        {
            float r = (100 - percentageContrast) / 100;
            color.r = (byte)(color.r * r);
            color.g = (byte)(color.g * r);
            color.b = (byte)(color.b * r);
            color.a /= percentageAlpha;
            return color;
        }
    }
}