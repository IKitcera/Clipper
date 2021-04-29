using Android;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.ViewPager.Widget;
using Bumptech.Glide;
using Clipper.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace ClipperA
{
    public class NewPostFragment : Fragment
    {
        private string userId;
        private AddViewModel addViewModel;
        private View first;
        private View second;
        private string path;
        private bool activeSecond = false;

        private AndroidX.RecyclerView.Widget.RecyclerView recView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            first = inflater.Inflate(Resource.Layout.NewPost, null);
            second = inflater.Inflate(Resource.Layout.NewPost2, null);

            userId = new UserSettings(this.Activity).getUserID();

            if (!activeSecond)
            {
                if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
                {
                    addViewModel = new AddViewModel();

                    ImageButton takePctrBtn = first.FindViewById<ImageButton>(Resource.Id.takePictureBtn);
                    ImageButton chooseFewBtn = first.FindViewById<ImageButton>(Resource.Id.fewPictures);

                    takePctrBtn.Click += (sender, e) => StartCameraActivity();
                    chooseFewBtn.Click += (sender, e) => ShowCheckCircles();

                    recView = first.FindViewById<AndroidX.RecyclerView.Widget.RecyclerView>(Resource.Id.recView);

                    var galleryPhotos = getPhotosFromGallery();

                    var imgAdapter = new ImageShortCutAdapter(this.Activity, galleryPhotos);

                    var grid = new AndroidX.RecyclerView.Widget.GridLayoutManager(this.Activity, 3);

                    recView.HasFixedSize = true;
                    recView.SetLayoutManager(grid);
                    recView.SetAdapter(imgAdapter);

                    imgAdapter.ItemClick += (sender, e) =>
                    {
                        int position = e;

                        if (imgAdapter.fewSelectActive)
                        {
                            var view = recView.GetChildAt(position);
                            var vh = recView.FindViewHolderForLayoutPosition(position);

                            if (view == null)
                                view = vh.ItemView;

                            if (!imgAdapter.CheckedPositions.Contains(position))
                            {
                                if (imgAdapter.CheckedPositions.Count >= 10)
                                    Toast.MakeText(this.Activity, "You could clip up to 10 photos", ToastLength.Short).Show();
                                else
                                    imgAdapter.CheckedPositions.Add(position);
                            }
                            else
                            {
                                imgAdapter.CheckedPositions.Remove(position);
                            }
                            imgAdapter.BindViewHolder(vh, position);
                        }
                        else
                        {
                            SelectedImgsFromGallery(new List<string>() {galleryPhotos[position] });
                        }
                    };
                }
                else
                {
                    ActivityCompat.RequestPermissions(this.Activity, new string[] { Manifest.Permission.ReadExternalStorage }, 200);
                    OnCreateView(inflater, container, savedInstanceState);
                }

                return first;
            }
            else
            {
                return second;
            }
        }

        private void StartCameraActivity()
        {
            Intent intent = new Intent(this.Activity, typeof(CameraActivity));
            int resultCode = 200;
            StartActivityForResult(intent, resultCode);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (data != null)
            {
                var view = second;
                Activity.SetContentView(view);

                path = data.GetStringExtra("imagePath");

                SelectedImgsFromGallery(new List<string>() { path });

            }
        }
        private List<string> getPhotosFromGallery()
        {
            List<string> listOfAllImages = new List<string>();
            int column_index_data, column_index_path;
            string PathOfImage = null;
            var uri = MediaStore.Images.Media.ExternalContentUri;

            var cursor = this.Activity.ContentResolver.Query(uri, null, null, null, "date_modified DESC");
            int idx = cursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);

            while (cursor.MoveToNext())
            {
                path = cursor.GetString(idx);
                listOfAllImages.Add(path);
            }

            return listOfAllImages;
        }
        private void AddNewPost(List<string> imgs)
        {
            if (addViewModel == null)
            {
                addViewModel = new AddViewModel();
            }
            addViewModel.Photos = imgs;
            addViewModel.Title = second.FindViewById<EditText>(Resource.Id.txtBelow).Text;

            if (addViewModel.AddNewPost(userId))
            {
                var intent = new Intent(this.Activity, typeof(MainActivity));
                this.Activity.Finish();
                StartActivity(intent);
            }
            else
            {
                var errorView = second.FindViewById<EditText>(Resource.Id.error);
                errorView.Visibility = ViewStates.Visible;
            }
        }

        private void ShowCheckCircles()
        {
            var adapter = recView.GetAdapter() as ImageShortCutAdapter;

            if (adapter != null)
            {
                ImageButton takePctrBtn = first.FindViewById<ImageButton>(Resource.Id.takePictureBtn);
                ImageButton doneBtn = first.FindViewById<ImageButton>(Resource.Id.doneBtn);

                doneBtn.Click += (sender, e) =>
                {
                    List<string> ImagesForPreview = new List<string>();

                    var imgs = getPhotosFromGallery();

                    foreach (var pos in adapter.CheckedPositions)
                        ImagesForPreview.Add(imgs.ElementAt(pos));

                    SelectedImgsFromGallery(ImagesForPreview);
                }; 

                if (takePctrBtn.Visibility == ViewStates.Visible)
                {
                    takePctrBtn.Visibility = ViewStates.Invisible;
                    doneBtn.Visibility = ViewStates.Visible;
                }
                else
                {
                    doneBtn.Visibility = ViewStates.Invisible;
                    takePctrBtn.Visibility = ViewStates.Visible;
                }
                adapter.ClickOnChoseFew();
            }
        }

        public void SelectedImgsFromGallery(List<string> imgs)
        {
            activeSecond = true;

            var pager = second.FindViewById<ViewPager>(Resource.Id.newPostPager);
            PhotoAdapter adapter = new PhotoAdapter(this.Activity, imgs);
            pager.Adapter = adapter;

            Activity.SetContentView(second);

            var postBtn = second.FindViewById<ImageButton>(Resource.Id.postBtn);
            postBtn.Click += (sender, e) => AddNewPost(imgs);

        }
    }
}