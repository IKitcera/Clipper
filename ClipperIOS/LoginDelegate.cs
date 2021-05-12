using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ClipperIOS
{
    [Register("LoginDelegate")]
    public class LoginDelegate: UIResponder, IUIWindowSceneDelegate
    {
		[Export("window")]
		public UIWindow Window { get; set; }
		public static UIStoryboard Storyboard = UIStoryboard.FromName("LoginStoryboard", null);
		public static UIViewController initialViewController;

		[Export("scene:willConnectToSession:options:")]
		public void WillConnect(UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
		{
			// Use this method to optionally configure and attach the UIWindow `window` to the provided UIWindowScene `scene`.
			// If using a storyboard, the `window` property will automatically be initialized and attached to the scene.
			// This delegate does not imply the connecting scene or session are new (see UIApplicationDelegate `GetConfiguration` instead).
		}

		[Export("sceneDidDisconnect:")]
		public void DidDisconnect(UIScene scene)
		{
			// Called as the scene is being released by the system.
			// This occurs shortly after the scene enters the background, or when its session is discarded.
			// Release any resources associated with this scene that can be re-created the next time the scene connects.
			// The scene may re-connect later, as its session was not neccessarily discarded (see UIApplicationDelegate `DidDiscardSceneSessions` instead).
		}

		[Export("sceneDidBecomeActive:")]
		public void DidBecomeActive(UIScene scene)
		{

			UIButton loginBtn = new UIButton();

			loginBtn.TouchUpInside += (sender, e) => { 
				
			};
			// Called when the scene has moved from an inactive state to an active state.
			// Use this method to restart any tasks that were paused (or not yet started) when the scene was inactive.
		}

		[Export("sceneWillResignActive:")]
		public void WillResignActive(UIScene scene)
		{
			// Called when the scene will move from an active state to an inactive state.
			// This may occur due to temporary interruptions (ex. an incoming phone call).
		}

		[Export("sceneWillEnterForeground:")]
		public void WillEnterForeground(UIScene scene)
		{
			// Called as the scene transitions from the background to the foreground.
			// Use this method to undo the changes made on entering the background.
		}

		[Export("sceneDidEnterBackground:")]
		public void DidEnterBackground(UIScene scene)
		{
			// Called as the scene transitions from the foreground to the background.
			// Use this method to save data, release shared resources, and store enough scene-specific state information
			// to restore the scene back to its current state.
		}

		[Export("loginOnActivated:")]
		public void OnActivated(UIApplication application)
		{
			Console.WriteLine("OnActivated called, App is active.");
		}

		[Export("loginWillEnterForeground:")]
		public void WillEnterForeground(UIApplication application)
		{
			Console.WriteLine("App will enter foreground");
		}

		[Export("loginOnResingnActivation:")]
		public void OnResignActivation(UIApplication application)
		{
			Console.WriteLine("OnResignActivation called, App moving to inactive state.");
		}

		[Export("loginDidEnterBackgroung:")]
		public void DidEnterBackground(UIApplication application)
		{
			Console.WriteLine("App entering background state.");
		}
		// not guaranteed that this will run

		[Export("loginWillTerminate:")]
		public void WillTerminate(UIApplication application)
		{
			Console.WriteLine("App is terminating.");
			application.Delegate = new AppDelegate();
		}

	}
}