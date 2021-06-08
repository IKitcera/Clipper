using CoreGraphics;
using Foundation;
using Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace ClipperIOS
{
    [Register("NewPostViewController")]
    public partial class NewPostViewController : UIViewController
    {
        public List<UIImage> selectedImgs;
        public List<string> selectedUrl;

        private bool selectOne = true;

        private string userId;


        public NewPostViewController(IntPtr handle) : base(handle)
        { 
            
        }

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            userId = ((MainTabNavController)ParentViewController).Settings.GetUserID();

            if (!((MainTabNavController)ParentViewController).Settings.GetStoragePermission())
            {
                Photos.PHPhotoLibrary.RequestAuthorization((perm) =>
                {
                    if(perm == Photos.PHAuthorizationStatus.Authorized)
                    {
                        BeginInvokeOnMainThread(() =>
                            ((MainTabNavController)ParentViewController).Settings.saveStoragePermission(true));
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
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            Init();
        }

        public override void ViewDidDisappear(bool animated)
        {
            Reset();

            base.ViewDidDisappear(animated);
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
                    editController.userId = userId;
                }
            }
            else if(segue.Identifier == "CameraCapture")
            {
                var cameraController = segue.DestinationViewController as CameraViewController;

                if(cameraController != null)
                {
                    cameraController.userId = userId;
                }
            }
        }
        #endregion

        #region Methods

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
                ((CollectionViewSource)libraryPhotos.DataSource).UncheckAll();

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


                    takePictureBtn.Hidden = false;
                    doneManyBtn.Hidden = true;
                }
            };

            doneManyBtn.TouchUpInside += (sender, e) => PerformSegue("PostEditing", this);
            takePictureBtn.TouchUpInside += (sender, e) => PerformSegue("CameraCapture", this);
        }

        public void Reset()
        {
            selectedImgs = new List<UIImage>();
            selectedUrl = new List<string>();

            selectOne = true;
            takePictureBtn.Hidden = false;
            doneManyBtn.Hidden = true;

            ((CollectionViewSource)libraryPhotos.DataSource).UncheckAll();
        }

        public void PreparePost(UIImage image, UIButton checkBtn)
        {
            if (selectOne)
            {
                selectedImgs = new List<UIImage>() { image};

                PerformSegue("PostEditing", this);
            }
            else
            {
                
                if (!selectedImgs.Contains(image))
                {
                    if (selectedImgs.Count == 10)
                        (this as UIViewController).ShowToast("You could clip up to 10 photos", 3);
                    else
                    {
                        selectedImgs.Add(image);
                        checkBtn.Hidden = false;
                    }
                }
                else
                {
                    selectedImgs.Remove(image);
                    checkBtn.Hidden = true;
                }
            }
        }
    }

    #endregion

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
                images.Add(ImageProcessing.ImgFromUrl(img));
        }
        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            
            var cell = (PhotoShortCut)collectionView.DequeueReusableCell("collectionCell", indexPath);

            var index = indexPath.Row % 3;

            var a = collectionView.Frame.Width/3.0;

            double offsetX = 0;
            switch (index)
            {
                case 0:
                    offsetX = 0;
                    break;
                case 1:
                    offsetX = a;
                    break;
                case 2:
                    offsetX = 2 * a;
                    break;
            }

            cell.Frame = new CGRect(offsetX, ((int)(indexPath.Row / 3.0))*a, a, a);

            imageMngr.RequestImageForAsset(
                (PHAsset)result[(int)indexPath.Row],
                new CoreGraphics.CGSize(2000, 2000),
                PHImageContentMode.AspectFit,
                new PHImageRequestOptions(),
                (img, info) =>
                {
                    cell.img.ContentMode = UIViewContentMode.ScaleAspectFill;
                    cell.img.Image = img;
                });

            cell.imgBtn.TouchUpInside += (sender, e) => postViewController.PreparePost(cell.img.Image, cell.check);
            
            cells.Add(cell);

            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return result.Count;
        }
        public void UncheckAll()
        {
            cells.FindAll(x => x.check.Hidden == false).ForEach((cell) => cell.check.Hidden = true);
        }
    }
}