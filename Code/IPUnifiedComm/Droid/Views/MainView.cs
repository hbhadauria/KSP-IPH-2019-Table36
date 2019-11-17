
using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.Enum;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Utils;
using IPUnifiedComm.Droid.Views.Fragments;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace IPUnifiedComm.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait,
            WindowSoftInputMode = SoftInput.AdjustPan, LaunchMode = LaunchMode.SingleTop)]
    public class MainView : BaseActivity<MainViewModel>
    {
        private FrameLayout chatFrame, taskFrame, menuFrame;

        public TaskFragment TaskFragment;
        public ChatHistoryFragment ChatFragment;
        public MenuFragment MenuFragment;

        public BottomNavigationView BottomNavigationView;
        public TextView txtUnReadNotification;

        public MainView() : base(Resource.Layout.activity_main)
        {

        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            SetupContainerFragments();

            BottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            BottomNavigationView.NavigationItemSelected += BottomNavigationItemSelected;
            BottomNavigationView.SelectedItemId = Resource.Id.tab_task;
            BottomNavigationView.UpdateBottomNavTitleFont();
        }

        private void SetupContainerFragments()
        {
            taskFrame = FindViewById<FrameLayout>(Resource.Id.content_frame_task);
            chatFrame = FindViewById<FrameLayout>(Resource.Id.content_frame_chat);
            menuFrame = FindViewById<FrameLayout>(Resource.Id.content_frame_menu);

            //task
            var fragmentTxHome = SupportFragmentManager.BeginTransaction();
            TaskFragment = TaskFragment.NewInstance();
            TaskFragment.DataContext = ViewModel.TaskViewModel;
            fragmentTxHome.Replace(Resource.Id.content_frame_task, TaskFragment);
            fragmentTxHome.Commit();

            //chat
            var fragmentTxChat = SupportFragmentManager.BeginTransaction();
            ChatFragment = ChatHistoryFragment.NewInstance();
            ChatFragment.DataContext = ViewModel.ChatViewModel;
            fragmentTxChat.Replace(Resource.Id.content_frame_chat, ChatFragment);
            fragmentTxChat.Commit();

            //menu
            var fragmentTxMenu = SupportFragmentManager.BeginTransaction();
            MenuFragment = MenuFragment.NewInstance();
            MenuFragment.DataContext = ViewModel.MenuViewModel;
            fragmentTxMenu.Replace(Resource.Id.content_frame_menu, MenuFragment);
            fragmentTxMenu.Commit();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            if (intent != null && intent.HasExtra("selectedIndex"))
            {
                var selectedIndex = intent.GetIntExtra("selectedIndex", 0);
                if (selectedIndex == 0)
                {
                    BottomNavigationView.SelectedItemId = Resource.Id.tab_menu;
                }
                else if (selectedIndex == 1)
                {
                    BottomNavigationView.SelectedItemId = Resource.Id.tab_chat;
                }
            }
        }

        protected override void SubscribeToLayoutEvents()
        {
            base.SubscribeToLayoutEvents();
        }

        protected override void UnSubscribeFromLayoutEvents()
        {
            base.UnSubscribeFromLayoutEvents();
        }

        private void BottomNavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        private void LoadFragment(int id)
        {
            using (var fragmentTx = SupportFragmentManager.BeginTransaction())
            {
                switch (id)
                {
                    case Resource.Id.tab_task:
                        ShowCurrentTabFragment(BottomTabType.Task);
                        fragmentTx.Detach(TaskFragment).Attach(TaskFragment).Commit();
                        break;

                    case Resource.Id.tab_chat:
                        ShowCurrentTabFragment(BottomTabType.Chat);
                        fragmentTx.Detach(ChatFragment).Attach(ChatFragment).Commit();
                        break;

                    case Resource.Id.tab_menu:
                        ShowCurrentTabFragment(BottomTabType.Menu);
                        fragmentTx.Detach(MenuFragment).Attach(MenuFragment).Commit();
                        break;
                }
            }
        }

        private void ShowCurrentTabFragment(BottomTabType tabType)
        {
            taskFrame.Visibility = ViewStates.Gone;
            chatFrame.Visibility = ViewStates.Gone;
            menuFrame.Visibility = ViewStates.Gone;

            switch (tabType)
            {
                case BottomTabType.Task:
                    taskFrame.Visibility = ViewStates.Visible;
                    break;

                case BottomTabType.Chat:
                    chatFrame.Visibility = ViewStates.Visible;
                    break;

                case BottomTabType.Menu:
                    menuFrame.Visibility = ViewStates.Visible;
                    break;
            }
        }
    }
}
