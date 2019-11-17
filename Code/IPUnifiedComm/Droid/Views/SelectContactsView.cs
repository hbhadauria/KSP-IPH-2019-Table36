using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Firebase.Database;
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
    public class SelectContactsView : BaseActivity<SelectContactsViewModel>
    {
        private DatabaseReference mDatabase;
        private SelectContactsAdapter adapter1, adapter2, adapter3;
        private Button btnSubmit;

        public SelectContactsView() : base(Resource.Layout.activity_selectContacts)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            SetToolBarItems("Select your Recipients");

            var verticalRecycler1 = FindViewById<GridVerticalRecyclerView>(Resource.Id.recyclerLevel1);
            verticalRecycler1.AddItemDecoration(new SpacesItemDecoration(20));
            var bindingContext1 = (IMvxAndroidBindingContext)BindingContext;
            adapter1 = new SelectContactsAdapter(ViewModel, bindingContext1);
            verticalRecycler1.SpanCount = 4;
            verticalRecycler1.Adapter = adapter1;

            var verticalRecycler2 = FindViewById<GridVerticalRecyclerView>(Resource.Id.recyclerLevel2);
            verticalRecycler2.AddItemDecoration(new SpacesItemDecoration(20));
            var bindingContext2 = (IMvxAndroidBindingContext)BindingContext;
            adapter2 = new SelectContactsAdapter(ViewModel, bindingContext2);
            verticalRecycler2.SpanCount = 4;
            verticalRecycler2.Adapter = adapter2;

            var verticalRecycler3 = FindViewById<GridVerticalRecyclerView>(Resource.Id.recyclerLevel3);
            verticalRecycler3.AddItemDecoration(new SpacesItemDecoration(20));
            var bindingContext3 = (IMvxAndroidBindingContext)BindingContext;
            adapter3 = new SelectContactsAdapter(ViewModel, bindingContext3);
            verticalRecycler3.SpanCount = 4;
            verticalRecycler3.Adapter = adapter3;


            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            var set = this.CreateBindingSet<SelectContactsView, SelectContactsViewModel>();
            set.Bind(btnSubmit).For(v => v.Enabled).To(vm => vm.EnableSubmitButton).OneWay();
            set.Apply();


            //  var firebase = FirebaseDatabase.GetInstance("https://ipunifiedcomm-hackathon.firebaseio.com/").GetReference("users");
        }
    }
}
