using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using IPUnifiedComm.Core.ViewModels;

namespace IPUnifiedComm.Droid.Views
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ProfileView:BaseActivity<ProfileViewModel>
    {
        public ProfileView():base(Resource.Layout.activity_profile)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            SetToolBarItems("My Profile");
        }
    }
}
