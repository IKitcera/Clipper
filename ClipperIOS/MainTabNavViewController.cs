using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    class MainTabNavViewController : UITabBarController
    {
        UIViewController mainFlowTab, newPostTab, profileTab;
        public MainTabNavViewController()
        {
            mainFlowTab = new UIViewController();
            mainFlowTab.TabBarItem = new UITabBarItem("Home", new UIImage("Resource/icons/outline_home_black_24pt.imageset/outline_home_black_24pt.2x.png"), 0);

            newPostTab = new UIViewController();
            newPostTab.TabBarItem = new UITabBarItem("New Post", new UIImage("Resource/icons/outline_add_black_24pt.imageset/outline_add_black_24pt.2x.png"), 1);

            profileTab = new UIViewController();
            profileTab.TabBarItem = new UITabBarItem("Profile", new UIImage("Resource/icons/outline_person_outline_black_24pt.imageset/outline_person_outline_black_24pt.2x.png"), 2);

            ViewControllers = new UIViewController[] { mainFlowTab, newPostTab, profileTab };
        }
    }
}