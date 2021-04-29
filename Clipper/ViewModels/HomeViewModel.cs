using Clipper.Models;
using Clipper.Services;
using System.Collections.Generic;

namespace Clipper.ViewModels
{
    public class HomeViewModel
    {
        private PostsFlowService postsFlow;
        public List<PhotoPost> Flow { get; private set; } = new List<PhotoPost>();
        public List<string> UsersAvatars { get; private set; }
        public List<string> UsersNames { get; private set; }
        public HomeViewModel(string userId, bool isOwn)
        {
            postsFlow = new PostsFlowService(userId);

            if (!isOwn)
            {
                Flow = postsFlow.SetFlow();
                UsersAvatars = postsFlow.SetAvtrs(Flow);
                UsersNames = postsFlow.SetNicks(Flow);
            }
            else
            {
                Flow = postsFlow.GetPostsForUser();

                var avtr = postsFlow.GetUserAvtr();
                var name = postsFlow.GetUserName();

                UsersAvatars = new List<string>();
                UsersNames = new List<string>(); 

                foreach (var p in Flow)
                {
                    UsersAvatars.Add(avtr);
                    UsersNames.Add(name);
                }
            }
        }

    }
}
