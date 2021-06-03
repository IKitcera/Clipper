using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace ClipperIOS
{
    public class ImageProcessing
    {
        static string imgFolder = "PostedImages";

        public static UIImage ImgFromUrl(string uri)
        {
            try
            {
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                    return UIImage.LoadFromData(data);
            }
            catch
            {
                try
                {
                    return new UIImage(uri);
                }
                catch
                {
                    return new UIImage("Assets/600px-No_image_available.svg.png");
                }
            }
        }

        public static string EncodeImg(UIImage image)
        {
            var nsData = image.AsJPEG();
            NSError error = new NSError();
            nsData.Compress(NSDataCompressionAlgorithm.Lz4, out error);

            return nsData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
        }

        public static UIImage DecodeImg(string encodedImage)
        {
            var data = new NSData(encodedImage, NSDataBase64DecodingOptions.None);
            return new UIImage(data);
        }


        private static string CreateOrOpenDirectory()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string photosPath = Path.Combine(documentsPath, imgFolder);

            if (!Directory.Exists(photosPath))
                Directory.CreateDirectory(photosPath);
            return photosPath;
        }

        public static NSUrl SaveImg(UIImage image)
        {
            var bytes = image.AsJPEG().ToArray();

            var path = CreateOrOpenDirectory();

            var newName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() +
                DateTime.Now.Millisecond;

            path = Path.Combine(path, newName);
            path += ".jpg";

            if (File.Exists(path))
                path = path.Insert(path.Length - 4 - 1, "1");
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(new ReadOnlySpan<byte>(bytes));
            fs.Close();

            return new NSUrl(path);
        }
        public static async Task<List<UIImage>> LoadImages(List<string> UrlS)
        {
            List<UIImage> images = new List<UIImage>();
            foreach (var url in UrlS)
                images.Add(await LoadOne(url));
            return images;

        }
        public static async Task<UIImage> LoadOne(string url)
        {
            return ImageProcessing.ImgFromUrl(url);
        }

    }
        
}
