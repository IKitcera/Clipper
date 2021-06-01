using Clipper.ViewModels;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            if (userId == null || userId == "")
            {
                Settings ??= ((MainTabNavController)ParentViewController).Settings;
                userId = Settings.GetUserID();
            }

            backBtn.TouchUpInside += (sender, e) => DismissViewController(false, null);

            if (!isOwn)
            {
                backBtn.Hidden = false;
                
            }

            profileViewModel ??= new ProfileViewModel(userId);

            postsCollectionView.ContentMode = UIViewContentMode.Center;

            var images = profileViewModel.PhotoPosts.Select(x => x.Images[0]).ToList();
            //images.Reverse();

            postsCollectionView.DataSource ??= new ProfilePostsCollectionSource(images, this);

            userNameLabel.Text = profileViewModel.Name;
            aboutMyselfLabel.Text = profileViewModel.TextAbout;

            UIImage avtr;
            if (profileViewModel.Avatar != "")
                avtr = ImageProcessing.ImgFromUrl(profileViewModel.Avatar);
            else
                avtr = ImageProcessing.ImgFromUrl("https://cdn1.vectorstock.com/i/thumb-large/72/35/male-avatar-profile-icon-round-man-face-vector-18307235.jpg");
            avtrImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            avtrImageView.Image = avtr;
            avtrImageView.Layer.CornerRadius = avtrImageView.Frame.Height/2;
            avtrImageView.ClipsToBounds = true;

            postsCount.Text = profileViewModel.PhotoPosts.Count.ToString();
            subscribersCount.Text = profileViewModel.Subscribers.ToString();
            subscribingsCount.Text = profileViewModel.Subscribings.ToString();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UISwipeGestureRecognizer SwipeRec = new UISwipeGestureRecognizer();
           
        }
        public async Task UpdateCollectionInBackground()
        {
            userId ??= Settings.GetUserID();

            profileViewModel = new ProfileViewModel(userId);

            var items = profileViewModel.PhotoPosts.Select(pp => pp.Images.ElementAt(0)).ToList();

            var images = await ImageProcessing.LoadImages(items);
            postsCollectionView.DataSource = new ProfilePostsCollectionSource(images, this);
        }

        public void ShowPostsFlow(NSIndexPath nspath)
        {
            var mainController = Storyboard.InstantiateViewController("MainNav") as MainTabNavController;
            var mainFlowController = mainController.ViewControllers.Where(controler => controler is MainFlowViewController).FirstOrDefault() as MainFlowViewController;

            mainFlowController.isOwn = true;

            mainFlowController.indexPath = nspath;

            var posts = profileViewModel.PhotoPosts;
           // posts.Reverse();
            mainFlowController.userId = posts[nspath.Row].UserId;

            mainController.SelectedViewController = mainFlowController;
            mainController.Settings = ((MainTabNavController)ParentViewController).Settings;

            ShowViewController(mainController, this);
        }
    }
}