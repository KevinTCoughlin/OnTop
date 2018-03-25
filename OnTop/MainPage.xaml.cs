using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace OnTop
{
    public sealed partial class MainPage : Page
    {
        readonly int CustomOverlayHeight = 500;
        readonly int CustomOverlayWidth = 500;
        readonly bool UseCustomSize = true;

        public MainPage()
        {
            InitializeComponent();

            var isSupported = ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay);
            if (isSupported)
            {
                SetViewModeAsync();
            }
        }

        private void OnKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            NavigateToUrl(UrlInput.Text);
        }

        private void NavigateToUrl(string urlStr)
        {
            try
            {
                var uri = new Uri(urlStr);
                WebView1.Navigate(uri);
            }
            catch (FormatException e)
            {
                Debug.WriteLine(String.Format("URL is invalid, try again.  Details --> {0}", e.Message));
            }
        }

        private async Task SetViewModeAsync()
        {
            bool modeSwitched;
            if (UseCustomSize)
            {
                var compactOptions = ViewModePreferences.CreateDefault(ApplicationViewMode.CompactOverlay);
                compactOptions.CustomSize = new Windows.Foundation.Size(CustomOverlayHeight, CustomOverlayWidth);
                modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay, compactOptions);
            }
            else
            {
                modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
            }
        }
    }
}
