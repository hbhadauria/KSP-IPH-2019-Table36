
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System.Globalization;

namespace IPUnifiedComm.Droid.Views.Fragments
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel>
         where TViewModel : BaseViewModel
    {
        private const int InitialId = -1;
       
        private readonly int contentResourceId = InitialId;
        private ImageView imgBackArrow;


        protected BaseFragment()
        {
            
        }

        protected BaseFragment(int contentResourceId)
            : this()
        {
            this.contentResourceId = contentResourceId;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            if (contentResourceId != InitialId)
            {
                this.EnsureBindingContextIsSet(inflater);
                view = this.BindingInflate(contentResourceId, null);
            }
            DoOnCreateView(view, savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var window = Activity?.Window;

                // clear FLAG_TRANSLUCENT_STATUS flag:
                window?.ClearFlags(WindowManagerFlags.TranslucentStatus);

                // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
                window?.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

                // finally change the color
            }

            return view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            DoOnCreate(savedInstanceState);
        }

        public override void OnStart()
        {
            base.OnStart();
            DoOnStart();
        }

        public override void OnResume()
        {
            base.OnResume();
            UnSubscribeFromLayoutEvents();
            SubscribeToLayoutEvents();
            DoOnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
            DoOnPause();
        }

        public override void OnStop()
        {
            base.OnStop();
            UnSubscribeFromLayoutEvents();
            DoOnStop();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            DoOnConfigurationChanged(newConfig);
            UnSubscribeFromLayoutEvents();
            SubscribeToLayoutEvents();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            DoOnDestroy();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            DoOnSaveInstanceState(outState);
        }

        protected virtual void DoOnCreate(Bundle bundle) { }
        protected virtual void DoOnCreateView(View view, Bundle savedInstanceState) { }
        protected virtual void DoOnStart() { }
        protected virtual void DoOnResume() { }
        protected virtual void DoOnPause() { }
        protected virtual void DoOnStop() { }
        protected virtual void SubscribeToLayoutEvents() { }
        protected virtual void UnSubscribeFromLayoutEvents() { }
        protected virtual void DoOnConfigurationChanged(Configuration newConfig) { }
        protected virtual void DoOnDestroy() { }
        protected virtual void DoOnSaveInstanceState(Bundle outState) { }
        protected virtual void DoOnToolbarBackPressed() { }
    }
}