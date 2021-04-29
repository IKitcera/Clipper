using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Clipper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ClipperAndroid
{
    class HomeAdapter : BaseAdapter<PhotoPost>
    {
        List<PhotoPost> posts;
        Activity context;
        public HomeAdapter(Activity context, List<PhotoPost> posts) : base()
        {
            this.context = context;
            this.posts = posts;
        }
        public override PhotoPost this[int position] => posts[position];

        public override int Count => posts.Count;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var post = posts[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.activity_main, null);

            //TODO: Setting image into ImgeView
            //TODO: Saving imgs in resources & IW parsing

            view.FindViewById<ImageButton>(Resource.Id.userIcon).SetImageBitmap(GetImageBitmapFromUrl("https://docs.microsoft.com/ru-ru/xamarin/android/user-interface/controls/view-pager/images/01-intro.png#lightbox"));
            //TODO: Change this ImageView on ViewPager
            view.FindViewById<ImageView>(Resource.Id.postImage).SetImageBitmap(GetImageBitmapFromUrl(posts[position].Images[0]));
            view.FindViewById<TextView>(Resource.Id.reactionUp).Text = posts[position].Reactions.FindAll(r => r == Reaction.Positive).Count.ToString();
            view.FindViewById<TextView>(Resource.Id.reactionDown).Text = posts[position].Reactions.FindAll(r => r == Reaction.Negative).Count.ToString();
            view.FindViewById<TextView>(Resource.Id.txtBelow).Text = posts[position].TextBelow;
            return view;
        }
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

    }
}