using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Adapters;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Views
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CreateMessageView : BaseActivity<CreateMessageViewModel>
    {
        ContactsAdapter contactsAdapter;

        public CreateMessageView() : base(Resource.Layout.activity_newMessage)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            SetToolBarItems("New Message");
            var contactListView = FindViewById<VerticalRecyclerView>(Resource.Id.contactRecyclerView);
            var bindingContext = (IMvxAndroidBindingContext)BindingContext;
            contactsAdapter = new ContactsAdapter(ViewModel, bindingContext);
            contactListView.Adapter = contactsAdapter;
            contactsAdapter.OnItemClick += OnContactsAdapterItemClick;
        }

        private void OnContactsAdapterItemClick(object sender, Core.DataEntity.Contact e)
        {
            ViewModel.ShowNewChatViewCommand.Execute(e);
        }

        private void OnSearchEvent(string searchText)
        {
            contactsAdapter.NotifyDataSetChanged();
        }
    }
}
