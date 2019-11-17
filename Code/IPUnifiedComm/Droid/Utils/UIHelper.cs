using System;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;

namespace IPUnifiedComm.Droid.Utils
{
    public static class UIHelper
    {
        public static void UpdateBottomNavTitleFont(this BottomNavigationView view)
        {
            var menuView = (BottomNavigationMenuView)view.GetChildAt(0);
            try
            {
                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var item = (BottomNavigationItemView)menuView.GetChildAt(i);
                    // set once again checked value, so view will be updated
                    //noinspection RestrictedApi
                    item.SetChecked(item.ItemData.IsChecked);
                    //Custom Style Title
                    
                }
                menuView.UpdateMenuView();
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
