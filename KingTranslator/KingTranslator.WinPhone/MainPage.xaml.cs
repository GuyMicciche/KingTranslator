using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;

namespace KingTranslator.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();

            KingTranslator.App.TranslatorManager = new TranslatorItemManager(Translator.Default);

            LoadApplication(new KingTranslator.App());

            SystemTray.SetProgressIndicator(this, new ProgressIndicator() { Text = "King Translator", IsVisible = true });
            SystemTray.SetBackgroundColor(this, ((System.Windows.Media.SolidColorBrush)System.Windows.Application.Current.Resources["Brush1"]).Color);
        }
    }
}
