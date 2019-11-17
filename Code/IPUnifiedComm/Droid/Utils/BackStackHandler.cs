using Android.Content;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace IPUnifiedComm.Droid.Utils
{
    public class BackStackHandler
    {
        private Context applicationcontext;
        private Type mainActivityType;

        public BackStackHandler(Context applicationcontext, Type mainActivityType)
        {
            this.applicationcontext = applicationcontext;
            this.mainActivityType = mainActivityType;
        }

        public Task<bool> HandleClearBackstackHint(MvxPresentationHint clearBackstackHint)
        {
            Intent i = new Intent(applicationcontext, mainActivityType);
            i.SetFlags(ActivityFlags.NewTask | ActivityFlags.SingleTop | ActivityFlags.ClearTop);
            applicationcontext.StartActivity(i); // Launch the mainActivity instance on top and stack after mainActivity
            return Task.FromResult(true);
        }
    }
}
