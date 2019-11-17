using System;
using MvvmCross.Commands;

namespace IPUnifiedComm.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public IMvxCommand ShowProfileViewCommand => new MvxCommand(DoShowProfileView);

        public MenuViewModel()
        {
        }

        private void DoShowProfileView()
        {
            NavigationService.Navigate<ProfileViewModel>();
        }
    }
}
