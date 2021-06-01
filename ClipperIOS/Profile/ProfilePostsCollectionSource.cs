using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace ClipperIOS
{
    public class ProfilePostsCollectionSource : UICollectionViewSource
    {
        List<UIImage> images;
        ProfileViewController owner;
        public ProfilePostsCollectionSource(List<UIImage> images, ProfileViewController profileController)
        {
            this.images = images;
            this.owner = profileController;
        }
        public ProfilePostsCollectionSource(List<string> urls, ProfileViewController profileController)
        {
            images = ImageProcessing.LoadImages(urls).Result;

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
