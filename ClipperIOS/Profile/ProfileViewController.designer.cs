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
	partial class ProfileViewController
	{
		[Outlet]
		UIKit.UILabel aboutMyselfLabel { get; set; }

		[Outlet]
		UIKit.UIImageView avtrImageView { get; set; }

		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		public UIKit.UICollectionView postsCollectionView { get; private set; }

		[Outlet]
		UIKit.UILabel postsCount { get; set; }

		[Outlet]
		UIKit.UILabel subscribersCount { get; set; }

		[Outlet]
		UIKit.UILabel subscribingsCount { get; set; }

		[Outlet]
		UIKit.UILabel userNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (aboutMyselfLabel != null) {
				aboutMyselfLabel.Dispose ();
				aboutMyselfLabel = null;
			}

			if (avtrImageView != null) {
				avtrImageView.Dispose ();
				avtrImageView = null;
			}

			if (postsCollectionView != null) {
				postsCollectionView.Dispose ();
				postsCollectionView = null;
			}

			if (postsCount != null) {
				postsCount.Dispose ();
				postsCount = null;
			}

			if (subscribersCount != null) {
				subscribersCount.Dispose ();
				subscribersCount = null;
			}

			if (subscribingsCount != null) {
				subscribingsCount.Dispose ();
				subscribingsCount = null;
			}

			if (userNameLabel != null) {
				userNameLabel.Dispose ();
				userNameLabel = null;
			}
		}
	}
}
