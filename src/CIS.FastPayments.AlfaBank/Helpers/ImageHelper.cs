using System.Drawing;

namespace CIS.FastPayments.AlfaBank.Helpers
{
    public static class ImageHelper
    {
        public static Image ConvertToImage(byte[] imageBytes)
        {
            //Convert byte type pictures into file
            using var im = new System.IO.MemoryStream(imageBytes);
            var img = Image.FromStream(im);

            return img;
        }

        public static void SaveImageToFile(byte[] imageBytes, string filename)
        {
            using var img = ConvertToImage(imageBytes);

            //save the file
            img.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
