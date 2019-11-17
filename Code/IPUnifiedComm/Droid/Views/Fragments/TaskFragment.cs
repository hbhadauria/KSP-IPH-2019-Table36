using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using IPUnifiedComm.Core.ViewModels;
using IPUnifiedComm.Droid.Adapters;
using IPUnifiedComm.Droid.Utils;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IPUnifiedComm.Droid.Views.Fragments
{
    public class TaskFragment : BaseFragment<TaskViewModel>
    {
        public TaskFragment() : base(Resource.Layout.fragment_task)
        {
        }

        public static TaskFragment NewInstance()
        {
            var fragment = new TaskFragment { Arguments = new Bundle() };
            return fragment;
        }

        protected override void DoOnCreateView(View view, Bundle savedInstanceState)
        {
            var selectedItemId = (Activity as MainView)?.BottomNavigationView.SelectedItemId;

            if (selectedItemId == Resource.Id.tab_task)
            {
                //Initialize Items
            }
            var taskListView = view.FindViewById<HorizontalRecyclerView>(Resource.Id.recentTasksRecyclerView);
            var bindingContext = (IMvxAndroidBindingContext)BindingContext;
            var adapter = new RecentTaskAdapter(ViewModel, bindingContext);
            taskListView.Adapter = adapter;
            // add pager behavior
            PagerSnapHelper snapHelper = new PagerSnapHelper();
            snapHelper.AttachToRecyclerView(taskListView);

            // pager indicator
            taskListView.AddItemDecoration(new LinePagerIndicatorDecoration());


            var taskByMeListView = view.FindViewById<VerticalRecyclerView>(Resource.Id.recentMyTasksRecyclerView);
            var bindingContextVertical = (IMvxAndroidBindingContext)BindingContext;
            var adapterVertical = new RecentTaskByMeAdapter(ViewModel, bindingContextVertical);
            taskByMeListView.Adapter = adapterVertical;
        }

    }
}