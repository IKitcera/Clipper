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
	partial class MainFlowViewController
	{
		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (table != null) {
				table.Dispose ();
				table = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}
		}
	}
}
