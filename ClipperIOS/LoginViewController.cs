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
            base.ViewDidLoad();

            loginViewModel = new LoginViewModel();
            settings = new UserSettings();

            loginBtn.TouchUpInside += (sender, e) => LoginClick();

            errorLabel.Hidden = true;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (settings.GetDoNotLogOut())
            {
                if (settings.GetUserID() != "" || settings.GetUserID() != null)
                {
                    PerformSegue("LoginSuccessful", this);
                }
                else
                {
                    uEmail.Text = settings.GetUserLogin();
                    uPass.Text = settings.GetUserPassword();
                }
            }
            preloader.Hidden = true;
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
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var mainController = segue.DestinationViewController as MainTabNavController;

            if (mainController != null)
                    mainController.Settings = settings;

        }
        #region Methods
        public void LoginClick()
        {
            loginViewModel.Email = uEmail.Text;
            loginViewModel.Password =uPass.Text;

            var userId = loginViewModel.TryLogin();
            if (userId != "")
            {
                var cb = doNotLogout;

                settings.saveUserLogin(loginViewModel.Email);
                settings.saveUserPassword(loginViewModel.Password);
                settings.saveUserID(userId);

                if (cb.On)
                   settings.DoNotLogOut(true);

                PerformSegue("LoginSuccessful", this);
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