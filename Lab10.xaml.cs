using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;

namespace Lab10_11
{
    /// <summary>
    /// Логика взаимодействия для Lab10.xaml
    /// </summary>
    public partial class Lab10 : Page
    {
        private string fileName;
        public Lab10()
        {
            InitializeComponent();
            fileName = string.Empty;
        }
        private void Create()
        {
            tbEdit.Text = "";
        }
        private async void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files(*.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(ofd.FileName))
                {
                    tbEdit.Text = await reader.ReadToEndAsync();
                }
                fileName = ofd.FileName;
            }
        }
        private async void Save()
        {
            if (fileName.Length != 0)
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    await writer.WriteLineAsync(tbEdit.Text);
                }
            }
            else
            {
                SaveAs();
            }
        }
        private async void SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text files(*.txt)|*.txt";
            if (sfd.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(sfd.FileName, false))
                {
                    await writer.WriteLineAsync(tbEdit.Text);
                }
                fileName = sfd.FileName;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void bold_Click(object sender, RoutedEventArgs e)
        {
            tbEdit.FontWeight = (bold.IsChecked==true)?FontWeights.Bold:
                FontWeights.Normal;
        }

        private void italic_Click(object sender, RoutedEventArgs e)
        {
            tbEdit.FontStyle = (italic.IsChecked == true) ? FontStyles.Italic :
                FontStyles.Normal;
        }

        private void underline_Click(object sender, RoutedEventArgs e)
        {
            tbEdit.TextDecorations = (underline.IsChecked==true) ? TextDecorations.Underline : null;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string? line;
                string? result="";
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Contains(filter.Text)) result += line;
                }
                tbEdit.Text = result;
            }
        }
    }
}
