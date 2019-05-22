using Android.App;
using Android.Widget;
using KingTranslator;
using KingTranslator.Droid;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(ImageDialogImplementation))]

namespace KingTranslator
{
    public class ImageDialogImplementation : IImageDialog
    {
        public static void Init() { }

        public async Task Display(Xamarin.Forms.ImageSource imageSource)
        {
            await Display(null, imageSource);
        }

        public async Task Display(string message, Xamarin.Forms.ImageSource imageSource)
        {
            var handler = GetHandler(imageSource);
            var context = (Activity)Xamarin.Forms.Forms.Context;

            var image = await handler.LoadImageAsync(imageSource, context);

            if (image != null)
            {
                var imageView = new ImageView(context);
                imageView.SetImageBitmap(image);

                var layout = new GridLayout(context);
                layout.AddView(imageView);
                layout.SetBackgroundResource(KingTranslator.Droid.Resource.Color.webBG);

                var builder = new AlertDialog.Builder(context).SetView(layout).SetCancelable(true);

                if (!string.IsNullOrWhiteSpace(message))
                {
                    builder.SetMessage(message);
                }

                builder.Create().Show();
            }
            else
            {
                throw new ArgumentNullException("Image is null");
            }
        }

        #region Private Methods

        private IImageSourceHandler GetHandler(Xamarin.Forms.ImageSource imageSource)
        {
            IImageSourceHandler returnValue = null;
            if (imageSource is Xamarin.Forms.UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (imageSource is Xamarin.Forms.FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (imageSource is Xamarin.Forms.StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        #endregion
    }
}