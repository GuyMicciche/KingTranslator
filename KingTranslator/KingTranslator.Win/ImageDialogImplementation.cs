using KingTranslator;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

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

            var image = new Windows.UI.Xaml.Controls.Image()
            {
                Source = windowsImageSource
            };

            if (image != null)
            {
                Popup popup = new Popup();

                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 244, 244, 244));
                stackPanel.Width = Application.Current.MainPage.Width;

                var button = new Windows.UI.Xaml.Controls.Button();
                button.Content = "X";
                button.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Black);
                button.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Black);
                button.Click += (sender,e) =>
                {
                    popup.IsOpen = false;
                };

                if (!string.IsNullOrEmpty(message))
                {
                    var label = new Windows.UI.Xaml.Controls.TextBlock()
                    {
                        Text = message,
                        FontSize = 48,
                        Padding = new Windows.UI.Xaml.Thickness(8),
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
                popup.DragLeave += (sender, e) =>
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
                returnValue = new UriImageSourceHandler();
            }
            else if (imageSource is Xamarin.Forms.FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (imageSource is Xamarin.Forms.StreamImageSource)
            {
                returnValue = new StreamImageSourceHandler();
            }
            return returnValue;
        }

        #endregion
    }
}