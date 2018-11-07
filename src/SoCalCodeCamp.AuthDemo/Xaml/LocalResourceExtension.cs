using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SoCalCodeCamp.AuthDemo.Xaml
{
    [ContentProperty(nameof(FileName))]
    [AcceptEmptyServiceProvider]
    internal class LocalResourceExtension : IMarkupExtension
    {
        public string FileName { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var assembly = GetType().Assembly;
            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(FileName, StringComparison.InvariantCultureIgnoreCase));

            if(string.IsNullOrWhiteSpace(resourceName))
            {
                Log.Warning("Warning", $"No Embedded Resource could be found with the name '{FileName}'");
                return null;
            }

            return ImageSource.FromStream(() => assembly.GetManifestResourceStream(resourceName));
        }
    }
}
