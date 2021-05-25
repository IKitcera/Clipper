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
            
           // if (cell == null)
               // cell = new PostsViewCell();

            UIImage avtr;
            if (avtrs[indexPath.Row] != "")
                avtr = ImgFromUrl(avtrs[indexPath.Row]);
            else
                avtr = ImgFromUrl("https://cdn1.vectorstock.com/i/thumb-large/72/35/male-avatar-profile-icon-round-man-face-vector-18307235.jpg");
            var nick = nicks[indexPath.Row];
            var post = posts[indexPath.Row];

            List<UIImage> images = new List<UIImage>();

            nfloat maxImgHeight = 0;
            foreach (var img in post.Images)
            {
                // images.Add(ImgFromUrl(img));
                var image = ImgFromUrl(img);
                var iv = cell.imageview;
                iv.Image = image;
                cell.scroll.AddSubview(iv);

                if (maxImgHeight < iv.Image.Size.Height)
                    maxImgHeight = iv.Image.Size.Height;
            }

            cell.pageControl.Pages = post.Images.Count;

            // cell.scroll.ContentSize = new CoreGraphics.CGSize(cell.scroll.ContentSize.Width, maxImgHeight);
            cell.scroll.Frame = new CoreGraphics.CGRect(
                0,0,cell.scroll.Frame.Width, maxImgHeight
                );
            cell.scroll.BackgroundColor = UIColor.Brown;
            cell.avtr.Image = avtr;
            cell.userName.Text = nick;
            //cell.SetPhotos(images);
            

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
    }
}