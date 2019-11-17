using System;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;

namespace IPUnifiedComm.Droid.Utils
{
    public class LinePagerIndicatorDecoration : RecyclerView.ItemDecoration
    {
        private string colorActive = "#12041f";
        private string colorInactive = "#d8d8d8";

        private static float DP = Application.Context.Resources.DisplayMetrics.Density;

        private int mIndicatorHeight = (int)(DP*16);

        /**
         * Indicator stroke width.
         */
        private float mIndicatorStrokeWidth = 6* DP;

        /**
         * Indicator width.
         */
        private float mIndicatorItemLength = DP*16;
        /**
         * Padding between indicators.
         */
        private float mIndicatorItemPadding = DP*8;

        /**
         * Some more natural animation interpolation
         */
        private AccelerateDecelerateInterpolator mInterpolator = new AccelerateDecelerateInterpolator();

        private Paint mPaint = new Paint();

        public LinePagerIndicatorDecoration()
        {
            mPaint.StrokeCap = Paint.Cap.Round;
            mPaint.StrokeWidth = mIndicatorStrokeWidth;
            mPaint.SetStyle(Paint.Style.Stroke);
            mPaint.AntiAlias = true;
        }
        public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            base.OnDrawOver(c, parent, state);
            int itemCount = parent.GetAdapter().ItemCount;

            // center horizontally, calculate width and subtract half from center
            float totalLength = mIndicatorItemLength * itemCount;
            float paddingBetweenItems = Math.Max(0, itemCount - 1) * mIndicatorItemPadding;
            float indicatorTotalWidth = totalLength + paddingBetweenItems;
            float indicatorStartX = (parent.Width - indicatorTotalWidth) / 2F;

            float indicatorPosY = parent.Height - mIndicatorHeight / 2F;

            drawInactiveIndicators(c, indicatorStartX, indicatorPosY, itemCount);


            // find active page (which should be highlighted)
            LinearLayoutManager layoutManager = (LinearLayoutManager)parent.GetLayoutManager();
            int activePosition = layoutManager.FindLastVisibleItemPosition();
            if (activePosition == RecyclerView.NoPosition)
            {
                return;
            }

            // find offset of active page (if the user is scrolling)
            View activeChild = layoutManager.FindViewByPosition(activePosition);
            int left = activeChild.Left;
            int width = activeChild.Width;

            // on swipe the active item will be positioned from [-width, 0]
            // interpolate offset for smooth animation
            float progress = mInterpolator.GetInterpolation(left * -1 / (float)width);

            drawHighlights(c, indicatorStartX, indicatorPosY, activePosition, progress, itemCount);
        }

        private void drawInactiveIndicators(Canvas c, float indicatorStartX, float indicatorPosY, int itemCount)
        {
            mPaint.Color = Color.ParseColor(colorInactive);

            // width of item indicator including padding
            float itemWidth = mIndicatorItemLength + mIndicatorItemPadding;

            float start = indicatorStartX;
            for (int i = 0; i < itemCount; i++)
            {
                // draw the line for every item
                c.DrawLine(start, indicatorPosY, start + mIndicatorItemLength, indicatorPosY, mPaint);
                start += itemWidth;
            }
        }

        private void drawHighlights(Canvas c, float indicatorStartX, float indicatorPosY,
                             int highlightPosition, float progress, int itemCount)
        {
            mPaint.Color = Color.ParseColor(colorActive);

            // width of item indicator including padding
            float itemWidth = mIndicatorItemLength + mIndicatorItemPadding;

            if (progress == 0F)
            {
                // no swipe, draw a normal indicator
                float highlightStart = indicatorStartX + itemWidth * highlightPosition;
                c.DrawLine(highlightStart, indicatorPosY,
                    highlightStart + mIndicatorItemLength, indicatorPosY, mPaint);
            }
            else
            {
                float highlightStart = indicatorStartX + itemWidth * highlightPosition;
                // calculate partial highlight
                float partialLength = mIndicatorItemLength * progress;

                // draw the cut off highlight
                c.DrawLine(highlightStart + partialLength, indicatorPosY,
                    highlightStart + mIndicatorItemLength, indicatorPosY, mPaint);

                // draw the highlight overlapping to the next item as well
                if (highlightPosition < itemCount - 1)
                {
                    highlightStart += itemWidth;
                    c.DrawLine(highlightStart, indicatorPosY,
                        highlightStart + partialLength, indicatorPosY, mPaint);
                }
            }
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);
            outRect.Bottom = mIndicatorHeight;
        }
    }
}
