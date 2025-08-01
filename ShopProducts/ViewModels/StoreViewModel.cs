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
            // Добавляем тестовые данные для проверки привязки
            Products.Add(new Product { Id = "test1", Name = "Тестовый продукт 1", Price = 100, ImagePath = "Assets/images/mouse.png" });
            Products.Add(new Product { Id = "test2", Name = "Тестовый продукт 2", Price = 200, ImagePath = "Assets/images/mouse.png" });
            System.Diagnostics.Debug.WriteLine("StoreViewModel created with test data");
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
                    Products.Clear();
                    foreach (var product in products)
                    {
                        Products.Add(product);
                        System.Diagnostics.Debug.WriteLine($"Added: {product.Name}");
                    }
                    System.Diagnostics.Debug.WriteLine($"Final count: {Products.Count}");
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
