using MaterialDesignThemes.Wpf;
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

namespace 高主动性的todo清单
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Image_KeyUp(object sender, KeyEventArgs e)
        {
          

        }

        private void Card_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Card_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Card_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Card card = (Card)sender;
            //扩展
            //card.
            
        }
    }
}
