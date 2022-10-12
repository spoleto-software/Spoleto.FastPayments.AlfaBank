//using System.Drawing;

//namespace Spoleto.FastPayments.AlfaBank.Helpers
//{
//    public static class ImageHelper
//    {
//        public static Image ConvertToImage(byte[] imageBytes)
//        {
//            if (imageBytes == null)
//                return null;

//            //Convert byte type pictures into file
//            using var im = new System.IO.MemoryStream(imageBytes);
//            var img = Image.FromStream(im);

//            return img;
//        }

//        public static void SaveImageToFile(byte[] imageBytes, string filename)
//        {
//            if (imageBytes == null)
//                return;

//            using var img = ConvertToImage(imageBytes);

//            //save the file
//            img.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
//        }
//    }
//}
