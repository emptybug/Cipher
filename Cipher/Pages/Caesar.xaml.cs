using Cipher.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cipher.Pages
{
    /// <summary>
    /// Caesar.xaml 的交互逻辑
    /// </summary>
    public partial class Caesar : Page
    {
        public Caesar()
        {
            InitializeComponent();
        }

        private void transform()
        {
            if (encrypt_R_button.IsChecked == true)
            {
                input_out.Text = Algorithm.Caesar.Encrypt(input_in.Text, (int)key_slider.Value);
            }
            else
            {
                input_out.Text = Algorithm.Caesar.Decrypt(input_in.Text, (int)key_slider.Value);
            }
        }

        private void input_in_TextChanged(object sender, TextChangedEventArgs e)
        {
            transform();
        }

        private void key_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            key_input.Text = "key: " + ((int)((Slider)sender).Value).ToString();
            transform();
        }

        private void R_button_Checked(object sender, RoutedEventArgs e)
        {
            transform();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.MainPage();
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
    }
}
