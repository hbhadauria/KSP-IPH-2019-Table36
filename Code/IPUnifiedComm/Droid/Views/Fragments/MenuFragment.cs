
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;

namespace IPUnifiedComm.Droid.Views.Fragments
{
    public class MenuFragment : BaseFragment<MenuViewModel>
    {
        public MenuFragment() : base(Resource.Layout.fragment_menu)
        {
        }

        public static MenuFragment NewInstance()
        {
            var fragment = new MenuFragment { Arguments = new Bundle() };
            return fragment;
        }

        protected override void DoOnCreateView(View view, Bundle savedInstanceState)
        {
            var selectedItemId = (Activity as MainView)?.BottomNavigationView.SelectedItemId;

            if (selectedItemId == Resource.Id.tab_menu)
            {
                //Initialize Items
            }
        }
    }
}