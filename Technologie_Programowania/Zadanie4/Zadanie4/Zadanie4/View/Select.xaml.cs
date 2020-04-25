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
using System.Windows.Shapes;
using Zadanie4.ViewModel;

namespace Zadanie4
{
    /// <summary>
    /// Interaction logic for Select.xaml
    /// </summary>
    public partial class Select : Window
    {
        Zadanie4ViewModel viewModel = new Zadanie4ViewModel();
        public Select()
        {
            InitializeComponent();

            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectReviewClick(object sender, RoutedEventArgs e)
        {
            ProductsGrid.Visibility = System.Windows.Visibility.Hidden;
            ReviewsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void SelectProductsClick(object sender, RoutedEventArgs e)
        {
            ProductsGrid.Visibility = System.Windows.Visibility.Visible;
            ReviewsGrid.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
