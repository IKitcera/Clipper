using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    public class UserSettings
    {
        private NSUserDefaults preferences = new NSUserDefaults();

        private static string UserId = "userid";
        private static string UserLogin = "userlogin";
        private static string UserPassword = "userpassword";
        private static string StoragePermission = "storagepermission";
        private static string CameraPermission = "camerapermission";
        private static string StayLogged = "staylogged";

        public void saveUserID(string value)
        {
            preferences.SetString(value, UserId);
            preferences.Synchronize();
        }
        public void saveUserLogin(string value)
        {
            preferences.SetString(value, UserLogin);
            preferences.Synchronize();
        }
        public void saveUserPassword(string value)
        {
            preferences.SetString(value, UserPassword);
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
        public string GetUserID() => preferences.StringForKey(UserId);
        public string GetUserLogin() => preferences.StringForKey(UserLogin);
        public string GetUserPassword() => preferences.StringForKey(UserPassword);
        public bool GetCameraPermission() => preferences.BoolForKey(CameraPermission);
        public bool GetStoragePermission() => preferences.BoolForKey(StoragePermission);
        public bool GetDoNotLogOut() => preferences.BoolForKey(StayLogged);
    }
}