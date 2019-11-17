using System;
using IPUnifiedComm.Core.DataEntity;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class TaskViewModel: BaseViewModel
    {

        private MvxObservableCollection<RecentTask> recentMyTasks;
        public MvxObservableCollection<RecentTask> RecentMyTasks
        {
            get => recentMyTasks;
            set => SetProperty(ref recentMyTasks, value);
        }

        private MvxObservableCollection<RecentTask> recentTasks;
        public MvxObservableCollection<RecentTask> RecentTasks
        {
            get => recentTasks;
            set => SetProperty(ref recentTasks, value);
        }

        public TaskViewModel()
        {
            recentTasks = new MvxObservableCollection<RecentTask>();
            recentTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Pending", Type = "CAUTIONARY", StatusType = "pending" });
            recentTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Completed", Type = "URGENT", StatusType = "completed" });
            recentTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Pending", Type = "CAUTIONARY", StatusType = "pending" });
            recentTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Completed", Type = "CAUTIONARY", StatusType = "completed" });


            recentMyTasks = new MvxObservableCollection<RecentTask>();
            recentMyTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Pending", Type = "URGENT", StatusType = "pending" });
            recentMyTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Completed", Type = "CAUTIONARY", StatusType = "completed" });
            recentMyTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Pending", Type = "CAUTIONARY", StatusType = "pending" });
            recentMyTasks.Add(new RecentTask() { FromDate = "15 Nov", FromDetail = "DSP, Bengaluru", FromName = "Ravneesh Kumar", Status = "Completed", Type = "CAUTIONARY", StatusType = "completed" });
        }

        public IMvxCommand ShowCreateTaskViewCommand => new MvxCommand(DoShowCreateTaskView);

        private void DoShowCreateTaskView()
        {
            NavigationService.Navigate<CreateTaskViewModel>();
        }

        public IMvxCommand ShowTaskDetailsViewCommand => new MvxCommand(DoShowTaskDetailsView);

        private void DoShowTaskDetailsView()
        {
            NavigationService.Navigate<TaskDetailViewModel>();
        }
    }
}
