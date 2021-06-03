using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("MainTabNavController")]
    public class MainTabNavController : UITabBarController
    {
        public UserSettings Settings { get; set; }

        public MainTabNavController()
        {

        }
        public MainTabNavController(IntPtr handle) : base(handle)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        public override void ViewWillAppear(bool animated)
        {

            base.ViewWillAppear(animated);
        }
        public override void ItemSelected(UITabBar tabbar, UITabBarItem item)
        {
           // ResetAll();
        }
        private void ResetAll()
        {
            for (int i = 0; i < ViewControllers.Count(); i++)
            {
                if (ViewControllers[i] is MainFlowViewController)
                {
                    var mfVC = Storyboard.InstantiateViewController("MainFlowController");
                    ViewControllers[i] = mfVC;
                }
                else if (ViewControllers[i] is NewPostViewController)
                {
                    var npVC = Storyboard.InstantiateViewController("NewPostController");
                    ViewControllers[i] = npVC;

                }
                else if (ViewControllers[i] is ProfileViewController)
                {
                    var pVC = Storyboard.InstantiateViewController("ProfileController");
                    ViewControllers[i] = pVC;

                }
            }
        }
    }
}