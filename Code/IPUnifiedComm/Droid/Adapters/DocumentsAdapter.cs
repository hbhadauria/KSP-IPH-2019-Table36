
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using IPUnifiedComm.Core.ViewModels;
using Java.IO;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System;
using System.Collections.Specialized;
using System.IO;
using ImageUtils = IPUnifiedComm.Droid.Utils.ImageUtils;
using Uri = Android.Net.Uri;

namespace IPUnifiedComm.Droid.Adapters
{
    public class DocumentsAdapter : MvxRecyclerAdapter
    {
        private const int ItemType = 1;
        private const int DefaultType = 0;

        private CreateTaskViewModel viewModel;

        private Context context;

        const int REQUEST_PICK_PHOTO = 4710;


        protected class ItemViewHolder : RecyclerView.ViewHolder
        {
            public ItemViewHolder(View view) : base(view) { }
        }

        protected class DefaultViewHolder : RecyclerView.ViewHolder
        {
            public DefaultViewHolder(View view) : base(view) { }
        }

        public DocumentsAdapter(CreateTaskViewModel claimsDocumentsViewModel, IMvxAndroidBindingContext bindingContext, Context context)
           : base(bindingContext)
        {
            this.viewModel = claimsDocumentsViewModel;
            this.context = context;
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0)
            {
                return DefaultType;
            }
            return ItemType;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder viewHolder = null;
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);

            if (viewType == ItemType)
            {
                var view = itemBindingContext.BindingInflate(Resource.Layout.item_document, parent, false);
                viewHolder = new DocumentItemViewHolder(view, itemBindingContext);
            }
            else if (viewType == DefaultType)
            {
                var view = itemBindingContext.BindingInflate(Resource.Layout.item_defaultDocument, parent, false);
                viewHolder = new DefaultItemViewHolder(view, itemBindingContext, OnClick);
            }
            return viewHolder;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            if (position > 0)
            {
                if (holder is DocumentItemViewHolder)
                {
                    var documentItemViewHolder = holder as DocumentItemViewHolder;
                    var base64String = viewModel.Documents[position].Data;

                    var imageBytes = Base64.Decode(base64String, Base64Flags.Default);
                    using (Bitmap decodedImage = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length))
                    {
                        documentItemViewHolder.DocumentImageView?.SetImageBitmap(decodedImage);
                        imageBytes = null;
                    }
                }
            }
        }

        public override void OnViewRecycled(Java.Lang.Object holder)
        {
            var DocumentItemViewHolder = holder as DocumentItemViewHolder;
            Bitmap bitmap = ((BitmapDrawable)DocumentItemViewHolder?.DocumentImageView?.Drawable)?.Bitmap;
            bitmap?.Recycle();
            bitmap = null;

            base.OnViewRecycled(holder);
        }

        private async void OnClick(int position)
        {
            if (position > 0)
            {
                return;
            }
            else
            {
                ShowPickerView();
            }
        }

        private void ShowPickerView()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            ((Activity)context).StartActivityForResult(Intent.CreateChooser(intent, "Select a Photo"), REQUEST_PICK_PHOTO);
        }

        public void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_PICK_PHOTO)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    Uri galleryImageURI = data.Data;

                    var bitmap = MediaStore.Images.Media.GetBitmap(context.ContentResolver, galleryImageURI);

                    MemoryStream outputStream = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Png, 100, outputStream);

                    var newImage = Base64.EncodeToString(outputStream.ToArray(), Base64.Default);

                    viewModel.AddImage(newImage);
                }
            }
        }

        private void AddImageToDocumentCollection(string documentImageUrl, string originalImageUrl)
        {
            byte[] documentImageArray;

            //var documentImageUri = Uri.Parse(documentImageUrl);
            //var documentImage = ImageUtils.LoadImage(documentImageUri, context);

            ////todo: disposable logic addition for bitmap
            //documentImageArray = ImageUtils.ResizeImage(documentImage, documentImage.Width, documentImage.Height, 70);
            //var documentImageBase64String = Convert.ToBase64String(documentImageArray);
            ////add image
            //viewModel.LoadDocuments();

        }
        #region ViewHolders

        protected class DefaultItemViewHolder : MvxRecyclerViewHolder
        {
            private readonly Action<int> listener;

            public DefaultItemViewHolder(View itemView, IMvxAndroidBindingContext context, Action<int> listener) : base(itemView, context)
            {
                this.listener = listener;
                ItemView.Click += OnItemClick;
            }

            private void OnItemClick(object sender, EventArgs e)
            {
                listener(AdapterPosition);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    ItemView.Click -= OnItemClick;

                base.Dispose(disposing);
            }
        }

        protected class DocumentItemViewHolder : MvxRecyclerViewHolder
        {
            public ImageView DocumentImageView;

            public DocumentItemViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
            {
                DocumentImageView = itemView.FindViewById<ImageView>(Resource.Id.imageViewDocument);
            }


            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }
        }

        protected override void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsSourceCollectionChanged(sender, e);

        }
        #endregion ViewHolders
    }
}
