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
    [Register("TableSource")]
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
            var cell = (PostsViewCell)tableView.DequeueReusableCell("cell", indexPath);

            UIImage avtr;
            if (avtrs[indexPath.Row] != "")
                avtr = ImgFromUrl(avtrs[indexPath.Row]);
            else
                avtr = ImgFromUrl("https://cdn1.vectorstock.com/i/thumb-large/72/35/male-avatar-profile-icon-round-man-face-vector-18307235.jpg");

            var nick = nicks[indexPath.Row];
            var post = posts[indexPath.Row];

            
            if (post.Images.Count == 1)
                cell.scroll.ScrollEnabled = false;

            cell.scroll.ContentSize = new CoreGraphics.CGSize(cell.scroll.VisibleSize.Width * (nfloat)post.Images.Count, cell.scroll.VisibleSize.Height);
      
            foreach (var img in post.Images)
            { 
                var image = ImgFromUrl(img);

                var i = post.Images.IndexOf(img);
                var iv = new UIImageView(new CoreGraphics.CGRect(cell.scroll.VisibleSize.Width * (nfloat)i, 0, cell.scroll.VisibleSize.Width, cell.scroll.VisibleSize.Height));

                iv.Image = image;
                iv.ContentMode = UIViewContentMode.ScaleAspectFit;
                
                cell.scroll.AddSubview(iv);
            }

            cell.pageControl.Pages = post.Images.Count;
            cell.scroll.Scrolled += (sender, e) =>
            {
                var sc = sender as UIScrollView;

                var offset = sc.ContentOffset.X;

                cell.pageControl.CurrentPage = cell.pageControl.Pages - ((int)Math.Round(sc.ContentSize.Width / (offset + sc.VisibleSize.Width)));
            };

            cell.avtr.Image = avtr;
            cell.avtr.Layer.CornerRadius = cell.avtr.Frame.Size.Height / 2;
            cell.avtr.ClipsToBounds = true;

            cell.userName.Text = nick;

            cell.txtBelow.Text = post.TextBelow;
            cell.comment.Text = post.Comments.Count.ToString();

            var countOfPositiveReactions = post.Reactions.FindAll(x => x == Reaction.Positive).Count;
            cell.reactionUp.Text = countOfPositiveReactions.ToString();
            cell.reactionDown.Text = (post.Reactions.Count - countOfPositiveReactions).ToString();

            cell.SizeToFit();
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
        static
       
    }

}