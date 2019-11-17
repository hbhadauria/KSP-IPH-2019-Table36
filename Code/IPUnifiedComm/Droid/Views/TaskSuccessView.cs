
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Utils;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace IPUnifiedComm.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TaskSuccessView : BaseActivity<TaskSuccessViewModel>
    {
        Button btnHome;

        public TaskSuccessView() : base(Resource.Layout.activity_taskSuccess)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            btnHome = FindViewById<Button>(Resource.Id.btnSubmit);

            btnHome.Click -= OnHomeButtonClick;
            btnHome.Click += OnHomeButtonClick;
        }
        void OnHomeButtonClick(object sender, EventArgs e)
        {
            DoHomeNavigation();
        }

        private void DoHomeNavigation()
        {
            var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            navigationService.ChangePresentation(new HomeNavigationHint());
        }
    }
}
