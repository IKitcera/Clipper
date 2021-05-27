// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ClipperIOS
{
	partial class NewPostViewController
	{
		[Outlet]
		UIKit.UIButton doneManyBtn { get; set; }

		[Outlet]
		UIKit.UICollectionView libraryPhotos { get; set; }

		[Outlet]
		UIKit.UIButton selectManyBtn { get; set; }

		[Outlet]
		UIKit.UIButton takePictureBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (libraryPhotos != null) {
				libraryPhotos.Dispose ();
				libraryPhotos = null;
			}

			if (selectManyBtn != null) {
				selectManyBtn.Dispose ();
				selectManyBtn = null;
			}

			if (takePictureBtn != null) {
				takePictureBtn.Dispose ();
				takePictureBtn = null;
			}

			if (doneManyBtn != null) {
				doneManyBtn.Dispose ();
				doneManyBtn = null;
			}
		}
	}
}
