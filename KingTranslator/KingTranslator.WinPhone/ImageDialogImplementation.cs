using KingTranslator;
using Microsoft.Phone.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: Xamarin.Forms.Dependency(typeof(ImageDialogImplementation))]

namespace KingTranslator
{
    public class ImageDialogImplementation : IImageDialog
    {
        public static void Init() { }

        public async Task Display(ImageSource imageSource)
        {
            await Display(null, imageSource);
        }

        public async Task Display(string message, ImageSource imageSource)
        {
            var handler = GetHandler(imageSource);
            var windowsImageSource = await handler.LoadImageAsync(imageSource);

            var image = new System.Windows.Controls.Image()
            {
                Source = windowsImageSource
            };

            if (image != null)
            {
                Popup popup = new Popup();

                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 244));
                stackPanel.Width = Application.Current.MainPage.Width;

                var button = new System.Windows.Controls.Button();
                button.Content = "X";
                button.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                button.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                button.Click += (sender,e) =>
                {
                    popup.IsOpen = false;
                };

                if (!string.IsNullOrEmpty(message))
                {
                    var label = new System.Windows.Controls.TextBlock()
                    {
                        Text = message,
                        FontSize = 48,
                        Padding = new System.Windows.Thickness(8),
                    };
                    stackPanel.Children.Add(label);
                }

                stackPanel.Children.Add(image);
                stackPanel.Children.Add(button);

                popup = new Popup()
                {
                    Child = stackPanel,
                    IsOpen = true,
                    VerticalOffset = 30
                };
                popup.MouseLeave += (sender, e) =>
                {
                    ((Popup)sender).IsOpen = false;
                };
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