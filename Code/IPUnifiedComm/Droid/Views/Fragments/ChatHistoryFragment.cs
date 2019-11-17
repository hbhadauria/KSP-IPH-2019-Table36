using Android.OS;
using Android.Views;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Adapters;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Views.Fragments
{
    public class ChatHistoryFragment : BaseFragment<ChatHistoryViewModel>
    {
        public ChatHistoryFragment() : base(Resource.Layout.fragment_chat)
        {
        }

        public static ChatHistoryFragment NewInstance()
        {
            var fragment = new ChatHistoryFragment { Arguments = new Bundle() };
            return fragment;
        }

        protected override void DoOnCreateView(View view, Bundle savedInstanceState)
        {
            var selectedItemId = (Activity as MainView)?.BottomNavigationView.SelectedItemId;

            if (selectedItemId == Resource.Id.tab_chat)
            {
                //Initialize Items
                
            }

            var chatListView = view.FindViewById<VerticalRecyclerView>(Resource.Id.chatHistoryRecyclerView);
            var bindingContext = (IMvxAndroidBindingContext)BindingContext;
            var adapter = new ChatHistoryAdapter(ViewModel, bindingContext);
            chatListView.Adapter = adapter;
        }
    }
}