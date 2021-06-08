using System;
using CoreFoundation;
using UIKit;

namespace ClipperIOS
{
    public static class Extensions
    {
        public static void ShowToast(this UIViewController controller, string Message, double DurationInSec)
        {
            var alert = new UIAlertController();

            alert.Message = Message;

            controller.PresentViewController(alert, true, null);

            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Default).DispatchAfter(
                new DispatchTime(DispatchTime.Now,
                                 new TimeSpan(0, 0, 0, (int)DurationInSec, (int)(DurationInSec - (Math.Round(DurationInSec, 3) * 1000)))),
                () =>
                {
                    System.Threading.Tasks.Task.Delay(1500).Wait();
                    controller.BeginInvokeOnMainThread(() => alert.DismissViewController(true, null));
                });
        }
    }
}
