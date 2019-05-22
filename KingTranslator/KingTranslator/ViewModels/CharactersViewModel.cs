using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace KingTranslator
{

    public class CharactersViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Character> characters;
        public ObservableCollection<Character> Characters
        {
            get { return characters; }
            set
            {

                characters = value;
                OnPropertyChanged("Items");
            }
        }


        public CharactersViewModel()
        {
            // Here you can have your data form db or something else,
            // some data that you already have to put in the list

            Characters = new ObservableCollection<Character>();

            Assembly assembly = typeof(CharacterPage).GetTypeInfo().Assembly;
            string file = assembly.GetManifestResourceNames().First(x => x.Contains("Characters.xml"));
            Stream stream = assembly.GetManifestResourceStream(file);
            XDocument doc = XDocument.Load(stream);

            Characters = (from query in doc.Descendants("c")
                         select new Character
                         {
                             Simplified = query.Attribute("s").Value,
                             Traditional = query.Attribute("t").Value,
                             Pinyin = query.Attribute("p").Value,
                             English = query.Attribute("e").Value,
                             URL = query.Attribute("u").Value
                        }).ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}