using System;
using System.Threading.Tasks;
using IPUnifiedComm.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            //if logged in

            //if  not logged

            return NavigationService.Navigate<LoginViewModel>();

            // return NavigationService.Navigate<MainViewModel>();

        }
    }
}