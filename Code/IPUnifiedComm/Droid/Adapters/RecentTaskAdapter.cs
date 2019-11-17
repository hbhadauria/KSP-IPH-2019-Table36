using System;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Adapters
{
    public class RecentTaskAdapter : MvxRecyclerAdapter
    {
        private readonly TaskViewModel viewModel;

        public RecentTaskAdapter(TaskViewModel viewModel, IMvxAndroidBindingContext bindingContext)
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
            var view = itemBindingContext.BindingInflate(Resource.Layout.item_recentTask, parent, false);
            viewHolder = new RecentTaskItemViewHolder(view, itemBindingContext, OnClick);
            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            var statusTxt = holder.ItemView.FindViewById<TextView>(Resource.Id.statusTxt);
            if (viewModel.RecentTasks[position].StatusType == "pending")
            {
                statusTxt.SetTextColor(Color.ParseColor("#e46c6c"));
            }
            else
            {
                statusTxt.SetTextColor(Color.ParseColor("#52b081"));
            }
        }

        private void OnClick(int position)
        {
                viewModel.ShowTaskDetailsViewCommand.Execute();
        }

        protected class RecentTaskItemViewHolder : MvxRecyclerViewHolder
        {
            private readonly Action<int> listener;

            public RecentTaskItemViewHolder(View itemView, IMvxAndroidBindingContext context, Action<int> listener) : base(itemView, context)
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
