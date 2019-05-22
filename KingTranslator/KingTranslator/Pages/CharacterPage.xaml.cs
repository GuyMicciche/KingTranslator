using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Xml.Linq;
//using SimpleControls.ImageDialog;
using Plugin.Connectivity;
using System.Threading;

namespace KingTranslator
{
    public partial class CharacterPage : ContentPage
    {
        ObservableCollection<Character> data = null;

        public const string URL = "http://content.visualmandarin.com/";

        public CharacterPage()
        {
            InitializeComponent();

            LoadUI();

            searchBar.TextChanged += (sender, e) => FilterCharacters(searchBar.Text);
        }

        private void FilterCharacters(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    characterList.ItemsSource = data;
                });
            }
            else
            {
                img.IsVisible = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    characterList.ItemsSource = data.Where(x => x.English.ToLower().Contains(text.ToLower()) || x.Simplified.ToLower().Contains(text.ToLower()));
                });
            }
        }

        private void LoadUI()
        {
            characterList.ItemTapped += CharacterList_ItemTapped;
            characterList.ItemSelected += CharacterList_ItemSelected;
            characterList.BackgroundColor = Color.FromHex("#FFB30808");

            characterList.ItemTemplate = new DataTemplate(typeof(TextCell));
            characterList.ItemTemplate.SetBinding(TextCell.TextProperty, "Header");
            characterList.ItemTemplate.SetBinding(TextCell.DetailProperty, "English");
            characterList.ItemTemplate.SetValue(TextCell.TextColorProperty, Color.White);
            characterList.ItemTemplate.SetValue(TextCell.DetailColorProperty, Color.White);

            //BindingContext = new CharactersViewModel();

            LoadCharacters();
        }

        public void LoadCharacters()
        {
            Task.Run(() =>
            {
                Assembly assembly = typeof(CharacterPage).GetTypeInfo().Assembly;
                string file = assembly.GetManifestResourceNames().First(x => x.Contains("Characters.xml"));
                Stream stream = assembly.GetManifestResourceStream(file);

                data = new ObservableCollection<Character>();
                XDocument doc = XDocument.Load(stream);

                IEnumerable<Character> characters = from query in doc.Descendants("c")
                                                    select new Character
                                                    {
                                                        Simplified = query.Attribute("s").Value,
                                                        Traditional = query.Attribute("t").Value,
                                                        Pinyin = query.Attribute("p").Value,
                                                        English = query.Attribute("e").Value,
                                                        URL = query.Attribute("u").Value
                                                    };
                data = characters.ToObservableCollection();

                Device.BeginInvokeOnMainThread(() =>
                {
                    characterList.ItemsSource = data;
                });

            });
            //await Task.Factory.StartNew(delegate
            //{

            //    XDocument doc = XDocument.Load(stream);

            //    IEnumerable<Character> characters = from query in doc.Descendants("c")
            //                                        select new Character
            //                                        {
            //                                            Simplified = query.Attribute("s").Value,
            //                                            Traditional = query.Attribute("t").Value,
            //                                            Pinyin = query.Attribute("p").Value,
            //                                            English = query.Attribute("e").Value,
            //                                            URL = query.Attribute("u").Value
            //                                        };
            //    data = characters.ToObservableCollection();
            //});
        }

        private void CharacterList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // ItemSelected is called on de-selection, which results in SelectedItem being set to null
            if (e.SelectedItem == null)
            {
                return;
            }
            // Comment out if you want to keep selections
            ListView lst = (ListView)sender;
            lst.SelectedItem = null;

            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("King Translator", "Unable to connect to the Internet.", "OK");
            }
        }

        private async void CharacterList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Character character = (Character)e.Item;

            img.IsVisible = true;
            img.Source = URL + character.URL;
            img.BackgroundColor = Color.FromHex("#F4F4F4");

            headerLabel.Text = character.Header;
            englishLabel.Text = character.English;
            englishLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));

            //await ImageDialog.Current.Display(ImageSource.FromFile("icon.png"));
            //await ImageDialog.Current.Display(character.Header, ImageSource.FromUri(new Uri(URL + character.URL)));

            //var imageDialogService = DependencyService.Get<IImageDialog>();
            //await imageDialogService.Display(character.Header, ImageSource.FromUri(new Uri(URL + character.URL)));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.WidthRequest || height != this.HeightRequest)
            {
                this.WidthRequest = width;
                this.HeightRequest = height;

                // Vertical favor
                if (Width < Height)
                {
                    perimeter.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    perimeter.ColumnDefinitions[1].Width = new GridLength(0);

                    perimeter.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    perimeter.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                    Grid.SetRow(imageLayout, 0);
                    Grid.SetColumn(imageLayout, 0);

                    Grid.SetRow(listLayout, 1);
                    Grid.SetColumn(listLayout, 0);
                }
                // Horizontal favor
                else
                {
                    perimeter.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    perimeter.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

                    perimeter.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    perimeter.RowDefinitions[1].Height = new GridLength(0);

                    Grid.SetRow(imageLayout, 0);
                    Grid.SetColumn(imageLayout, 0);

                    Grid.SetRow(listLayout, 0);
                    Grid.SetColumn(listLayout, 1);
                }
            }
        }
    }
}