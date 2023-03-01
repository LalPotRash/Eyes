using System;
using Eyes.DB;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace Eyes
{
    /// <summary>
    /// Interaction logic for AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public Agent Agent { get; set; }
        public List<AgentType> AgentTypes { get; set; }
        public List<Product> Products { get; set; }

        public bool Temp;
        public AgentPage(Agent agent, bool temp)
        {
            InitializeComponent();
            Temp = temp;
            Agent = agent;

            AgentTypes = DBConnection.connection.AgentType.ToList();
            Products = DBConnection.connection.Product.ToList();

            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Temp == true)
                {
                    DBConnection.connection.Agent.Add(Agent);
                }
                
                DBConnection.connection.SaveChanges();
                NavigationService.Navigate(new AgentsList());
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить изменения");
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (fileDialog.ShowDialog().Value)
            {
                var photo = File.ReadAllBytes(fileDialog.FileName);
                if (photo.Length > 1024 * 150)
                {
                    MessageBox.Show("Размер фотографии не должен превышать 150 КБ", "Ошибка");
                    return;
                }
                AgentLogo.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void ManForProductionTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Удалить данного агента?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DBConnection.connection.Agent.Remove(Agent);
                    DBConnection.connection.SaveChanges();

                    NavigationService.Navigate(new AgentsList());
                }
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить изменения", "Данные заполнены некорректно");
            }
        }
    }
}
