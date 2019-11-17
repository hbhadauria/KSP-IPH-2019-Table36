using System;
using Android.Support.V7.Widget;
using Android.Views;
using Firebase.Auth;
using IPUnifiedComm.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Adapters
{
    public class NewChatAdapter : MvxRecyclerAdapter
    {
        private const int MessageSentType = 0;
        private const int MessageReceiveType = 1;

        private readonly NewChatViewModel viewModel;

        public NewChatAdapter(NewChatViewModel viewModel, IMvxAndroidBindingContext bindingContext)
           : base(bindingContext)
        {
            this.viewModel = viewModel;
        }

        public override int GetItemViewType(int position)
        {
            if (viewModel.Messages[position].SenderId == FirebaseAuth.Instance.CurrentUser.PhoneNumber)
                return MessageSentType;
            else
                return MessageReceiveType;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder viewHolder = null;
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);

            View view = null;
            if (viewType == MessageSentType)
            {
                view = itemBindingContext.BindingInflate(Resource.Layout.item_sentMessage, parent, false);
                viewHolder = new SentItemViewHolder(view, itemBindingContext) { };
            }
            else
            {
                view = itemBindingContext.BindingInflate(Resource.Layout.item_receivedMessage, parent, false);
                viewHolder = new ReceivedItemViewHolder(view, itemBindingContext) { };
            }


            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
        }

        private void OnClick(int position)
        {
            //if (OnItemClick != null)
            //OnItemClick(this, viewModel.Contacts[position]);
        }

        protected class SentItemViewHolder : MvxRecyclerViewHolder
        {
            public SentItemViewHolder(View itemView, IMvxAndroidBindingContext context)
                : base(itemView, context)
            {

            }
        }

        protected class ReceivedItemViewHolder : MvxRecyclerViewHolder
        {
            public ReceivedItemViewHolder(View itemView, IMvxAndroidBindingContext context)
                : base(itemView, context)
            {

            }
        }
    }
}