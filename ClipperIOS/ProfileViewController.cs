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

        public string userId { get; set; }


        public ProfileViewController(IntPtr handle) : base(handle)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            userId = ((MainTabNavController)ParentViewController).Settings.GetUserID();

            profileViewModel = new ProfileViewModel(userId);

            postsCollectionView.ContentMode = UIViewContentMode.Center;

            postsCollectionView.DataSource = new ProfilePostsCollectionSource(profileViewModel.PhotoPosts.Select(x => x.Images[0]).ToList());

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
      
        public void UpdateTableSource()
        {
            profileViewModel = new ProfileViewModel(userId);
            postsCollectionView.DataSource = new ProfilePostsCollectionSource(profileViewModel.PhotoPosts.Select(x => x.Images[0]).ToList());
       
        }
    }
}