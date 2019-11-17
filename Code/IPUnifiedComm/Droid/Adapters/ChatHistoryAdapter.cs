using System;
using Android.Support.V7.Widget;
using Android.Views;
using IPUnifiedComm.Core.DataEntity;
using IPUnifiedComm.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Adapters
{
    public class ChatHistoryAdapter : MvxRecyclerAdapter
    {
        private readonly ChatHistoryViewModel viewModel;

        public event EventHandler<Contact> OnItemClick;

        public ChatHistoryAdapter(ChatHistoryViewModel viewModel, IMvxAndroidBindingContext bindingContext)
           : base(bindingContext)
        {
            this.viewModel = viewModel;
        }

        public override int GetItemViewType(int position)
        {
            return 0;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder viewHolder = null;
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);
            var view = itemBindingContext.BindingInflate(Resource.Layout.item_chatHistory, parent, false);
            viewHolder = new ChatHistoryItemViewHolder(view, itemBindingContext, OnClick) { };
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

        protected class ChatHistoryItemViewHolder : MvxRecyclerViewHolder
        {
            private readonly Action<int> listener;

            public ChatHistoryItemViewHolder(View itemView, IMvxAndroidBindingContext context, Action<int> listener)
                : base(itemView, context)
            {
                this.listener = listener;
                ItemView.Click += OnItemClick;
            }

            private void OnItemClick(object sender, EventArgs e)
            {
                listener(AdapterPosition);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    ItemView.Click -= OnItemClick;

                base.Dispose(disposing);
            }
        }
    }
}
