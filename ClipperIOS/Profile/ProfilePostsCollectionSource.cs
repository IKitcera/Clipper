using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace ClipperIOS
{
    public class ProfilePostsCollectionSource : UICollectionViewSource
    {
        List<UIImage> images;
        ProfileViewController owner;
        public ProfilePostsCollectionSource(List<string> imgs, ProfileViewController profileController)
        {
            images = new List<UIImage>();

            foreach(var img in imgs)
            {
                var uiimage = ImageProcessing.ImgFromUrl(img);

                if (uiimage == null)
                    uiimage = ImageProcessing.DecodeImg(img);

                images.Add(uiimage);
            }

            owner = profileController;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return images.Count;
        }


        public override UICollectionViewCell GetCell(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
        {
            var cell = (ProfileImgShortCell)collectionView.DequeueReusableCell("profilePostsViewCell", indexPath);

            cell.img.ContentMode = UIViewContentMode.ScaleAspectFill;
            cell.img.Image = images[indexPath.Row];

            cell.clickBtn.TouchUpInside += (sender, e) => owner.ShowPostsFlow(indexPath);
            
            cell.SizeToFit();

            return cell;
        }
    }
}
