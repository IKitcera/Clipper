using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Xamarin.Essentials;

namespace ClipperA.Listeners
{
    public enum ImageOrientation
    {
        Portrait,
        PortraitBottom,
        LandscapeLeft,
        LandscapeRight
    }
    class SensorOrientationListener
    {
        private SensorSpeed speed = SensorSpeed.UI;

        private Activity context;
        private ImageOrientation Orientation;
        public SensorOrientationListener(Activity context)
        {
            this.context = context;
        }
        
        void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            Quaternion q = e.Reading.Orientation;

            const float eps = 0.137472f;
            const float deg90 = 0.707f;

            if (q.X > 0 && Math.Abs(q.Y - q.Z) <= eps)
                Orientation = ImageOrientation.Portrait;
            else if (q.X < 0 && (Math.Abs(q.Y - q.Z) <= eps || Math.Abs(q.Y + q.Z) <= eps))
                Orientation = ImageOrientation.PortraitBottom;
            else if (Math.Abs(q.Z - q.X) <= eps && q.Y < 0)
                Orientation = ImageOrientation.LandscapeLeft;
            else if (Math.Abs(q.Z + q.X) <= eps && q.Y > 0)
                Orientation = ImageOrientation.LandscapeRight;
            else if (Math.Abs(q.X) <= eps && Math.Abs(q.Y) <= eps)
                Orientation = ImageOrientation.Portrait;
            else if (q.X < 0 && Math.Abs(q.Z) <= eps && Math.Abs(q.W) <= eps)
                Orientation = ImageOrientation.Portrait;

            SetOrientationToContext();
        }
       
        public void Start()
        {
            try
            {
                if (!OrientationSensor.IsMonitoring)
                    OrientationSensor.Start(speed);
                OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (System.Exception ex)
            {
                // Other error has occurred.
            }
        }

        public void Stop()
        {
            if (OrientationSensor.IsMonitoring)
                OrientationSensor.Stop();
            OrientationSensor.ReadingChanged -= OrientationSensor_ReadingChanged;
        }

        private void SetOrientationToContext()
        {
            var cameraActivity = (CameraActivity)context;

            cameraActivity.mSensorOrientation = Orientation;
            cameraActivity.OrientationChanged();
        }
    }  
}
