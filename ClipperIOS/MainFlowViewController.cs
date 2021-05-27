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
    public partial class MainFlowViewController :  UIViewController
    {
        HomeViewModel homeViewModel;
        public string userId;
        public UserSettings Settings;
        public bool isOwn { get; set; } = false;

        public MainFlowViewController(IntPtr handle):base(handle)
        {
           
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Settings = ((MainTabNavController)ParentViewController).Settings;
            userId = Settings.GetUserID();

            homeViewModel = new HomeViewModel(userId, isOwn);

            table.Source = new PostsTableSource(homeViewModel);

            var permissions = Photos.PHPhotoLibrary.AuthorizationStatus;
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
}