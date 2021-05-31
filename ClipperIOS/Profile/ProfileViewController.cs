using Clipper.ViewModels;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("ProfileViewController")]
    public partial class ProfileViewController : UIViewController
    {
        private ProfileViewModel profileViewModel;

        public UserSettings Settings { get; set; }
        public string userId { get; set; }
        public bool isOwn = true;

        public ProfileViewController(IntPtr handle) : base(handle)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (isOwn)
            {
                Settings = ((MainTabNavController)ParentViewController).Settings;
                userId = Settings.GetUserID();
            }
            else
            {
                backBtn.Hidden = false;
                backBtn.TouchUpInside += (sender, e) => DismissViewController(false, null);
            }
            profileViewModel = new ProfileViewModel(userId);

            postsCollectionView.ContentMode = UIViewContentMode.Center;

            postsCollectionView.DataSource = new ProfilePostsCollectionSource(profileViewModel.PhotoPosts.Select(x => x.Images[0]).ToList(), this);

            userNameLabel.Text = profileViewModel.Name;
            aboutMyselfLabel.Text = profileViewModel.TextAbout;

            UIImage avtr;
            if (profileViewModel.Avatar != "")
                avtr = ImageProcessing.ImgFromUrl(profileViewModel.Avatar);
            else
                avtr = ImageProcessing.ImgFromUrl("https://cdn1.vectorstock.com/i/thumb-large/72/35/male-avatar-profile-icon-round-man-face-vector-18307235.jpg");
            avtrImageView.Image = avtr;
            avtrImageView.Layer.CornerRadius = avtrImageView.Frame.Size.Height / 2;
            avtrImageView.ClipsToBounds = true;

            postsCount.Text = profileViewModel.PhotoPosts.Count.ToString();
            subscribersCount.Text = profileViewModel.Subscribers.ToString();
            subscribingsCount.Text = profileViewModel.Subscribings.ToString();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            postsCount.Text = profileViewModel.PhotoPosts.Count.ToString();
            postsCollectionView.DataSource = new ProfilePostsCollectionSource(profileViewModel.PhotoPosts.Select(x => x.Images[0]).ToList(), this);
    
        }
        public void ShowPostsFlow(NSIndexPath nspath)
        {
            var mainFlowController = Storyboard.InstantiateViewController("MainFlowController") as MainFlowViewController;
            if(Settings == null)
                Settings = ((MainTabNavController)ParentViewController).Settings;

            if(isOwn)
                mainFlowController.isOwn = true;
           
            mainFlowController.indexPath = nspath;
            mainFlowController.Settings = Settings;
            //PresentViewController(mainFlowController,false, null);
            ShowViewController(mainFlowController, this);
            
        }
    }
}