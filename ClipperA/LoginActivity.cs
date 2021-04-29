using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClipperA.Resources
{
    [Activity(Label = "@string/app_name", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Login : AppCompatActivity
    {
        Clipper.ViewModels.LoginViewModel login = new Clipper.ViewModels.LoginViewModel();
        UserSettings settings;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Login);
            Button button = FindViewById<Button>(Resource.Id.lgnButton);
            button.Click += (sender, e) => LoginClick();
        }

        protected override void OnStart()
        {
            base.OnStart();

            settings = new UserSettings(this);

            if (settings.getDoNotLogOut())
            {
                if(settings.getUserID() != "")
                {
                    StartActivity(typeof(MainActivity));
                    Finish();
                }
            }
            FindViewById<EditText>(Resource.Id.Uemail).Text = settings.getUserLogin();
            FindViewById<EditText>(Resource.Id.Upassword).Text = settings.getUserPassword();
            FindViewById<CheckBox>(Resource.Id.logoutCB).Checked = settings.getDoNotLogOut();

        }
        public void LoginClick()
        {

            login.Email = FindViewById<EditText>(Resource.Id.Uemail).Text;
            login.Password = FindViewById<EditText>(Resource.Id.Upassword).Text;

            var userId = login.TryLogin();
            if(userId != "")
            {
                //Intent intent = new Intent(this, typeof(MainActivity));
                //Bundle bundle = new Bundle();
                //intent.PutExtra("userId", userId);

                var cb = FindViewById<CheckBox>(Resource.Id.logoutCB);

                settings.saveUserLogin(login.Email);
                settings.saveUserPassword(login.Password);
                  settings.saveUserID(userId);

                if (FindViewById<CheckBox>(Resource.Id.logoutCB).Checked)
                {
                    settings.DoNotLogOut(true);
                }

                //StartActivity(intent);

                StartActivity(typeof(MainActivity));
                Finish();
            }
            else
            {
                FindViewById<TextView>(Resource.Id.lgnError).Visibility = ViewStates.Visible;
                FindViewById<TextView>(Resource.Id.lgnError).Text = login.Message;
            }
        }
    }
}