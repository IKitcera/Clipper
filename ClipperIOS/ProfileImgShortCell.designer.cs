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
	[Register ("ProfileImgShortCell")]
	partial class ProfileImgShortCell
	{
		[Outlet]
		public UIKit.UIImageView img { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (img != null) {
				img.Dispose ();
				img = null;
			}
		}
	}
}
