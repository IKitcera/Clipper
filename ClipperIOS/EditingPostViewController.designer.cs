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
	[Register ("EditingPostViewController")]
	partial class EditingPostViewController
	{
		[Outlet]
		public UIKit.UIButton backBtn { get; set; }

		[Outlet]
		public UIKit.UIPageControl pageCntrl { get; set; }

		[Outlet]
		public UIKit.UIButton postBtn { get; set; }

		[Outlet]
		public UIKit.UIScrollView scroll { get; set; }

		[Outlet]
		public UIKit.UITextField txtBelow { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scroll != null) {
				scroll.Dispose ();
				scroll = null;
			}

			if (pageCntrl != null) {
				pageCntrl.Dispose ();
				pageCntrl = null;
			}

			if (txtBelow != null) {
				txtBelow.Dispose ();
				txtBelow = null;
			}

			if (postBtn != null) {
				postBtn.Dispose ();
				postBtn = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}
		}
	}
}
