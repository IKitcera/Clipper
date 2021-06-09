using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using AndroidX.Core.App;
using Android;
using AndroidX.Core.Content;
using Android.Content.PM;

namespace ClipperA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        FragmentManager fm;
        UserSettings settings;
        Fragment currentF;

        const int PERMISSION_ALL = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            fm = this.SupportFragmentManager;
            settings = new UserSettings(this);
            currentF = new MainFlowFragment();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            switch (requestCode)
            {
                case PERMISSION_ALL:
                    {
                        bool storPermission = false;
                        if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
                        {
                            storPermission = true;
                        }
                        settings.saveStoragePermission(storPermission);
                 
                        bool camPermission = false;
                        if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
                        {
                            camPermission = true;
                        }
                        settings.saveCameraPermission(camPermission);
                        break;
                    }
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart()
        {
            base.OnStart();
            string [] Permissions = new string[]
            {
                 Manifest.Permission.WriteExternalStorage,
                 Manifest.Permission.ReadExternalStorage,
                 Manifest.Permission.Camera,
                 Manifest.Permission.Internet
            };
           
           ActivityCompat.RequestPermissions(this, Permissions, PERMISSION_ALL); 
           
            loadFragment(currentF);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            while (SupportFragmentManager.BackStackEntryCount != 0)
                SupportFragmentManager.PopBackStackImmediate();

            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    currentF = new MainFlowFragment();
                    loadFragment(currentF);
                    return true;
                case Resource.Id.navigation_plus:
                    currentF = new NewPostFragment();
                    loadFragment(currentF);
                    return true;
                case Resource.Id.navigation_profile:
                    currentF = new ProfileFragment();
                    loadFragment(currentF);
                    return true;
            }
            return false;
        }

        private bool loadFragment(Fragment fragment)
        {
            if (fragment != null)
            {
                AndroidX.Fragment.App.FragmentTransaction ft = fm.BeginTransaction();
                ft.SetCustomAnimations(Resource.Animation.fragment_open_enter, Resource.Animation.fragment_open_exit, Resource.Animation.abc_popup_enter, Resource.Animation.abc_popup_exit);
                ft.Replace(Resource.Id.fragmContainer, fragment).Commit();
                return true;
            }
            return false;
        }
        public void ResetMainFragment()
        {
            currentF = new MainFlowFragment();
            loadFragment(currentF);
        }
    }
}

