using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KingTranslator
{
	public partial class MainPage : TabbedPage
    {
        private DictionaryPage dictionaryPage;

        private static Color BG_COLOR = Color.FromHex("#FFB30808");

        public MainPage()
		{
			InitializeComponent();

#if __ANDROID__
            Title = "King Translator";
#elif __IOS__
            Title = "King Translator";
#else
            Title = "King Translator";
#endif

            dictionaryPage = new DictionaryPage();
            dictionaryPage.Title = "Dictionary";
            dictionaryPage.BackgroundColor = BG_COLOR;

            //this.Children.Add(new NavigationPage(new TranslatePage()) { Title = "Translate" });
            //this.Children.Add(new NavigationPage(dictionaryPage) { Title = "Dictionary" });
            //this.Children.Add(new NavigationPage(new CharacterPage()) { Title = "Characters" });

            this.Children.Add(new TranslatePage() { Title = "Translate" , BackgroundColor = BG_COLOR });
            this.Children.Add(dictionaryPage);
            this.Children.Add(new CharacterPage() { Title = "Characters", BackgroundColor = BG_COLOR });
        }

        protected override void OnCurrentPageChanged()
        {
            dictionaryPage.Refresh();
        }
    }
}
