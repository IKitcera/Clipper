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

            var index = indexPath.Row % 3;

            var a = collectionView.Frame.Width / 3.0;

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

            cell.Frame = new CGRect(offsetX, ((int)(indexPath.Row / 3.0)) * a, a, a);

            cell.img.ContentMode = UIViewContentMode.ScaleAspectFill;
            cell.img.Image = images[indexPath.Row];

            cell.clickBtn.TouchUpInside += (sender, e) => owner.ShowPostsFlow(indexPath);
            
            cell.SizeToFit();

            return cell;
        }

    }
}
