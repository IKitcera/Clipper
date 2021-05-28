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
        public MainTabNavController(IntPtr handle):base(handle)
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
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}