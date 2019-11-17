using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using System;

namespace IPUnifiedComm.Droid.Views.Controls
{
    public class GridVerticalRecyclerView : VerticalRecyclerView
    {
        private int spanCount = 3;
        private int spanLookup = 1;
        public int SpanLookup { get { return spanLookup; } set { spanLookup = value; } }
        public int SpanCount
        {
            get { return spanCount; }
            set
            {
                spanCount = value;
                SetLayoutManager(LayoutManagerFactory(Context));
            }
        }

        protected GridVerticalRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public GridVerticalRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { }
        public GridVerticalRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs) { }

        protected override LayoutManager LayoutManagerFactory(Context context)
        {
            var layoutManager = new GridLayoutManager(context, SpanCount);
            layoutManager.SetSpanSizeLookup(new MySpanSizeLookup(1, 1, 1));
            return layoutManager;
        }
    }
}