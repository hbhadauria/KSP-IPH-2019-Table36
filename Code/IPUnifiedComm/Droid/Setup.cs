using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using IPUnifiedComm.Core;
using IPUnifiedComm.Droid.Bindings;
using IPUnifiedComm.Droid.Utils;
using IPUnifiedComm.Droid.Views;
using IPUnifiedComm.Droid.Views.Controls;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.IoC;
using MvvmCross.Localization;
using MvvmCross.Platforms.Android.Presenters;
using System.Collections.Generic;
using System.Reflection;

namespace IPUnifiedComm.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {

        protected override IMvxIoCProvider InitializeIoC()
        {
            return base.InitializeIoC();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
            registry.RegisterCustomBindingFactory<VerticalRecyclerView>("RecyclerSource", view => new VerticalRecyclerViewRecyclerSourceTargetBinding(view));
            registry.RegisterCustomBindingFactory<View>("SubmitClaimButtonBackround", view => new ViewSubmitClaimButtonBackroundTargetBinding(view));
            //registry.RegisterCustomBindingFactory<VerticalRecyclerView>("LoadMore", view => new VerticalRecyclerViewLoadMoreTargetBinding(view));
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(ViewPager).Assembly,
            typeof(MvxRecyclerView).Assembly,
            typeof(MvxSwipeRefreshLayout).Assembly,
            typeof(NestedScrollView).Assembly,
            typeof(RecyclerView).Assembly
        };

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvxLanguageConverter());
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = base.CreateViewPresenter();
            var claimsHomeViewHandler = new BackStackHandler(ApplicationContext, typeof(MainView));
            presenter.AddPresentationHintHandler<HomeNavigationHint>(hint => claimsHomeViewHandler.HandleClearBackstackHint(hint));
            return presenter;
        }
    }
}