using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Prism.Ioc;
using Microsoft.Identity.Client;

namespace SoCalCodeCamp.AuthDemo.Sample.Droid
{
    [Activity(Label = "SoCalCodeCamp.AuthDemo.Sample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPlatformInitializer
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(this));
        }

        void IPlatformInitializer.RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(new UIParent(this, true));
        }
    }
}