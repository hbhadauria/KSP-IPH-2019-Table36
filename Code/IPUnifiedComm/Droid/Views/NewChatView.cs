using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Firebase.Database;
using IPUnifiedComm.Core.DataEntity;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Adapters;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Views
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class NewChatView : BaseActivity<NewChatViewModel>, IValueEventListener
    {
        private EditText messageEditText;
        private ImageView sendButton;

        public NewChatView() : base(Resource.Layout.activity_newChat)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            messageEditText = FindViewById<EditText>(Resource.Id.messageEditText);
            sendButton = FindViewById<ImageView>(Resource.Id.sendButton);

            FirebaseDatabase.Instance.GetReference("messages").AddValueEventListener(this);

            var messageListView = FindViewById<VerticalRecyclerView>(Resource.Id.messageRecyclerView);
            var bindingContext = (IMvxAndroidBindingContext)BindingContext;
            var chatAdapter = new NewChatAdapter(ViewModel, bindingContext);
            messageListView.Adapter = chatAdapter;

            sendButton.Click += delegate
            {
                PostMessage();
            };
        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {

        }

        private void PostMessage()
        {
            try
            {
                var chatmessage = new ChatMessage { SenderId = Firebase.Auth.FirebaseAuth.Instance.CurrentUser.PhoneNumber, Message = "Test", ReceiverId = ViewModel.PhoneNumber };
                var chatmessageJavObj = new JavaObjectChatMessage { SenderId = chatmessage.SenderId, ReceiverId = chatmessage.ReceiverId, Message = chatmessage.Message };

                var databaseReference = FirebaseDatabase.Instance.GetReferenceFromUrl("https://ipunifiedcomm-hackathon.firebaseio.com/");
                databaseReference.Child("messages").SetValue(chatmessageJavObj);
                messageEditText.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

    }

    public class JavaObjectChatMessage : Java.Lang.Object
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }
    }
}
