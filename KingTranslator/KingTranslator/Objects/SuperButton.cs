using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KingTranslator
{
    public class SuperButton : Frame
    {
        private Button superButton;

        public Button ButtonControl
        {
            get
            {
                return superButton;
            }
        }

        public SuperButton()
        {
            this.Padding = new Thickness(15, 0, 15, 0);
            this.HasShadow = false;
            superButton = new Button();
            superButton.BorderWidth = 1; 
            Content = superButton;
        }
    }
}
