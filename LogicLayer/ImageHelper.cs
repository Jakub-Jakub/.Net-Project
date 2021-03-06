using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LogicLayer
{
    public static class ImageHelper
    {
        public static ImageSource ConvertByteArrayToImageSource(byte[] imagedata)
        {
            BitmapImage bmImg = new BitmapImage();
            MemoryStream strm = new MemoryStream(imagedata);
            bmImg.BeginInit();
            bmImg.StreamSource = strm;
            bmImg.EndInit();

            ImageSource imageSource = bmImg as ImageSource;

            return imageSource;
        }
        public static byte[] ConvertFileToByteArray(string fileName)
        {
            byte[] fileData = null;
            
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                var binaryReader = new BinaryReader(fs);
                fileData = binaryReader.ReadBytes((int)fs.Length);
            }
            return fileData;
        }
        public static byte[] BitMapImageToByte(BitmapImage imageSource)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
        public static ImageSource ConvertFileToImageSource(string fileName)
        {
            BitmapImage bmImg = new BitmapImage();
            bmImg.BeginInit();
            bmImg.UriSource = new Uri(fileName);
            bmImg.EndInit();

            ImageSource imageSource = bmImg as ImageSource;

            return imageSource;
        }
        public static ImageSource GetRandomKitten()
        {
            string url = "https://placekitten.com/g/";
            Random r = new Random();
            int x = (r.Next(100, 1300) / 50) * 50;
            int y = (r.Next(100, 1300) / 50) * 50;
            url += x.ToString() + "/" + y.ToString();
            ImageSource image = new BitmapImage(new Uri(url)); //https://placekitten.com/g/200/200
            return image;
        }
    }
}
