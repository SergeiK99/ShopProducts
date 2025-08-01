using ShopProducts.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ShopProducts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            // Явно устанавливаем DataContext
            var viewModel = new StoreViewModel();
            this.DataContext = viewModel;
            
            System.Diagnostics.Debug.WriteLine($"MainPage constructor - DataContext set: {DataContext != null}");
            System.Diagnostics.Debug.WriteLine($"Initial products count: {viewModel.Products.Count}");
            
            this.Loaded += StorePage_Loaded;
        }

        private async void StorePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("MainPage loaded, starting to load products...");
            
            if (DataContext is StoreViewModel vm)
            {
                System.Diagnostics.Debug.WriteLine("DataContext is StoreViewModel, loading products...");
                await vm.LoadProductsAsync();
                System.Diagnostics.Debug.WriteLine($"Products loaded, count: {vm.Products.Count}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DataContext is not StoreViewModel!");
            }
        }
    }
}
