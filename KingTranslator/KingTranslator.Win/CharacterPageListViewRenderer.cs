using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using KingTranslator;
using KingTranslator.Win;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CharacterPageListView), typeof(CharacterPageListViewRenderer))]
namespace KingTranslator.Win
{
    public class CharacterPageListViewRenderer : ListViewRenderer
    {
        ListView listView;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            listView = Control as ListView;

            if (e.OldElement != null)
            {
                // Unsubscribe
                listView.SelectionChanged -= OnSelectedItemChanged;
            }
            if (e.NewElement != null)
            {
                listView.SelectionMode = ListViewSelectionMode.Single;
                listView.IsItemClickEnabled = false;
                listView.ItemsSource = ((CharacterPageListView)e.NewElement).Items;

                // Subscribe
                listView.SelectionChanged += OnSelectedItemChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CharacterPageListView.ItemsProperty.PropertyName)
            {
                listView.ItemsSource = ((CharacterPageListView)Element).Items;
            }
        }

        void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            ((CharacterPageListView)Element).NotifyItemSelected(listView.SelectedItem);
        }
    }
}