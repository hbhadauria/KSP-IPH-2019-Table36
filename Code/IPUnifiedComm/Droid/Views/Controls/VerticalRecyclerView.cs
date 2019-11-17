using System;
using System.Collections;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IPUnifiedComm.Droid.Views.Controls
{
    public class VerticalRecyclerView : MvxRecyclerView
    {
        protected const int DefaultId = 0;

        public event EventHandler<int> LoadMore;

        private int templateId;
        private IEnumerable itemsSource = new object[0];

        protected VerticalRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public VerticalRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Init(context, attrs);
        }

        public VerticalRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public VerticalRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
            Init(context, attrs);
        }

        protected void Init(Context context, IAttributeSet attrs)
        {
            templateId = attrs.GetAttributeResourceValue("http://schemas.android.com/apk/res-auto", "itemTemplate", DefaultId);
            var dividerId = attrs.GetAttributeResourceValue("http://schemas.android.com/apk/res-auto", "listDivider", DefaultId);

            var layoutManager = LayoutManagerFactory(context);
            SetLayoutManager(layoutManager);

            var linearLayoutManager = layoutManager as LinearLayoutManager;
            if (linearLayoutManager != null)
            {
                //var scrollListener = new EndlessRecyclerOnScrollListener(linearLayoutManager, OnLoadMore);
                //AddOnScrollListener(scrollListener);
            }

            if (dividerId == DefaultId)
                return;

            var drawable = ContextCompat.GetDrawable(context, dividerId);
            var itemDecorator = new DividerItemDecoration(context, DividerItemDecoration.Vertical);
            itemDecorator.SetDrawable(drawable);
            AddItemDecoration(itemDecorator);
        }

        protected virtual LayoutManager LayoutManagerFactory(Context context)
        {
            var layoutManager = new LinearLayoutManager(context);

            return layoutManager;
        }
    }
}
