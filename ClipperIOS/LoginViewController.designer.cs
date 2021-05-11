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
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		UIKit.UISwitch doNotLogout { get; set; }

		[Outlet]
		UIKit.UILabel errorLabel { get; set; }

		[Outlet]
		UIKit.UIButton loginBtn { get; set; }

		[Outlet]
		UIKit.UITextField uEmail { get; set; }

		[Outlet]
		UIKit.UITextField uPass { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (uEmail != null) {
				uEmail.Dispose ();
				uEmail = null;
			}

			if (uPass != null) {
				uPass.Dispose ();
				uPass = null;
			}

			if (doNotLogout != null) {
				doNotLogout.Dispose ();
				doNotLogout = null;
			}

			if (loginBtn != null) {
				loginBtn.Dispose ();
				loginBtn = null;
			}

			if (errorLabel != null) {
				errorLabel.Dispose ();
				errorLabel = null;
			}
		}
	}
}
