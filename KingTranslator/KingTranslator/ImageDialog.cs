using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingTranslator
{
    public class ImageDialog
    {
        static Lazy<IImageDialog> Implementation = new Lazy<IImageDialog>(() => CreateImageDialog(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IImageDialog Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IImageDialog CreateImageDialog()
        {
#if PORTABLE
        return null;
#else
            return new ImageDialogImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }


        /// <summary>
        /// Dispose of everything 
        /// </summary>
        public static void Dispose()
        {
            if (Implementation != null && Implementation.IsValueCreated)
            {
                Implementation = new Lazy<IImageDialog>(() => CreateImageDialog(), System.Threading.LazyThreadSafetyMode.PublicationOnly);
            }
        }
    }
}