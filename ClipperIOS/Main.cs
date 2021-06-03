using System;
using CoreFoundation;
using UIKit;

namespace ClipperIOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");

        }
    }

    public static class Extensions
    {
        public static void ShowToast(this UIViewController controller, string Message, double DurationInSec)
        {
            var alert = new UIAlertController();

            alert.Message = Message;

            controller.PresentViewController(alert, true, null);

           // controller.BeginInvokeOnMainThread(() => System.Threading.Tasks.Task.Delay(2000).Wait());

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