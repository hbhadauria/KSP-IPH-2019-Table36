using Android.App;
using Android.Arch.Lifecycle;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using IPUnifiedComm.Core;
using Java.Interop;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Core;
using Plugin.CurrentActivity;
using System;
using Xamarin.Essentials;

namespace IPUnifiedComm.Droid
{
#if DEBUG
    [Application(Debuggable = true, LargeHeap = true)]
#else
    [Application(Debuggable = false, LargeHeap = true)]
#endif
    public partial class MainApplication : MvxAppCompatApplication<Setup, App>, ILifecycleObserver
    {
        //Images are stored in AppDataDirectory and not visble to the user.

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            try
            {
                base.OnCreate();
                CrossCurrentActivity.Current.Init(this);

                RegisterActivityLifecycleCallbacks(this);
            }
            catch (Exception ex)
            {
            }
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        [Lifecycle.Event.OnStart]
        [Export]
        public void Started()
        {
            try
            {
                Log.Debug("Lifecycle.Event.OnStart", "App entered foreground state.");

            }
            catch (Exception ex)
            {
            }
        }

        [Lifecycle.Event.OnStop]
        [Export]
        public void Stopped()
        {
            Log.Debug("Lifecycle.Event.OnStop", "App entered background state.");
        }
    }

    //Activity Lifecycle Callbacks
    public partial class MainApplication : Application.IActivityLifecycleCallbacks
    {
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
            Platform.Init(activity, savedInstanceState);
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}