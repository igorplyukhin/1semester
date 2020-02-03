namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale2(Pixel[,] original)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var grayScaledPic = new double[picWidth, picHeight];
            for (var i = 0; i < picWidth; i++)
            {
                for (var j = 0; j < picHeight; j++)
                {
                    var pixel = original[i, j];
                    grayScaledPic[i, j] = CalcColor(pixel);
                }
            }

            return grayScaledPic;
        }

        private static double CalcColor(Pixel pixel) => (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
        
        public static double[,] ToGrayscale1(Pixel[,] original)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var grayScaledPic = new double[picWidth, picHeight];
            for (var i = 0; i < original.GetLength(0); i++)
            {
                for (var j = 0; j < original.GetLength(1); j++)
                {
                    var pixel = original[i, j];
                    grayScaledPic[i, j] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
                }
            }

            return grayScaledPic;
        }
        
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var grayScaledPic = new double[picWidth, picHeight];
            for (var i = 0; i < picWidth; i++)
            {
                for (var j = 0; j < picHeight; j++)
                {
                    var pixel = original[i, j];
                    grayScaledPic[i, j] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
                }
            }

            return grayScaledPic;
        }
    }
}