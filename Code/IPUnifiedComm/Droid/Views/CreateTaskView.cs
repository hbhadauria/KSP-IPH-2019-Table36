
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Adapters;
using IPUnifiedComm.Droid.Helpers;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace IPUnifiedComm.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CreateTaskView : BaseActivity<CreateTaskViewModel>
    {
        private DocumentsAdapter adapter;
        private Button btnSubmit;

        public CreateTaskView() : base(Resource.Layout.activity_createTask)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            SetToolBarItems("New Task");
            var verticalRecycler = FindViewById<GridVerticalRecyclerView>(Resource.Id.recyclerUploads);

            verticalRecycler.AddItemDecoration(new SpacesItemDecoration(20));

            var bindingContext = (IMvxAndroidBindingContext)BindingContext;
            adapter = new DocumentsAdapter(ViewModel, bindingContext, this);
            verticalRecycler.Adapter = adapter;

            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            var set = this.CreateBindingSet<CreateTaskView, CreateTaskViewModel>();
            set.Bind(btnSubmit).For(v => v.Enabled).To(vm => vm.EnableSubmitButton).OneWay();
            set.Apply();
            // Create your application here
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            adapter?.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
