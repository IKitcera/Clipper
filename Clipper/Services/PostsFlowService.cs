using Clipper.Models;
using Clipper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipper.Services
{
    class PostsFlowService
    {
        readonly MockDataStoreService store;// = new MockDataStore();
        readonly Profile currentProfile;

        public PostsFlowService(string userId)
        {
            store = StorageService.GetStorage();

            currentProfile = store.Profiles.Where(p => p.UserId == userId).FirstOrDefault();
        }
        public List<PhotoPost> SetFlow()
        {
            var result = from profiles in store.Profiles
                         join subsribings in currentProfile.SubscribedId
                         on profiles.UserId equals subsribings
                         select profiles.PhotoPosts.OrderByDescending(post => post.CreatingTime).ToList();

            List<PhotoPost> resultList = new List<PhotoPost>();
            foreach(var firstD in result)
            {
                foreach(var secondD in firstD)
                {
                    resultList.Add(secondD);
                }
            }
            //resultList.
            return resultList;
        }
        public List<string> SetAvtrs(List<PhotoPost> posts)
        {
            var result = from post in posts
                         join profile in store.Profiles
                         on post.UserId equals profile.UserId
                         select profile.Avatar;
            return result.ToList();
        }
        public List<string> SetNicks(List<PhotoPost> posts)
        {
            var result = from post in posts
                         join user in store.Users
                         on post.UserId equals user.Id
                         select user.Name;
            return result.ToList();
        }
        public List<PhotoPost> GetPostsForUser()
        {
            return currentProfile.PhotoPosts.OrderBy(p => p.CreatingTime).ToList();
        }
        public string GetUserName()
        {
            var name = from user in store.Users
            where user.Id == currentProfile.UserId
            select user.Name;
            return name.FirstOrDefault();
        }
        public string GetUserAvtr()
        {
            return currentProfile.Avatar;
        }
    }
}
