using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KingTranslator
{
    public partial class TranslatePage : ContentPage
    {
        private TranslatorItem phrase;

        private Database storehouse;

        public TranslatePage()
        {
            InitializeComponent();

            storehouse = new Database();

            var phrases = storehouse.GetAllPhrases();

            var toolbarItem = new ToolbarItem
            {
                Text = "Add",
                Icon = String.Format("{0}{1}", Device.OnPlatform("", "", "Assets/"), "addicon.png"),
                Command = new Command(() =>
                {
                    if (phrase == null)
                    {
                        DisplayAlert("King Translator", "Nothing to add.", "OK");
                    }
                    else
                    {
                        if (storehouse.PhraseExists(phrase))
                        {
                            DisplayAlert("King Translator", "'" + phrase.Phrase + "' already in dictionary.", "OK");
                        }
                        else
                        {
                            storehouse.AddPhrase(phrase);
                            DisplayAlert("Success!", "'" + phrase.Phrase + "' added to dictionary.", "OK");
                        }
                    }
                })
            };

            // Enter pressed
            phraseInput.Completed += PhraseInput_Completed;
            phraseInput.TextChanged += PhraseInput_TextChanged;


            // OR THIS WAY
            //var toolbarItem = new ToolbarItem();
            //toolbarItem.Text = "+";
            //toolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("SavePhrase"));

            // UI STUFF
            ToolbarItems.Add(toolbarItem);
            //phraseInput.IsEnabled = false;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                chineseBtn.Font = Font.SystemFontOfSize(NamedSize.Small);
                englishBtn.Font = Font.SystemFontOfSize(NamedSize.Small);
                pinyin.Font = Font.SystemFontOfSize(NamedSize.Small);
                translatedPhrase.Font = Font.SystemFontOfSize(NamedSize.Small);

            }
            else

            {
                chineseBtn.Font = Font.SystemFontOfSize(NamedSize.Large);
                englishBtn.Font = Font.SystemFontOfSize(NamedSize.Large);
                pinyin.Font = Font.SystemFontOfSize(NamedSize.Large);
                translatedPhrase.Font = Font.SystemFontOfSize(NamedSize.Large);
            }

            //Padding = new Thickness(Device.OnPlatform(0, 12, 0), Device.OnPlatform(20, 0, 0), Device.OnPlatform(0, 12, 0), 0);

            //var btn = new SuperButton();
            //btn.ButtonControl.Text = "Awesome!";
            //btn.ButtonControl.Font = Font.SystemFontOfSize(NamedSize.Large);

            //buttons.Children.Add(btn);

            App.TranslatorManager.TranslationChanged += TranslatorManager_TranslationChanged;
        }

        private void PhraseInput_Completed(object sender, EventArgs e)
        {
            phraseInput.Unfocus();
            InitializeTranslatorParadigm(Language.English, Language.Chinese);
        }

        private void PhraseInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string last = "";
            if (String.IsNullOrEmpty(e.NewTextValue) == false)
                if (e.NewTextValue.Length > 2)
                    last = e.NewTextValue.Substring(e.NewTextValue.Length - 1, 1);
            if (last == "\n")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    chineseBtn.Focus();
                    InitializeTranslatorParadigm(Language.English, Language.Chinese);
                });
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.WidthRequest || height != this.HeightRequest)
            {
                this.WidthRequest = width;
                this.HeightRequest = height;

                if(Width < Height)
                {
                    perimeter.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    perimeter.ColumnDefinitions[1].Width = new GridLength(0);

                    perimeter.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    perimeter.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
                    
                    Grid.SetRow(translatorLayout, 0);
                    Grid.SetColumn(translatorLayout, 0);

                    Grid.SetRow(resultsLayout, 1);
                    Grid.SetColumn(resultsLayout, 0);
                }
                else
                {
                    perimeter.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    perimeter.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

                    perimeter.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    perimeter.RowDefinitions[1].Height = new GridLength(0);

                    Grid.SetRow(translatorLayout, 0);
                    Grid.SetColumn(translatorLayout, 1);

                    Grid.SetRow(resultsLayout, 0);
                    Grid.SetColumn(resultsLayout, 0);
                }
                

                // USE THIS IF USING STACKPANELS!
                //if (width > height)
                //{
                //    perimeter.Orientation = StackOrientation.Horizontal;
                //}
                //else
                //{
                //    perimeter.Orientation = StackOrientation.Vertical;
                //}
            }
        }

        async void SavePhrase(object sender, EventArgs e)
        {
            await App.TranslatorManager.SaveToDictionary(phrase);
        }

        private void TranslatorManager_TranslationChanged(object sender, TranslatorItemManager.TranslationChangedArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.Phrase.TranslatedPhraseLanguage == Language.English.GetStringValue())
                {
                    pinyin.IsVisible = false;
                }
                else
                {
                    pinyin.IsVisible = true;
                }

                translatedPhrase.Text = e.Phrase.TranslatedPhrase;
                pinyin.Text = e.Phrase.Pinyin;
            });
        }

        void ChineseButtonClicked(object sender, EventArgs e)
        {
            InitializeTranslatorParadigm(Language.English, Language.Chinese);
        }

        void EnglishButtonClicked(object sender, EventArgs e)
        {
            InitializeTranslatorParadigm(Language.Chinese, Language.English);
        }

        private void InitializeTranslatorParadigm(Language fromLang, Language toLang)
        {
            if (!AllowTranslation())
            {
                return;
            }

            phrase = new TranslatorItem
            {
                Phrase = phraseInput.Text,
                PhraseLanguage = fromLang.GetStringValue(),
                TranslatedPhraseLanguage = toLang.GetStringValue()
            };

            App.TranslatorManager.TranslatePhrase(phrase);
        }

        private bool AllowTranslation()
        {
            if (string.IsNullOrWhiteSpace(phraseInput.Text))
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DisplayAlert("King Translator", "Unable to connect to the Internet.", "OK");

                    return false;
                }

                DisplayAlert("King Translator", "Type something to translate.", "OK");

                return false;
            }

            return true;
        }
    }

    public static class XamarinFormsUtil
    {
        public static View WithPadding(this View view, double all)
        {
            return WithPadding(view, all, all, all, all);
        }

        public static View WithPadding(this View view, double left, double top, double right, double bottom)
        {
            return new Frame()
            {
                Content = view,
                Padding = new Thickness(left, top, right, bottom)
            };
        }
    }
}
