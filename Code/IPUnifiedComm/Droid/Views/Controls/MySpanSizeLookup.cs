using System;
using Android.Support.V7.Widget;

namespace IPUnifiedComm.Droid.Views.Controls
{
    public class MySpanSizeLookup : GridLayoutManager.SpanSizeLookup
    {
        private int spanPos, spanCnt1, spanCnt3;

        public MySpanSizeLookup(int spanPos, int spanOne, int spanThree)
        {
            this.spanPos = spanPos; this.spanCnt1 = spanOne; this.spanCnt3 = spanThree;
        }

        public override int GetSpanSize(int position)
        {
            return position == this.spanPos ? this.spanCnt3 : this.spanCnt1;
        }
    }
}