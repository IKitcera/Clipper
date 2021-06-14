using Clipper.Models;
using Clipper.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clipper.ViewModels
{
    public class AddViewModel
    {
        PhotoPost photoPost;
        public List<string> Photos { set => photoPost.Images = value; }
        public string Title { set => photoPost.TextBelow = value; }
        public AddViewModel()
        {
            photoPost = new PhotoPost();
            photoPost.Images = new List<string>();
        }
        public bool AddNewPost(string userId)
        {
            photoPost.UserId = userId;
            photoPost.CreatingTime = DateTime.Now;
            if (photoPost.Images.Count == 0)
                return false;
            photoPost.UserId = userId;
            photoPost.Comments = new List<Comment>();
            photoPost.Reactions = new List<ReactionItem>();
            StorageService.GetStorage().Profiles.Where(p => p.UserId == userId).FirstOrDefault().PhotoPosts.Add(photoPost);
            return true;
        }
    }

}
