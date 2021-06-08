using Foundation;
using System;
using System.Drawing;
using UIKit;
using Clipper.ViewModels;
using System.Threading.Tasks;

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
            base.DidReceiveMemoryWarning();
        }

        #region View lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            loginViewModel = new LoginViewModel();
            settings = new UserSettings();

            loginBtn.TouchUpInside += (sender, e) => LoginClick();

            uEmail.EditingDidEndOnExit += (s, e) => ResignFirstResponder();
            uPass.EditingDidEndOnExit += (s, e) => ResignFirstResponder();
            errorLabel.Hidden = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            uEmail.Text = settings.GetUserLogin();
            uPass.Text = settings.GetUserPassword();
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
        private void LoginClick()
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