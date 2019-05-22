using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KingTranslator
{
	public partial class DictionaryPage : ContentPage
	{
        public static ObservableCollection<TranslatorItem> Items { get; set; }

        public static Database Storehouse { get; set; }

        public DictionaryPage()
		{
            InitializeComponent();

            Storehouse = new Database();

            phraseList.ItemTemplate = new DataTemplate(typeof(DictionaryCell));
            phraseList.BackgroundColor = Color.FromHex("#FFB30808");
            phraseList.ItemTapped += PhraseList_ItemTapped;
            //phraseList.ItemSelected += PhraseList_ItemSelected;
            //phraseList.ItemTemplate.SetBinding(TextCell.TextProperty, "Phrase");
            //phraseList.ItemTemplate.SetBinding(TextCell.DetailProperty, "TranslatedPhrase");

            var toolbarItem = new ToolbarItem
            {
                Text = "New",
                //Command = new Command(() => Navigation.PushAsync(new ThoughtEntryPage(this, database)))
                Command = new Command(() => DisplayAlert("King Translator", "Coming soon...", "OK")),
                Icon = String.Format("{0}{1}", Device.OnPlatform("", "", "Assets/"), "addicon.png")
            };

            ToolbarItems.Add(toolbarItem);
        }

        private void PhraseList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // ItemSelected is called on de-selection, which results in SelectedItem being set to null
            if (e.SelectedItem == null)
            {
                return;
            }
            // Comment out if you want to keep selections
            ListView lst = (ListView)sender;
            lst.SelectedItem = null;
        }

        private void PhraseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            TranslatorItem phrase = (TranslatorItem)e.Item;

            if (phrase.TranslatedPhraseLanguage == Language.Chinese.GetStringValue())
            {
                DisplayAlert(phrase.Phrase, phrase.Pinyin + "\n" + phrase.TranslatedPhrase, "OK");
            }
            else
            {
                DisplayAlert(phrase.Phrase, phrase.TranslatedPhrase, "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Refresh();
        }

        public void Refresh() 
        {
            Items = new ObservableCollection<TranslatorItem>(Storehouse.GetAllPhrases());
            
            phraseList.ItemsSource = Items;
        }
    }

    public class DictionaryCell : TextCell
    {
        public DictionaryCell()
        {
            SetBinding(TextProperty, new Binding("Phrase"));
            SetBinding(DetailProperty, new Binding("TranslatedPhrase"));            
            SetValue(TextColorProperty, Color.White);
            SetValue(DetailColorProperty, Color.White);
         
            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += OnDelete;

            this.ContextActions.Add(deleteAction);
        }

        void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            TranslatorItem phrase = (TranslatorItem)item.CommandParameter;

            DictionaryPage.Items.Remove(phrase);
            DictionaryPage.Storehouse.DeletePhrase(phrase.ID);
        }
    }
}
