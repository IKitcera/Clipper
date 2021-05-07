
using Foundation;
using System;
using System.Drawing;
using UIKit;
using Clipper.ViewModels;

namespace ClipperIOS
{
    public partial class LoginViewController : UIViewController
    {
        LoginViewModel loginViewModel;
        UserSettings settings;
        public LoginViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            loginViewModel = new LoginViewModel();
            settings = new UserSettings();

            if (settings.GetDoNotLogOut())
            {
                if (settings.GetUserID() != "")
                {

                    //NEW CONTROLLER
                  //  var nSWindowController = storyboard.InstantiateControllerWithIdentifier("MainWindow") as NSWindowController;

                    //check
                  //  StartActivity(typeof(MainActivity));
                   // Finish();
                }
            }

            base.ViewDidLoad();


            // Perform any additional setup after loading the view, typically from a nib.
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

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion

        #region Methods
        public void LoginClick()
        {
            //loginViewModel.Email = FindViewById<EditText>(Resource.Id.Uemail).Text;
            //login.Password = FindViewById<EditText>(Resource.Id.Upassword).Text;

            //var userId = login.TryLogin();
            //if (userId != "")
            //{
            //    //Intent intent = new Intent(this, typeof(MainActivity));
            //    //Bundle bundle = new Bundle();
            //    //intent.PutExtra("userId", userId);

            //    var cb = FindViewById<CheckBox>(Resource.Id.logoutCB);

            //    settings.saveUserLogin(login.Email);
            //    settings.saveUserPassword(login.Password);
            //    settings.saveUserID(userId);

            //    if (FindViewById<CheckBox>(Resource.Id.logoutCB).Checked)
            //    {
            //        settings.DoNotLogOut(true);
            //    }

            //    //StartActivity(intent);

            //    StartActivity(typeof(MainActivity));
            //    Finish();
            //}
            //else
            //{
            //    FindViewById<TextView>(Resource.Id.lgnError).Visibility = ViewStates.Visible;
            //    FindViewById<TextView>(Resource.Id.lgnError).Text = login.Message;
            //}
        }
        #endregion
    }
}