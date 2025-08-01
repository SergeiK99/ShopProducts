using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProducts.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        
        public string FormattedPrice => $"{Price:C}";
        
        // Добавляем свойство для корректного пути к изображению в UWP
        public Uri ImageSourceUri => new Uri($"ms-appx:///{ImagePath}");
    }
}
