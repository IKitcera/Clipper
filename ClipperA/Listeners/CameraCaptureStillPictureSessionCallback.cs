using Android.Hardware.Camera2;
using Android.Util;

namespace ClipperA.Listeners
{
    public class CameraCaptureStillPictureSessionCallback : CameraCaptureSession.CaptureCallback
    {
        private static readonly string TAG = "CameraCaptureStillPictureSessionCallback";

        private readonly CameraActivity owner;

        public CameraCaptureStillPictureSessionCallback(CameraActivity owner)
        {
            if (owner == null)
                throw new System.ArgumentNullException("owner");
            this.owner = owner;
        }

        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
    
          owner.UnlockFocus();
        }
    }
}
