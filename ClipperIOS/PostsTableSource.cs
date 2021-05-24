using Clipper.Models;
using Clipper.ViewModels;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using System.Net;
using System.IO;

namespace ClipperIOS
{
    [Register("PostsTableController")]
    public class PostsTableSource : UITableViewSource
    {
        List<PhotoPost> posts;
        List<string> avtrs;
        List<string> nicks;
       
        public event Action<string> ItemClick;
        Action<string> listener;
      
        public PostsTableSource(HomeViewModel home)
        {
            posts = home.Flow;
            avtrs = home.UsersAvatars;
            nicks = home.UsersNames;

            listener = OnClick;
        }

        private void OnClick(string obj)
        {
            throw new NotImplementedException();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");

            var artr = avtrs[indexPath.Row];
            var nick = nicks[indexPath.Row];
            var post = posts[indexPath.Row];

            List<UIImage> images = new List<UIImage>();

            foreach(var img in post.Images)
            {
                images.Add(ImgFromUrl(img));
            }
            //cell.avtr = avtr;
            //cell.nick = nick;
            //cell.photos = photos;
            //cell.txtBelow = txtBelow;
            //cell.comment = comment;
            //cell.reactionUp = reactionUp;
            //cell.reactionDown = reactionDown;

            return cell;
            
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return posts.Count;
        }
        static UIImage ImgFromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data); 
        }
    }
}