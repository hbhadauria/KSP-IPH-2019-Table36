using Android.Support.V7.Widget;

namespace IPUnifiedComm.Droid.Helpers
{
    public class SpacesItemDecoration : RecyclerView.ItemDecoration
    {
        private int space;

        //Added for influencer public profile
        private bool skipFirstRow;
        public bool SkipFirstRow
        {
            get
            {
                return skipFirstRow;
            }
            set
            {
                skipFirstRow = value;
            }
        }

        public SpacesItemDecoration(int space)
        {
            this.space = space;
        }

#pragma warning disable CS0672 // Member overrides obsolete member
        public override void GetItemOffsets(Android.Graphics.Rect outRect, int itemPosition, Android.Support.V7.Widget.RecyclerView parent)
#pragma warning restore CS0672 // Member overrides obsolete member
        {
            if (SkipFirstRow && itemPosition == 0)
            {
                outRect.Top = 0;
                outRect.Left = 0;
            }
            else if (itemPosition % 2 == 0)
            {
                outRect.Top = space;
                // outRect.Left = space;
                outRect.Bottom = space;
            }
            else
            {
                outRect.Top = space;
                // outRect.Left = space;
                outRect.Bottom = space;
            }
        }
    }
}