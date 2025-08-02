using ShopProducts.ViewModels;
using ShopProducts.Models;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using System;

namespace ShopProducts
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            System.Diagnostics.Debug.WriteLine("MainPage constructor completed");
        }

        private async void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("MainPage loaded, loading products...");
            
            try
            {
                var viewModel = new StoreViewModel();
                await viewModel.LoadProductsAsync();
                
                System.Diagnostics.Debug.WriteLine($"Loaded {viewModel.Products.Count} products");
                
                // Добавляем элементы в StackPanel
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        ProductsPanel.Children.Clear();
                        foreach (var product in viewModel.Products)
                        {
                            var border = new Border
                            {
                                BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.LightGray),
                                BorderThickness = new Windows.UI.Xaml.Thickness(1),
                                Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.DarkBlue),
                                Margin = new Windows.UI.Xaml.Thickness(10),
                                Padding = new Windows.UI.Xaml.Thickness(15),
                                CornerRadius = new Windows.UI.Xaml.CornerRadius(8)
                            };
                            
                            var stackPanel = new StackPanel();
                            
                            // Название продукта
                            var nameText = new TextBlock
                            {
                                Text = product.Name,
                                Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White),
                                FontSize = 18,
                                FontWeight = Windows.UI.Text.FontWeights.Bold,
                                Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 8)
                            };
                            
                            // ID продукта
                            var idText = new TextBlock
                            {
                                Text = $"ID: {product.Id}",
                                Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.LightGray),
                                FontSize = 12,
                                Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 4)
                            };
                            
                            // Цена
                            var priceText = new TextBlock
                            {
                                Text = product.FormattedPrice,
                                Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.LightGreen),
                                FontSize = 16,
                                FontWeight = Windows.UI.Text.FontWeights.SemiBold,
                                Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 8)
                            };
                            
                            // Кнопка "Добавить в корзину"
                            var button = new Button
                            {
                                Content = "Добавить в корзину",
                                Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Green),
                                Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White),
                                FontWeight = Windows.UI.Text.FontWeights.Bold,
                                Padding = new Windows.UI.Xaml.Thickness(10, 5, 10, 5),
                                CornerRadius = new Windows.UI.Xaml.CornerRadius(4)
                            };
                            
                            stackPanel.Children.Add(nameText);
                            stackPanel.Children.Add(idText);
                            stackPanel.Children.Add(priceText);
                            stackPanel.Children.Add(button);
                            
                            border.Child = stackPanel;
                            ProductsPanel.Children.Add(border);
                        }
                        
                        System.Diagnostics.Debug.WriteLine($"Added {viewModel.Products.Count} items to UI");
                    });
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
