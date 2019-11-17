using System;
using Android.App;
using Android.Content.PM;
using IPUnifiedComm.Core.ViewModels;

namespace IPUnifiedComm.Droid.Views
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TaskDetailView:BaseActivity<TaskDetailViewModel>
    {
        public TaskDetailView():base(Resource.Layout.activity_taskDetails)
        {
        }
    }
}
