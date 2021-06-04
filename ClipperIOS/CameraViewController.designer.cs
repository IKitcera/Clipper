// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ClipperIOS
{
	[Register ("CameraViewController")]
	partial class CameraViewController
	{
		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UIButton lightBtn { get; set; }

		[Outlet]
		UIKit.UILabel notAvailableLabel { get; set; }

		[Outlet]
		UIKit.UIImageView postImageView { get; set; }

		[Outlet]
		UIKit.UIButton rotateCameraBtn { get; set; }

		[Outlet]
		UIKit.UIButton takePictureBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (notAvailableLabel != null) {
				notAvailableLabel.Dispose ();
				notAvailableLabel = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (lightBtn != null) {
				lightBtn.Dispose ();
				lightBtn = null;
			}

			if (postImageView != null) {
				postImageView.Dispose ();
				postImageView = null;
			}

			if (rotateCameraBtn != null) {
				rotateCameraBtn.Dispose ();
				rotateCameraBtn = null;
			}

			if (takePictureBtn != null) {
				takePictureBtn.Dispose ();
				takePictureBtn = null;
			}
		}
	}
}
