using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace KingTranslator.Droid
{
    [Activity(Label = "King Translator", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);            

            App.TranslatorManager = new TranslatorItemManager(Translator.Default);

            LoadApplication(new App());
        }
    }
}