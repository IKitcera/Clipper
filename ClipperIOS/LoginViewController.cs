
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
                    var nSWindowController = Storyboard.InstantiateViewController("MainWindow");
              
                    //check
                  //  StartActivity(typeof(MainActivity));
                   // Finish();
                }
            }
            errorLabel.Hidden = true;
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
            loginViewModel.Email = uEmail.Text;
            loginViewModel.Password =uPass.Text;

            var userId = loginViewModel.TryLogin();
            if (userId != "")
            {
                //    //Intent intent = new Intent(this, typeof(MainActivity));
                //    //Bundle bundle = new Bundle();
                //    //intent.PutExtra("userId", userId);

                var cb = doNotLogout;

                settings.saveUserLogin(loginViewModel.Email);
                    settings.saveUserPassword(loginViewModel.Password);
                    settings.saveUserID(userId);

                    if (cb.On)
                    {
                        settings.DoNotLogOut(true);
                    }

                //    //StartActivity(intent);

                //    StartActivity(typeof(MainActivity));
                //    Finish();
                }
                else
                {
                    errorLabel.Text = loginViewModel.Message;
                    errorLabel.Hidden = false;
                   
                }
            }
            #endregion
        }
}