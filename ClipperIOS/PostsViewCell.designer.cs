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
	partial class PostsViewCell
	{
		[Outlet]
		public UIKit.UIImageView avtr { get; set; }

		[Outlet]
		public UIKit.UILabel comment { get; set; }

		[Outlet]
		public UIKit.UIImageView imageview { get; set; }

		[Outlet]
		public UIKit.UIPageControl pageControl { get; set; }

		[Outlet]
		public UIKit.UILabel reactionDown { get; set; }

		[Outlet]
		public UIKit.UILabel reactionUp { get; set; }

		[Outlet]
		public UIKit.UIScrollView scroll { get; set; }

		[Outlet]
		public UIKit.UILabel txtBelow { get; set; }

		[Outlet]
		public UIKit.UILabel userName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (userName != null) {
				userName.Dispose ();
				userName = null;
			}

			if (avtr != null) {
				avtr.Dispose ();
				avtr = null;
			}

			if (txtBelow != null) {
				txtBelow.Dispose ();
				txtBelow = null;
			}

			if (scroll != null) {
				scroll.Dispose ();
				scroll = null;
			}

			if (pageControl != null) {
				pageControl.Dispose ();
				pageControl = null;
			}

			if (reactionDown != null) {
				reactionDown.Dispose ();
				reactionDown = null;
			}

			if (comment != null) {
				comment.Dispose ();
				comment = null;
			}

			if (reactionUp != null) {
				reactionUp.Dispose ();
				reactionUp = null;
			}

			if (imageview != null) {
				imageview.Dispose ();
				imageview = null;
			}
		}
	}
}
