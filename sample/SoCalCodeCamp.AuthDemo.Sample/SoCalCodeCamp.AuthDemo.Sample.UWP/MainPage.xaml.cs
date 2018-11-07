using Microsoft.Identity.Client;
using Prism;
using Prism.Ioc;
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

namespace SoCalCodeCamp.AuthDemo.Sample.UWP
{
    public sealed partial class MainPage : IPlatformInitializer
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new SoCalCodeCamp.AuthDemo.Sample.App(this));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(new UIParent());
        }
    }
}
