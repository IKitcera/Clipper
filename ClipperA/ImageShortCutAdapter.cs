using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Load.Engine;
using Bumptech.Glide.Module;
using Bumptech.Glide.Request;
using Java.Interop;
using Java.Util.Logging;
using System;
using System.Collections.Generic;

namespace ClipperA
{
    class ImageShortCutAdapter : RecyclerView.Adapter
    {
        List<string> images;
        public List<int> CheckedPositions { get; private set; }

        Activity context;

        public bool fewSelectActive = false;

        public event EventHandler<int> ItemClick;

        List<RecyclerHolder> holders = new List<RecyclerHolder>();

        public ImageShortCutAdapter(Activity context, List<string> imagesUrl)
        {
            images = imagesUrl;
            this.context = context;

            CheckedPositions = new List<int>();
        }
        public override int ItemCount => images.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = images[position];

            var holder = viewHolder as RecyclerHolder;

            RequestOptions ro = new RequestOptions().CenterCrop();
            var glide = Glide.With(context).AsBitmap().Apply(ro).Load(item);
            glide.DiskCacheStrategy_T(DiskCacheStrategy.None);
            glide.Placeholder(Resource.Drawable.outline_file_upload_24).Error(Resource.Drawable.outline_error_24);
            glide.Into(holder.image);

            if (CheckedPositions.Contains(position))
                holder.checkBtn.Visibility = ViewStates.Visible;
            else
                holder.checkBtn.Visibility = ViewStates.Invisible;
           
            holders.Add(holder);

        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewtype)
        {
            View view = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.photoShCut, parent, false);

            RecyclerHolder vh = new RecyclerHolder(view, OnClick);

            return vh;
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        public void ClickOnChoseFew()
        {
            if (!fewSelectActive)
            {
                fewSelectActive = true;
            }
            else
            {
                fewSelectActive = false;

                foreach (var h in holders)
                    h.checkBtn.Visibility = ViewStates.Invisible;
            }
        }
    }

    class RecyclerHolder : RecyclerView.ViewHolder
    {
        public ImageView image  { get; set; }
        public ImageButton checkBtn { get; private set; }
        public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
        {
            image = itemView.FindViewById<ImageView>(Resource.Id.img);
            checkBtn = itemView.FindViewById<ImageButton>(Resource.Id.imgSelected);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);

        }
 
    }

}
