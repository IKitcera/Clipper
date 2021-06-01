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
		public UIKit.UIButton backBtn { get; private set; }

		[Outlet]
		public UIKit.UIPageControl pageCntrl { get; private set; }

		[Outlet]
		public UIKit.UIButton postBtn { get; private set; }

		[Outlet]
		UIKit.UIProgressView progressBar { get; set; }

		[Outlet]
		public UIKit.UIScrollView scroll { get; private set; }

		[Outlet]
		public UIKit.UITextField txtBelow { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (progressBar != null) {
				progressBar.Dispose ();
				progressBar = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (pageCntrl != null) {
				pageCntrl.Dispose ();
				pageCntrl = null;
			}

			if (postBtn != null) {
				postBtn.Dispose ();
				postBtn = null;
			}

			if (scroll != null) {
				scroll.Dispose ();
				scroll = null;
			}

			if (txtBelow != null) {
				txtBelow.Dispose ();
				txtBelow = null;
			}
		}
	}
}
