using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KingTranslator
{
    public interface IImageDialog
    {
        Task Display(ImageSource imageSource);

        Task Display(string message, ImageSource imageSource);
    }
}