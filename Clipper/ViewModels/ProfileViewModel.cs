using Clipper.Models;
using Clipper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipper.ViewModels
{
    public class ProfileViewModel
    {
        Models.Profile profile;
        public string Name { get; private set; }
        public string Avatar { get => profile.Avatar; set => profile.Avatar = value; }
        public string TextAbout { get => profile.TextAbout; set => profile.TextAbout = value; }
        public List<PhotoPost> PhotoPosts { get => profile.PhotoPosts; set => profile.PhotoPosts = value; }

        public int Subscribers { get => profile.SubscribersId.Count; }
        public int Subscribings { get => profile.SubscribedId.Count; }
       
        public ProfileViewModel(string userId)
        {
            profile = StorageService.GetStorage().Profiles.Where(p => p.UserId == userId).FirstOrDefault();
            Name = StorageService.GetStorage().Users.Where(u => u.Id == userId).FirstOrDefault().Name;
        }
    }
}
