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
	[Register ("PhotoShortCut")]
	partial class PhotoShortCut
	{
		[Outlet]
		public UIKit.UIButton check { get; private set; }

		[Outlet]
		public UIKit.UIImageView img { get; private set; }

		[Outlet]
		public UIKit.UIButton imgBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (check != null) {
				check.Dispose ();
				check = null;
			}

			if (img != null) {
				img.Dispose ();
				img = null;
			}

			if (imgBtn != null) {
				imgBtn.Dispose ();
				imgBtn = null;
			}
		}
	}
}
