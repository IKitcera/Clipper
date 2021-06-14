using Android.OS;
using Android.Views;
using Android.Widget;
using Clipper.Services;
using Clipper.ViewModels;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace ClipperA
{
    public class MainFlowFragment : Fragment
    {
        public int? specPosition { get; set; } = null;
        public string userId { get; private set; }

        private HomeViewModel homeViewModel;
        private ListView listView;

        private HomeAdapter adapter;
        private bool isOwn;
        public MainFlowFragment()
        {
            isOwn = false;
        }
        public MainFlowFragment(string userId)
        {
            this.userId = userId;
            isOwn = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.MainFlow, null);

            if (userId == null)
                userId = new UserSettings(this.Activity).getUserID();
            homeViewModel = new HomeViewModel(userId, isOwn);
            listView = view.FindViewById<ListView>(Resource.Id.listView);

            adapter = new HomeAdapter(this, homeViewModel);
            listView.Adapter = adapter;

            adapter.ItemClick += (id) =>
                {
                    var pfFragment = new ProfileFragment(id);


                    var mngr = this.Activity.SupportFragmentManager;
                    var tag = "flow";
                    var replacing = mngr.BeginTransaction().Replace(Resource.Id.fragmContainer, pfFragment);
                    if (mngr.BackStackEntryCount == 0 || mngr.GetBackStackEntryAt(mngr.BackStackEntryCount - 1).Name != tag)
                        replacing.AddToBackStack(tag);
                    replacing.Commit();

                };
            adapter.LeftReactionClick += (reactionItm) =>
            {
                var mainFrowActivityService = new MainFlowActivityProcess(homeViewModel.Flow);
                mainFrowActivityService.SetReaction(reactionItm);
                adapter.NotifyDataSetInvalidated();
                adapter.NotifyDataSetChanged();
            };
            if (specPosition != null)
                listView.SetSelectionFromTop((int)specPosition, 0);
            return view;
        }
    }
}