using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Plugin.CurrentActivity;

namespace IPUnifiedComm.Droid.Views
{
    public abstract class BaseActivity<T> : MvxAppCompatActivity<T>
        where T : BaseViewModel
    {
        private ImageView imgBackArrow;
        public TextView TxtTitle;
        private readonly int contentResourceId = -1;

        private string titleText;
        public string TitleText
        {
            get => titleText;
            set
            {
                titleText = value;
                var txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
                if (txtTitle != null) txtTitle.Text = titleText;
            }
        }

        protected bool UseLightTheme;


        public void SetToolBarItems(string txtTitle)
        {
            TxtTitle = FindViewById<TextView>(Resource.Id.txtTitle);

            if (TxtTitle != null)
                TxtTitle.Text = txtTitle;
        }

        protected BaseActivity()
        {
            // InitlizeAppServices();
        }

        protected BaseActivity(int contentResourceId)
        {
            this.contentResourceId = contentResourceId;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (contentResourceId >= 0)
            {
                SetContentView(contentResourceId);
            }
            imgBackArrow = FindViewById<ImageView>(Resource.Id.imgBackArrow);
            // InitlizeAppServices();
            if (imgBackArrow != null && !imgBackArrow.HasOnClickListeners)
            {
                imgBackArrow.Click += (sender, e) =>
                {
                    ViewModel.BackCommand.Execute(null);
                };
            }

            if (UseLightTheme)
            {
                SetLightTheme();
            }
            else
            {
                SetDarkTheme();
            }

            DoOnCreate(bundle);
        }



        protected override void OnStart()
        {
            base.OnStart();


            DoOnStart();
            SubscribeToLayoutEvents();
            SubscribeToViewModelEvents(ViewModel);

        }

        protected override void OnPause()
        {
            base.OnPause();
            DoOnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            DoOnResume();
        }

        protected override void OnStop()
        {
            base.OnStop();


            DoOnStop();
            UnSubscribeFromLayoutEvents();
            UnSubscribeFromViewModelEvents(ViewModel);
        }

        protected override void OnDestroy()
        {

            DoOnDestroy();
            base.OnDestroy();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            DoOnSaveInstanceState(outState);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            var viewModel = ViewModel;
            if (viewModel == null)
                return;

            viewModel.BackCommand.Execute(null);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing == true)
            {

            }
        }

        protected virtual void DoOnCreate(Bundle bundle) { }
        protected virtual void DoOnStart() { }
        protected virtual void DoOnPause() { }
        protected virtual void DoOnResume() { }
        protected virtual void DoOnStop() { }
        protected virtual void DoOnDestroy() { }
        protected virtual void DoOnSaveInstanceState(Bundle outState) { }
        protected virtual void SubscribeToLayoutEvents() { }
        protected virtual void SubscribeToViewModelEvents(T viewModel) { }
        protected virtual void UnSubscribeFromLayoutEvents() { }
        protected virtual void UnSubscribeFromViewModelEvents(T viewModel) { }
        protected virtual void DoOnBackPressed() { }

        protected void GoToAndroidHomeLauncher()
        {
            var startMain = new Intent(Intent.ActionMain);
            startMain.AddCategory(Intent.CategoryHome);
            startMain.SetFlags(ActivityFlags.NewTask);
            StartActivity(startMain);
        }

        private void SetDarkTheme()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var currentWindow = GetCurrentWindow();
                currentWindow.DecorView.SystemUiVisibility = 0;
                currentWindow.SetStatusBarColor(Color.ParseColor("#12041f"));
            }
        }

        private void SetLightTheme()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var currentWindow = GetCurrentWindow();
                currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                currentWindow.SetStatusBarColor(Color.White);
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