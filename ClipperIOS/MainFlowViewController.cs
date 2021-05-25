using Clipper.ViewModels;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("MainFlowViewController")]
    public partial class MainFlowViewController :  UIViewController
    {
        HomeViewModel homeViewModel;
        public string userId;

        public bool isOwn { get; set; } = false;

        public MainFlowViewController(IntPtr handle):base(handle)
        {
           
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            userId = ((MainTabNavController)ParentViewController).Settings.GetUserID();

            homeViewModel = new HomeViewModel(userId, isOwn);
            //table = new UITableView();
            table.Source = new PostsTableSource(homeViewModel);
         //   table.RowHeight = 500f;
          //  table.EstimatedRowHeight = 5f;
          //  table.ReloadData();
        }
    }
}