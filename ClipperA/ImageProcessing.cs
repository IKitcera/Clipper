using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.Util;
using Android.Widget;
using ClipperA.Listeners;
using Java.IO;
using Java.Lang;
using Java.Nio;
using Java.Util;
namespace ClipperA
{
    class ImageProcessing
    {
        private CameraActivity context;
        public ImageProcessing(CameraActivity context)
        {
            this.context = context;
        }
        public static Bitmap ConvertImgToBtmp(Image img)
        {
            ByteBuffer buffer = img.GetPlanes()[0].Buffer;
            byte[] bytes = new byte[buffer.Capacity()];
            buffer.Get(bytes);

            return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
        }
        public static void RotateBitmap(ImageOrientation orientation, ref Bitmap image)
        {
            var matrix = new Matrix();

            // var scaleWidth = ((float)width) / image.Width;
            // var scaleHeight = ((float)height) / image.Height;
            // matrix.PostRotate(90);
            // matrix.PreScale(scaleWidth, scaleHeight);

            switch (orientation)
            {
                case ImageOrientation.Portrait:
                    matrix.PostRotate(90);
                    break;
                case ImageOrientation.LandscapeLeft:
                    //matrix.PostRotate(90);
                    break;
                case ImageOrientation.LandscapeRight:
                    matrix.PostRotate(180);
                    break;
                case ImageOrientation.PortraitBottom:
                    matrix.PostRotate(270);
                    break;
                default:
                    break;
            }

            image = Bitmap.CreateBitmap(image, 0, 0, image.Width, image.Height, matrix, true);
        }

        public static void SaveBitmap(Bitmap btmp, File file)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            btmp.Compress(Bitmap.CompressFormat.Jpeg, 80, stream);

            byte[] bytes = stream.ToArray();

            using (var output = new FileOutputStream(file))
            {
                try
                {
                    output.Write(bytes);
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
                finally
                {
                    btmp.Dispose();
                }
            }
        }


        public static void ScaleImage(ImageView view, int w, int h)
        {
            // Get bitmap from the the ImageView.
            Bitmap bitmap = null;

            try
            {
                var drawing = view.Drawable;
                bitmap = ((BitmapDrawable)drawing).Bitmap;
            }
            catch (NullPointerException e)
            {
                throw new NoSuchElementException("No drawable on given view");
            }
            catch (ClassCastException e)
            {
                // Check bitmap is Ion drawable

            }

            // Get current dimensions AND the desired bounding box
            int width = 0;

            try
            {
                width = bitmap.Width;
            }
            catch (NullPointerException e)
            {
                throw new NoSuchElementException("Can't find bitmap on given view/drawable");
            }

            int height = bitmap.Height;
            Log.Info("Test", "original width = " + width.ToString());
            Log.Info("Test", "original height = " + height.ToString());

            // Determine how much to scale: the dimension requiring less scaling is
            // closer to the its side. This way the image always stays inside your
            // bounding box AND either x/y axis touches it.  
            float xScale = ((float)w) / width;
            float yScale = ((float)h) / height;
            float scale = (xScale <= yScale) ? xScale : yScale;
            Log.Info("Test", "xScale = " + xScale.ToString());
            Log.Info("Test", "yScale = " + yScale.ToString());
            Log.Info("Test", "scale = " + scale.ToString());

            // Create a matrix for the scaling and add the scaling data
            Matrix matrix = new Matrix();
            matrix.PostScale(scale, scale);

            // Create a new bitmap and convert it to a format understood by the ImageView 
            Bitmap scaledBitmap = Bitmap.CreateBitmap(bitmap, 0, 0, width, height, matrix, true);
            width = scaledBitmap.Width; // re-use
            height = scaledBitmap.Height; // re-use
            BitmapDrawable result = new BitmapDrawable(scaledBitmap);
            Log.Info("Test", "scaled width = " + width.ToString());
            Log.Info("Test", "scaled height = " + height.ToString());

            // Apply the scaled bitmap
            view.SetImageDrawable(result);

            // Now change ImageView's dimensions to match the scaled image
            var p = (LinearLayout.LayoutParams)view.LayoutParameters; 

            p.Width = width;
            p.Height = height;
            view.LayoutParameters = p;

            Log.Info("Test", "done");
        }

    }
}
