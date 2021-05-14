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

            List<>
            foreach(var img in post.Images)
            {
                var image = ImgFromUrl(img);
            }
            
            wc.DownloadFile()
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