using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cipher.Pages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void caesar_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.Caesar();
        }

        private void hill_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.Hill();
        }

        private void playfair_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.Playfair();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ((Window)this.Parent).DragMove();
            }
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Close();
        }

        private void minimize_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).WindowState = WindowState.Minimized;
        }

        private void rsa_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.Rsa();
        }
    }
}
