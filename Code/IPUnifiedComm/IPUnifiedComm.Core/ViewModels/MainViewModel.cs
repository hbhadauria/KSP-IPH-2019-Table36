using System;
namespace IPUnifiedComm.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public TaskViewModel TaskViewModel { get; private set; }
        public ChatHistoryViewModel ChatViewModel { get; private set; }
        public MenuViewModel MenuViewModel { get; private set; }

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                SetProperty(ref selectedIndex, value);
                RaisePropertyChanged(() => SelectedIndex);
            }
        }

        public MainViewModel()
        {
            TaskViewModel = TaskViewModel ?? new TaskViewModel();
            ChatViewModel = ChatViewModel ?? new ChatHistoryViewModel();
            MenuViewModel = MenuViewModel ?? new MenuViewModel();
        }
    }
}
