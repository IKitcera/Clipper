using Clipper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clipper.Services
{
    public class MainFlowActivityProcess
    {
        readonly MockDataStoreService storage;
        private List<PhotoPost> flow;

        public MainFlowActivityProcess(List<PhotoPost> flow)
        {
            this.flow = flow;
            storage = StorageService.GetStorage();
        }

        public void SetReaction(ReactionItem reactionItem)
        {
            var postReactions = flow.Find(post => post.Id == reactionItem.PostId).Reactions;
            var existedReactionByTheUser = postReactions.Find(rctn => rctn.UserLeftedId == reactionItem.UserLeftedId);

            if (existedReactionByTheUser != null)
            {
                if (existedReactionByTheUser.Reaction == reactionItem.Reaction)
                {
                    postReactions.Remove(existedReactionByTheUser);

                    /*
                    var theReaction = storage.Profiles.Select(p => p.PhotoPosts.Select(pp => pp.Reactions.Where(r => r.UserLeftedId == reactionItem.UserLeftedId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                    if (theReaction != null)
                    {
                        var r = storage.Profiles.Select(p => p.PhotoPosts.Select(pp => pp.Reactions).FirstOrDefault()).FirstOrDefault().Remove(theReaction);
                    }
                    */
                }
                else
                {

                   // existedReactionByTheUser = reactionItem;
                   
                    postReactions.Remove(existedReactionByTheUser);
                    postReactions.Add(reactionItem);

                    /*
                    var theReaction = storage.Profiles.Select(p => p.PhotoPosts.Select(pp => pp.Reactions.Where(r => r.UserLeftedId == reactionItem.UserLeftedId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                    if (theReaction == null)
                    {
                        storage.Profiles.Select(p => p.PhotoPosts.Select(pp => pp.Reactions).FirstOrDefault()).FirstOrDefault().Add(theReaction);
                    }
                    */
                    //Maybe writ to stor also
                }
            }
            else
            {
                postReactions.Add(reactionItem);
            }
        }
    }
}
