using Clipper.ViewModels;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("MainFlowViewController")]
    public partial class MainFlowViewController : UIViewController
    {
        HomeViewModel homeViewModel;
        public string userId { get; set; }
        public UserSettings Settings { get; set; }
        public bool isOwn { get; set; } = false;
        public NSIndexPath indexPath {get; set;}

        public MainFlowViewController(IntPtr handle):base(handle)
        {
          
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (userId == null || userId == "")
            {
                    Settings ??= ((MainTabNavController)ParentViewController).Settings;
                    userId = Settings.GetUserID();
            }

            homeViewModel = new HomeViewModel(userId, isOwn);

            table.Source = new PostsTableSource(homeViewModel, this);

            if (isOwn)
            {
                backBtn.Hidden = false;
                table.Frame = new CoreGraphics.CGRect(0, 70, 414, 860);

                backBtn.TouchUpInside += (sender, e) => DismissViewController(false, null);
            }
               
            var permissions = Photos.PHPhotoLibrary.AuthorizationStatus;

            if (Settings != null)
            {
                if (Settings.GetStoragePermission() == false)
                {
                    switch (permissions)
                    {
                        case Photos.PHAuthorizationStatus.NotDetermined:
                            Photos.PHPhotoLibrary.RequestAuthorization((p) =>
                            {
                                switch (p)
                                {
                                    case Photos.PHAuthorizationStatus.Denied:
                                        Settings.saveStoragePermission(false);
                                        break;
                                    case Photos.PHAuthorizationStatus.Authorized:
                                        Settings.saveStoragePermission(true);
                                        break;
                                }
                            });
                            break;
                    }
                }
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if(isOwn)
                table.ScrollToRow(indexPath, UITableViewScrollPosition.Top, false);
        }

        /*
        public void ShowUsersProfile(string userId)
        {
            var profileControler = Storyboard.InstantiateViewController("ProfileController") as ProfileViewController;
            profileControler.userId = userId;
            profileControler.isOwn = false;

            ShowViewController(profileControler, this);
        }
        */

        public void ShowUsersProfile(string userId)
        {
            var mainController = Storyboard.InstantiateViewController("MainNav") as MainTabNavController;
            var profileControler = mainController.ViewControllers.Where(controler => controler is ProfileViewController).FirstOrDefault() as ProfileViewController;

            profileControler.userId = userId;
            profileControler.isOwn = false;

            mainController.SelectedViewController = profileControler;
            mainController.Settings = ((MainTabNavController)ParentViewController).Settings;

            ShowViewController(mainController, this);
        }
    }
}