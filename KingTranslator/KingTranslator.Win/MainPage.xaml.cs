using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KingTranslator.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            KingTranslator.App.TranslatorManager = new TranslatorItemManager(Translator.Default);

            LoadApplication(new KingTranslator.App());


            //SystemTray.SetProgressIndicator(this, new ProgressIndicator() { Text = "King Translator", IsVisible = true });
            //SystemTray.SetBackgroundColor(this, ((System.Windows.Media.SolidColorBrush)System.Windows.Application.Current.Resources["Brush1"]).Color);
        }
    }
}
