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
	[Register ("PostItemCell")]
	partial class PostItemCell
	{

		[Outlet]
		public UIKit.UIButton aBtn { get; set; }

		[Outlet]
		public UIKit.UIImageView avtr { get; private set; }

		[Outlet]
		public UIKit.UILabel comment { get; private set; }

		[Outlet]
		public UIKit.UIImageView imageview { get; private set; }

		[Outlet]
		public UIKit.UIPageControl pageControl { get; private set; }

		[Outlet]
		public UIKit.UILabel reactionDown { get; private set; }

		[Outlet]
		public UIKit.UILabel reactionUp { get; private set; }

		[Outlet]
		public UIKit.UIScrollView scroll { get; private set; }

		[Outlet]
		public UIKit.UILabel txtBelow { get; private set; }

		[Outlet]
		public UIKit.UILabel userName { get; private set; }

		void ReleaseDesignerOutlets()
		{
			if (aBtn != null)
			{
				aBtn.Dispose();
				aBtn = null;
			}

			if (avtr != null)
			{
				avtr.Dispose();
				avtr = null;
			}

			if (comment != null)
			{
				comment.Dispose();
				comment = null;
			}

			if (imageview != null)
			{
				imageview.Dispose();
				imageview = null;
			}

			if (pageControl != null)
			{
				pageControl.Dispose();
				pageControl = null;
			}

			if (reactionDown != null)
			{
				reactionDown.Dispose();
				reactionDown = null;
			}

			if (reactionUp != null)
			{
				reactionUp.Dispose();
				reactionUp = null;
			}

			if (scroll != null)
			{
				scroll.Dispose();
				scroll = null;
			}

			if (txtBelow != null)
			{
				txtBelow.Dispose();
				txtBelow = null;
			}

			if (userName != null)
			{
				userName.Dispose();
				userName = null;
			}
		}
	}
}
