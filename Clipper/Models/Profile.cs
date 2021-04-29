using System;
using System.Collections.Generic;
using System.Text;

namespace Clipper.Models
{
     public class Profile
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        //public byte [] UserImage { get; set; }
        public string Avatar { get; set; }
        public string TextAbout { get; set; }
        public List<PhotoPost> PhotoPosts { get; set; }
        public List<string> SubscribersId { get; set; } //UserIds
        public List<string> SubscribedId { get; set; }
    }
}
