using CoreGraphics;
using Foundation;
using Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("NewPostViewController")]
    public partial class NewPostViewController : UIViewController
    {
        public List<UIImage> selectedImgs;
        public List<string> selectedUrl;

        private bool selectOne = true;

        public string userId { get; set; }


        public NewPostViewController(IntPtr handle) : base(handle)
        { 

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (!((MainTabNavController)ParentViewController).Settings.GetStoragePermission())
            {
                Photos.PHPhotoLibrary.RequestAuthorization((perm) =>
                {
                    if(perm == Photos.PHAuthorizationStatus.Authorized)
                    {
                        ((MainTabNavController)ParentViewController).Settings.saveStoragePermission(true);
                        Init();
                    }
                    else
                    {
                        DismissViewController(true, null);
                    }
                });
            }
            else
            {
                Init();
                
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if(segue.Identifier == "PostEditing")
            {
                var editController = segue.DestinationViewController as EditingPostViewController;

                if(editController != null)
                {
                    editController.images = selectedImgs;
                    editController.uris = selectedUrl;
                    
                }
            }
        }

        private void Init()
        {
            var options = new PHFetchOptions();
            options.SortDescriptors = new NSSortDescriptor[]{
                new NSSortDescriptor("creationDate", false) };
            var fetchResults = PHAsset.FetchAssets(PHAssetMediaType.Image, options);

            libraryPhotos.DataSource = new CollectionViewSource(fetchResults, this);

            selectedImgs = new List<UIImage>();
            selectedUrl = new List<string>();

            selectManyBtn.TouchUpInside += (sender, e) =>
            {
                if (selectOne)
                {
                    selectOne = false;

                    takePictureBtn.Hidden = true;
                    doneManyBtn.Hidden = false;
                }
                else
                {
                    selectOne = true;
                    selectedImgs = new List<UIImage>();
                    selectedUrl = new List<string>();

                    ((CollectionViewSource)libraryPhotos.DataSource).UncheckAll();

                    takePictureBtn.Hidden = false;
                    doneManyBtn.Hidden = true;
                }
            };

            doneManyBtn.TouchUpInside += (sender, e) => PerformSegue("PostEditing", this);
        }

        public void PreparePost(UIImage image, UIButton checkBtn, string path)
        {
            if (selectOne)
            {
                if (selectedImgs.Count > 0)
                {
                     selectedImgs = new List<UIImage>();
                     selectedUrl = new List<string>();
                }

                selectedImgs.Add(image);
                selectedUrl.Add(path);

                PerformSegue("PostEditing", this);
            }
            else
            {
                if (!selectedImgs.Contains(image))
                {
                    selectedImgs.Add(image);
                    selectedUrl.Add(path);

                    checkBtn.Hidden = false;
                }
                else
                {
                    selectedImgs.Remove(image);
                    selectedUrl.Remove(path);

                    checkBtn.Hidden = true;
                }
            }
        }
    }



    public class CollectionViewSource : UICollectionViewDataSource
    {
        List<UIImage> images;
        List<PhotoShortCut> cells = new List<PhotoShortCut>();

        PHCachingImageManager imageMngr = new PHCachingImageManager();
        PHFetchResult result;
        NewPostViewController postViewController;

        public CollectionViewSource(PHFetchResult result, UIViewController owner)
        {
            this.result = result;
            postViewController = (NewPostViewController)owner;
        }
        public CollectionViewSource(List<string> photos)
        {
            images = new List<UIImage>();

            foreach (var img in photos)
                images.Add(ImgFromUrl(img));
        }
        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            
            var cell = (PhotoShortCut)collectionView.DequeueReusableCell("collectionCell", indexPath);

            NSDictionary inf = new NSDictionary();

            imageMngr.RequestImageForAsset(
                (PHAsset)result[(int)indexPath.Row],
                new CoreGraphics.CGSize(200, 200),
                PHImageContentMode.AspectFit,
                new PHImageRequestOptions(),
                (img, info) =>
                {
                    cell.img.ContentMode = UIViewContentMode.ScaleAspectFill;
                    cell.img.Image = img;
                    inf = info;
                });

            string path = (NSString)((PHAsset)result[(int)indexPath.Row]).ValueForKey((NSString)"filename");

            cell.imgBtn.TouchUpInside += (sender, e) => postViewController.PreparePost(cell.img.Image, cell.check, path);

            cells.Add(cell);

            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return result.Count;
        }

        static UIImage ImgFromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        static string ImgToUrl(UIImage image)
        {
            var data = image.AsPNG();
            return data.GetBase64EncodedString(new NSDataBase64EncodingOptions());

        }

        public void UncheckAll()
        {
            cells.FindAll(x => x.check.Hidden == false).ForEach((cell) => cell.check.Hidden = true);
        }
    }
}