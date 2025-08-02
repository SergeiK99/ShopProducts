using ShopProducts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace ShopProducts.ViewModels
{
    public class StoreViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; } = new();

        public StoreViewModel()
        {
            System.Diagnostics.Debug.WriteLine("StoreViewModel created");
        }

        public async Task LoadProductsAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting LoadProductsAsync...");
                
                var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var file = await folder.GetFileAsync("Assets\\products.json");
                System.Diagnostics.Debug.WriteLine($"File found: {file.Name}");
                
                var jsonText = await FileIO.ReadTextAsync(file);
                System.Diagnostics.Debug.WriteLine($"JSON content: {jsonText}");
                
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonText);
                System.Diagnostics.Debug.WriteLine($"Deserialized {products?.Count ?? 0} products");

                if (products != null)
                {
                    // Создаем новую коллекцию
                    var newProducts = new ObservableCollection<Product>(products);
                    
                    // Заменяем старую коллекцию
                    Products = newProducts;
                    
                    // Уведомляем UI об изменении свойства
                    OnPropertyChanged(nameof(Products));
                    
                    System.Diagnostics.Debug.WriteLine($"Final count: {Products.Count}");
                    
                    // Принудительно обновляем UI
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                        Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            OnPropertyChanged(nameof(Products));
                        });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
