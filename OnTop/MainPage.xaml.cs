using System;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace OnTop
{
    public sealed partial class MainPage : Page
    {
        readonly int CustomOverlayHeight = 500;
        readonly int CustomOverlayWidth = 500;
        readonly bool UseCustomSize = true;
        readonly string URL = "https://www.youtube.com/";

        public MainPage()
        {
            InitializeComponent();

            var isSupported = ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay);
            if (isSupported)
                SetViewModeAsync();

            WebView1.Navigate(new Uri(URL));
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
