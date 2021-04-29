using Android.OS;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using Clipper.ViewModels;
using System.Linq;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace ClipperA
{
    public class ProfileFragment : Fragment
    {
        private string userId;
        private ProfileViewModel profileViewModel;
        private bool isOwnProfile;
        private bool postViewActive = false;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public ProfileFragment()
        {
            isOwnProfile = true;
        }

        public ProfileFragment(string userID)
        {
            isOwnProfile = false;
            userId = userID;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Profile, null);

            var postView = inflater.Inflate(Resource.Layout.Post, null);

            if (userId == null)
                userId = new UserSettings(this.Activity).getUserID();
            profileViewModel = new ProfileViewModel(userId);

            // if (!postViewActive)
            //{
            var avtrView = view.FindViewById<AbdulAris.Civ.CircleImageView>(Resource.Id.profileIcon);

            if (profileViewModel.Avatar != "")
            {
                Glide.With(this.Activity)
                    .Load(profileViewModel.Avatar)
                    .Into(avtrView);
            }
            else
            {
                Glide.With(this.Activity)
                    .Load("https://st3.depositphotos.com/1156795/35622/v/600/depositphotos_356226476-stock-illustration-profile-placeholder-image-gray-silhouette.jpg")
                    .Into(avtrView);
            }
            view.FindViewById<TextView>(Resource.Id.profileUserName).Text = profileViewModel.Name;
            view.FindViewById<TextView>(Resource.Id.profilePostsCount).Text = profileViewModel.PhotoPosts.Count.ToString();
            view.FindViewById<TextView>(Resource.Id.profileSubscribers).Text = profileViewModel.Subscribers.ToString();
            view.FindViewById<TextView>(Resource.Id.profileSubscribings).Text = profileViewModel.Subscribings.ToString();
            view.FindViewById<TextView>(Resource.Id.profileAbouUser).Text = profileViewModel.TextAbout;

            var recView = view.FindViewById<AndroidX.RecyclerView.Widget.RecyclerView>(Resource.Id.profilePosts);

            var galleryPhotos = profileViewModel.PhotoPosts.Select(p => p.Images.FirstOrDefault()).ToList();
            var imgAdapter = new ImageShortCutAdapter(this.Activity, galleryPhotos);
            var grid = new AndroidX.RecyclerView.Widget.GridLayoutManager(this.Activity, 3);

            recView.HasFixedSize = true;
            recView.SetLayoutManager(grid);
            recView.SetAdapter(imgAdapter);


            imgAdapter.ItemClick += (sender, e) =>
            {
                int position = e;
                postViewActive = true;

                var mfFragment = new MainFlowFragment(userId);
                mfFragment.specPosition = position;


                var mngr = this.Activity.SupportFragmentManager;
                var tag = "profile";
                var replacing = mngr.BeginTransaction().Replace(Resource.Id.fragmContainer, mfFragment);

                if (mngr.BackStackEntryCount == 0 || mngr.GetBackStackEntryAt(mngr.BackStackEntryCount - 1).Name != tag)
                    replacing.AddToBackStack(tag);
                replacing.Commit();
            };

            return view;
        }

        public override void OnDestroy()
        {
            if (postViewActive)          
                postViewActive = false;

            base.OnDestroy();
        }
    }
}
