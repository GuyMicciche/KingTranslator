using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using KingTranslator;
using Xamarin.Forms.Platform.WinPhone;
using Microsoft.Phone.Controls;
using KingTranslator.WinPhone;

[assembly: ExportRenderer(typeof(CarouselPage), typeof(CustomCarouselPageRenderer))]
namespace KingTranslator.WinPhone
{
    public class CustomCarouselPageRenderer : CarouselPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var titleTemplate = App.Current.Resources.SingleOrDefault(x => x.Key.ToString() == "PanoramaTitle").Value as System.Windows.DataTemplate;
            if (titleTemplate != null)
            {
                TitleTemplate = titleTemplate;
            }

            var headerTemplate = App.Current.Resources.SingleOrDefault(x => x.Key.ToString() == "PanoramaItemHeader").Value as System.Windows.DataTemplate;
            if (headerTemplate != null)
            {
                HeaderTemplate = headerTemplate;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i] as PanoramaItem;
                var page = Element as CarouselPage;
                item.Header = page.Children[i].Title;

                var content = item.Content as CarouselPagePresenter;
                content.Margin = new System.Windows.Thickness(0, -30, 0, 0);
            }
        }
    }
}
