using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService NavigationService;
        private readonly IMvxCommand closeCommand;
        public IMvxCommand CloseCommand => closeCommand;
        public IMvxCommand BackCommand => closeCommand;


        public BaseViewModel()
        {
            closeCommand = new MvxCommand(DoClose);
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            DoSubscribeMessages();
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
            DoSubscribeMessages();
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
        }

        public void DoSubscribeMessages()
        {
            //Unsubscribe all messages before subscribing to them.
            DoUnsubscribeMessages();
        }

        public void DoUnsubscribeMessages()
        {
        }

        protected virtual void DoClose()
        {
            NavigationService.Close(this);
        }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter> where TParameter : class
    {
        public void Prepare(TParameter parameter)
        {
            DoPrepare(parameter);
        }

        protected virtual void DoPrepare(TParameter parameter)
        {
        }
    }

    public abstract class BaseViewModelWithResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult> where TResult : class
    {
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }
}
