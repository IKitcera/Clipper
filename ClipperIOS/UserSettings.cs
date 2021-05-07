using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    class UserSettings
    {
        private NSUserDefaults preferences = NSUserDefaults.StandardUserDefaults;

        private static string UserId = "UserId";
        private static string UserLogin = "UserLogin";
        private static string UserPassword = "UserPassword";
        private static string StoragePermission = "StoragePermission";
        private static string CameraPermission = "CameraPermission";
        private static string StayLogged = "StayLogged";
        public UserSettings()
        {
            preferences = NSUserDefaults.StandardUserDefaults;
        }

        public void saveUserID(string value)
        {
            preferences.SetString(UserId, value);
            preferences.Synchronize();
        }
        public void saveUserLogin(string value)
        {
            preferences.SetString(UserLogin, value);
            preferences.Synchronize();
        }
        public void saveUserPassword(string value)
        {
            preferences.SetString(UserPassword, value);
            preferences.Synchronize();
        }
        public void saveCameraPermission(bool value)
        {
            preferences.SetBool(value, CameraPermission);
            preferences.Synchronize();
        }
        public void saveStoragePermission(bool value)
        {
            preferences.SetBool(value, StoragePermission);
            preferences.Synchronize();
        }
        public void DoNotLogOut(bool value)
        {
            preferences.SetBool(value, StayLogged);
            preferences.Synchronize();
        }
        public string GetUserID()
        {
            return preferences.StringForKey(UserId);
        }
        public string GetUserLogin()
        {
            return preferences.StringForKey(UserLogin);
        }
        public string GetUserPassword()
        {
            return preferences.StringForKey(UserPassword);
        }
        public bool GetCameraPermission()
        {
            return preferences.BoolForKey(CameraPermission);
        }
        public bool GetStoragePermission()
        {
            return preferences.BoolForKey(StoragePermission);
        }
        public bool GetDoNotLogOut()
        {
            return preferences.BoolForKey(StayLogged);
        }
    }
}