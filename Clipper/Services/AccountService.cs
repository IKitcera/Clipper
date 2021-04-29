using Clipper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clipper.Services
{
    public class AccountService
    {
        private List<User> users;
        private User currentUser;
        public string UserId { get; private set; }
        public string Message { get; private set; } 
        public AccountService()
        {
            users = StorageService.GetStorage().Users;
        }
        private bool Identificate(string email)
        {
            currentUser = users.Find(u => u.Email == email); 
            if(currentUser != null)
                return true;
            Message = "Couldn't find this user";
            return false;
        }
        private bool Authentificate(string password)
        {
            if (currentUser.PasswordHash == password)
                return true;
            Message = "Incorrect password";
            return false;
        }
        public string Authorize(string email, string password)
        {
            if (Identificate(email))
                if (Authentificate(password))
                    return currentUser.Id;
            return "";
        }
    }
}
