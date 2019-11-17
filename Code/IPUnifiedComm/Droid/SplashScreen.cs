
using System;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using IPUnifiedComm.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
using Plugin.CurrentActivity;

namespace IPUnifiedComm.Droid
{
    [Activity(
        MainLauncher = true
           , Theme = "@style/Theme.Splash"
       , NoHistory = true
       //Launch mode set to 'single task' to open app on click of notification.
       , LaunchMode = LaunchMode.SingleTask
       , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
    {
        public SplashScreen() : base(Resource.Layout.activity_splashScreen)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var currentWindow = GetCurrentWindow();
                currentWindow.DecorView.SystemUiVisibility = 0;
                currentWindow.SetStatusBarColor(Color.ParseColor("#12041f"));
            }

            Console.WriteLine("MvxSplashScreenAppCompatActivity: IsTaskRoot " + IsTaskRoot);

            if (!IsTaskRoot)
            {
                Finish();
            }
        }

        private Window GetCurrentWindow()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;

            // clear FLAG_TRANSLUCENT_STATUS flag:
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }
    }
}
