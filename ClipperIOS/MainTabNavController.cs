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
    }
}