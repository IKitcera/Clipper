using Android.App;
using Android.Graphics;
using Android.Media;
using Android.Util;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using Java.IO;
using Java.Lang;
using Java.Nio;
using System;
using System.Numerics;
using System.Threading;
using Xamarin.Essentials;
using static Android.Provider.MediaStore;
using Math = System.Math;

namespace ClipperA.Listeners
{
    public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
    {
        private ManualResetEvent mre;
        private readonly CameraActivity owner;
        public File PostResult { get; private set; }
        public ImageAvailableListener(CameraActivity context, ManualResetEvent mre)
        {
            if (context == null)
                throw new System.ArgumentNullException("context");

            owner = context;
            this.mre = mre;
        }
        public void OnImageAvailable(ImageReader reader)
        {
            var img = reader.AcquireNextImage();

            var btmp = ImageProcessing.ConvertImgToBtmp(img);

            if (owner.isFaceCamera)
                if (owner.mSensorOrientation == ImageOrientation.Portrait)
                    owner.mSensorOrientation = ImageOrientation.PortraitBottom;
                else if (owner.mSensorOrientation == ImageOrientation.PortraitBottom)
                    owner.mSensorOrientation = ImageOrientation.Portrait;

            ImageProcessing.RotateBitmap(owner.mSensorOrientation, ref btmp);
           
            var imageSaver = new ImageSaver(btmp, owner.GetExternalFilesDir(null), mre);

            owner.mBackgroundHandler.Post(imageSaver);
            PostResult = imageSaver.mFile;
        }

        // Saves a JPEG {@link Image} into the specified {@link File}.
        private class ImageSaver : Java.Lang.Object, IRunnable
        {
            // The JPEG image
            private Bitmap mImage;
            private ManualResetEvent mre;
            // The file we save the image into.
            public File mFile { get; private set; }

            public ImageSaver(Bitmap image, File file, ManualResetEvent mre)
            {
                if (image == null)
                    throw new System.ArgumentNullException("image");
                if (file == null)
                    throw new System.ArgumentNullException("file");

                mImage = image;

                var time = DateTime.Now;
                var fileName = time.Year.ToString() + time.Month.ToString() + time.Day.ToString() + "_" + time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString() + time.Millisecond.ToString() + ".jpg";

                mFile = new File(file, fileName);

                this.mre = mre;
            }

            public void Run()
            {
                ImageProcessing.SaveBitmap(mImage, mFile);
                mre.Set();
            }
        }
    }
}