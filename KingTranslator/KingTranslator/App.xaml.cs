using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KingTranslator
{
    public partial class App : Application
    {
        public static TranslatorItemManager TranslatorManager { get; set; }

        public App()
        {
            // The root page of your application
            //MainPage = new KingTranslator.MainPage();

            var page = new NavigationPage(new KingTranslator.MainPage());
            page.BarBackgroundColor = Color.FromHex("#FFB30808");
            page.BarTextColor = Color.FromHex("#FFFFFFFF");

            MainPage = page;

            //MainPage = new ContentPage {
            //	Content = new StackLayout {
            //		VerticalOptions = LayoutOptions.Center,
            //		Children = {
            //			new Label {
            //				XAlign = TextAlignment.Center,
            //				Text = "Welcome to Xamarin Forms!"
            //			}
            //		}
            //	}
            //};

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
