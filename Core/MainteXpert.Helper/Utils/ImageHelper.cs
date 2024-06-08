namespace MainteXpert.Helper.Utils
{
    public class ImageHelper
    {
        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        public static string CompressImage(string base64String, float width, float height)
        {
            try
            {
                var bmp = ConvertFromBase64ToBitmap(base64String);
                byte[] arr = ConvertToImageJpegByteArray(bmp, width, height);
                return Convert.ToBase64String(arr);
            }
            catch (Exception)
            {
                return base64String;
            }
        }

        public static Bitmap ConvertFromBase64ToBitmap(string base64ImageString)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(base64ImageString);
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    var image = Image.FromStream(ms, false, false);
                    var bmp = new Bitmap(image);
                    return bmp;
                }
            }
            catch (Exception ex)
            {
                return new Bitmap(1, 1);
            }

        }

        public static byte[] ConvertToImageJpegByteArray(Image image, float width, float height)
        {
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                var brush = new SolidBrush(Color.White);
                width = Math.Min(image.Width, width);
                height = Math.Min(image.Height, height);

                float scale = Math.Min(width / image.Width, height / image.Height);
                var scaleWidth = width;
                var scaleHeight = height;
                var scaledBitmap = new Bitmap((int)scaleWidth, (int)scaleHeight);
                Graphics graph = Graphics.FromImage(scaledBitmap);
                graph.InterpolationMode = InterpolationMode.High;
                graph.CompositingQuality = CompositingQuality.HighQuality;
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
                graph.DrawImage(image, new Rectangle(0, 0, (int)scaleWidth, (int)scaleHeight));
                scaledBitmap.Save(ms, ImageFormat.Bmp);
                arr = ms.ToArray();
            }
            return arr;
        }
    }
}
