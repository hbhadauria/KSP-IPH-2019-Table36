using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using MvvmCross;
using System;


namespace IPUnifiedComm.Droid.Views.Controls
{
	[Register("IPUnifiedComm.Droid.Controls.ToggleView")]
	public class ToggleView: RelativeLayout
    {
        private int checkedDrawable, uncheckedDrawable, checkedBackgroundDrawable, uncheckedBackgroundDrawable;

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set { isChecked = value; UpdateImage(); Invalidate(); }
        }

        private ImageView toggleImageView;
        public ImageView ToggleImageView
        {
            get => toggleImageView;
            set { toggleImageView = value; Invalidate(); }
        }

        public ToggleView(Context context) : base(context)
        {
            Initialize();
        }

        public ToggleView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            var typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.toggleView, 0, 0);
            try
            {
                isChecked = typedArray.GetBoolean(Resource.Styleable.toggleView_isChecked, false);
                checkedDrawable = typedArray.GetResourceId(Resource.Styleable.toggleView_checkedDrawable, 0);
                uncheckedDrawable = typedArray.GetResourceId(Resource.Styleable.toggleView_uncheckedDrawable, 0);
                checkedBackgroundDrawable = typedArray.GetResourceId(Resource.Styleable.toggleView_checkedBackgroundDrawable, 0);
                uncheckedBackgroundDrawable = typedArray.GetResourceId(Resource.Styleable.toggleView_uncheckedBackgroundDrawable, 0);
                //toggleTextViewVisibility = typedArray.GetBoolean(Resource.Styleable.toggleView_toggleTextViewVisibility, false);
            }
            finally
            {
                typedArray.Recycle();
            }

            Initialize();
            UpdateImage();
        }

        public ToggleView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        protected ToggleView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private void Initialize()
        {
            Inflate(Context, Resource.Layout.view_toggleButton, this);
            ToggleImageView = FindViewById<ImageView>(Resource.Id.toggleImageView);
        }

        private void UpdateImage()
        {
            if (checkedDrawable != 0 && uncheckedDrawable != 0)
            {
                toggleImageView.SetImageResource(IsChecked ? checkedDrawable : uncheckedDrawable);
            }

            if (checkedBackgroundDrawable != 0 && uncheckedBackgroundDrawable != 0)
            {
                SetBackgroundResource(IsChecked ? checkedBackgroundDrawable : uncheckedBackgroundDrawable);
            }
        }
    }
}