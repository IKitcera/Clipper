using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("ProfileViewController")]
    public class ProfileViewController : UIViewController
    {
        public string userId { get; set; }

        public ProfileViewController(IntPtr handle) : base(handle)
        {

        }
    }
}