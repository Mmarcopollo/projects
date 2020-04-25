using System;
using System.Collections.Generic;
using System.Linq;
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
using Zadanie4.ViewModel;

namespace Zadanie4
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Zadanie4ViewModel viewModel = new Zadanie4ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            //this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
        }
       
        private void StartAction(object sender, RoutedEventArgs e)
        {
            MenuImage.Visibility = System.Windows.Visibility.Visible;
            Insert.Visibility = System.Windows.Visibility.Visible;
            Select.Visibility = System.Windows.Visibility.Visible;
            Update.Visibility = System.Windows.Visibility.Visible;
            Delete.Visibility = System.Windows.Visibility.Visible;
        }
     

        private void InsertButton(object sender, RoutedEventArgs e)
        {
            new Insert().Show();
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            new Select().Show();
        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            new Update().Show();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            new Delete().Show();
        }
    }
}
