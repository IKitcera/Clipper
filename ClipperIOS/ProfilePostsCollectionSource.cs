using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace ClipperIOS
{
    public class ProfilePostsCollectionSource : UICollectionViewSource
    {
        List<UIImage> images;

        public ProfilePostsCollectionSource(List<string> imgs)
        {
            images = new List<UIImage>();

            foreach(var img in imgs)
            {
                var uiimage = ImageProcessing.ImgFromUrl(img);

                if (uiimage == null)
                    uiimage = ImageProcessing.DecodeImg(img);

                images.Add(uiimage);
            }
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

            cell.SizeToFit();

            return cell;
        }
    }
}
