using System;
using Android.Graphics;
using Android.Views;
using MvvmCross;

namespace IPUnifiedComm.Droid.Bindings
{
    public class ViewSubmitClaimButtonBackroundTargetBinding : BaseTargetBinding<string, View>
    {
        public ViewSubmitClaimButtonBackroundTargetBinding(View targetObject) : base(targetObject)
        {
        }

        protected override void DoSetValueImpl(View target, string value)
        {
            bool btnEnabled = Convert.ToBoolean(value);

            if (btnEnabled)
            {
                target.Background.SetColorFilter(Color.ParseColor("#c2bb7f"), PorterDuff.Mode.SrcAtop);
            }
            else
            {
                target.Background.SetColorFilter(Color.ParseColor("#b5b5b5"),PorterDuff.Mode.SrcAtop);
            }
        }
    }
}
