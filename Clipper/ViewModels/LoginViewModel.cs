using Clipper.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Clipper.ViewModels
{
    public class LoginViewModel
    {
        private AccountService account;

        public string Email { get; set; }
        public string Password { get; set; }
        public string Message { get => account.Message; }

        public string TryLogin()
        {
            account = new AccountService();
            string userId = account.Authorize(Email, Password);
            if (userId != "")
                return userId;
            return "";

        }
    }
   
}
