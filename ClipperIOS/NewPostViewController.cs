﻿using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("NewPostViewController")]
    public class NewPostViewController : UIViewController
    {
        public string userId { get; set; }
        public NewPostViewController(IntPtr handle) : base(handle)
        { 

        }
    }
}