using Android.Support.V7.Widget;
using Android.Views;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Adapters
{
    public class SelectContactsAdapter : MvxRecyclerAdapter
    {
        private readonly SelectContactsViewModel viewModel;

        public SelectContactsAdapter(SelectContactsViewModel myAwardsViewModel, IMvxAndroidBindingContext bindingContext)
           : base(bindingContext)
        {
            this.viewModel = myAwardsViewModel;
        }

        public override int GetItemViewType(int position)
        {
            return 0;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder viewHolder = null;
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);
            var view = itemBindingContext.BindingInflate(Resource.Layout.item_myContact, parent, false);
            viewHolder = new MyContactItemViewHolder(view, itemBindingContext);
            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            var viewHolder = holder as MyContactItemViewHolder;
            var toggleButton= holder.ItemView.FindViewById<ToggleView>(Resource.Id.toggleView);
            if (!toggleButton.HasOnClickListeners)
                toggleButton.Click += ToggleButton_Click;

        }

        private void ToggleButton_Click(object sender, System.EventArgs e)
        {
            var toggleButton = sender as ToggleView;
            toggleButton.IsChecked = !toggleButton.IsChecked;
            viewModel.ValidateButton(toggleButton.IsChecked);
        }

        protected class MyContactItemViewHolder : MvxRecyclerViewHolder
        {
            public MyContactItemViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
            {
            }
        }
    }
}