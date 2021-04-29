using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClipperA
{
     class UserSettings
    {
       
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static String UserId = "UserId";
        private static String UserLogin = "UserLogin";
        private static String UserPassword = "UserPassword";
        private static String StoragePermission = "StoragePermission";
        private static String CameraPermission = "CameraPermission";
        private static String StayLogged = "StayLogged";
        public UserSettings(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveUserID(string value)
        {
            mPrefsEditor.PutString(UserId, value);
            mPrefsEditor.Commit();
        }
        public void saveUserLogin(string value)
        {
            mPrefsEditor.PutString(UserLogin, value);
            mPrefsEditor.Commit();
        }
        public void saveUserPassword(string value)
        {
            mPrefsEditor.PutString(UserPassword, value);
            mPrefsEditor.Commit();
        }
        public void saveCameraPermission(bool value)
        {
            mPrefsEditor.PutBoolean(CameraPermission, value);
            mPrefsEditor.Commit();
        }
        public void saveStoragePermission(bool value)
        {
            mPrefsEditor.PutBoolean(StoragePermission, value);
            mPrefsEditor.Commit();
        }
        public void DoNotLogOut(bool value)
        {
            mPrefsEditor.PutBoolean(StayLogged, value);
            mPrefsEditor.Commit();
        }
        public string getUserID()
        {
            return mSharedPrefs.GetString(UserId, "");
        }
        public string getUserLogin()
        {
            return mSharedPrefs.GetString(UserLogin, "");
        }
        public string getUserPassword()
        {
            return mSharedPrefs.GetString(UserPassword, "");
        }
        public bool getCameraPermission()
        {
            return mSharedPrefs.GetBoolean(CameraPermission, false);
        }
        public bool getStoragePermission()
        {
            return mSharedPrefs.GetBoolean(StoragePermission, false);
        }
        public bool getDoNotLogOut()
        {
            return mSharedPrefs.GetBoolean(StayLogged, false);
        }
    }
}