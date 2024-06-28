using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab10_11
{
    public class Television
    {
        public string Brand { get; set; }
        public double Diagonal { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"Фирма: {Brand}, Диагональ: {Diagonal} дюймов, Стоимость: {Price} руб.";
        }
    }

    public partial class Lab11 : Page
    {
        private List<Television> televisions = new List<Television>();

        public Lab11()
        {
            InitializeComponent();
        }

        private void AddTv_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BrandName.Text) || !double.TryParse(DiagonalSize.Text, out double diagonal) || !double.TryParse(Price.Text, out double price))
            {
                MessageBox.Show("Введите корректные данные для всех полей.");
                return;
            }

            var tv = new Television
            {
                Brand = BrandName.Text,
                Diagonal = diagonal,
                Price = price
            };

            televisions.Add(tv);
            TvPanel.Children.Add(new TextBlock { Text = tv.ToString(), Margin = new Thickness(5) });

            BrandName.Clear();
            DiagonalSize.Clear();
            Price.Clear();

            UpdateSamsungCount();
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(televisions, options);
            File.WriteAllText("televisions.json", jsonString);
        }

        private void LoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("televisions.json"))
            {
                var jsonString = File.ReadAllText("televisions.json");
                televisions = JsonSerializer.Deserialize<List<Television>>(jsonString);
            }

            TvPanel.Children.Clear();
            foreach (var tv in televisions)
            {
                TvPanel.Children.Add(new TextBlock { Text = tv.ToString(), Margin = new Thickness(5) });
            }

            UpdateSamsungCount();
        }

        private void UpdateSamsungCount()
        {
            var samsungTvs = televisions.Where(tv => tv.Brand.Equals("Samsung", StringComparison.OrdinalIgnoreCase) && tv.Diagonal >= 32).ToList();
            SamsungCount.Content = $"Samsung TV > 32\": {samsungTvs.Count}";
        }
    }
}
