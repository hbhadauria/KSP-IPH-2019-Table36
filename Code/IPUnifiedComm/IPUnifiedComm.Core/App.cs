using System;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterCustomAppStart<AppStart>();
        }
    }
}
