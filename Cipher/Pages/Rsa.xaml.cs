using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Rsa.xaml 的交互逻辑
    /// </summary>
    public partial class Rsa : Page
    {
        private Algorithm.RSA rsa;

        public Rsa()
        {
            InitializeComponent();
            encrypt_R_button.IsChecked = true;
            recreate_rsa();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.MainPage();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Close();
        }

        private void minimize_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).WindowState = WindowState.Minimized;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ((Window)this.Parent).DragMove();
            }
        }

        private void recreate_rsa()
        {
            rsa = new Algorithm.RSA();
            rsa_p.Text = "P：" + rsa.P.ToString();
            rsa_q.Text = "Q：" + rsa.Q.ToString();
            rsa_e.Text = "E：" + rsa.E.ToString();
            rsa_d.Text = "D：" + rsa.D.ToString();
            rsa_n.Text = "N：" + rsa.N.ToString();
        }

        private void recreate_rsa_n_Button_Click(object sender, RoutedEventArgs e)
        {
            recreate_rsa();
        }

        private void crypt_button_Click(object sender, RoutedEventArgs e)
        {
            if (encrypt_R_button.IsChecked == true)
            {
                string input = input_in.Text;
                BigInteger[] bi = rsa.Encrypt(input);
                input_out.Text = "";
                foreach (BigInteger item in bi)
                {
                    input_out.Text += item.ToString();
                    input_out.Text += " ";
                }
                //去除末尾的空格
                input_out.Text = input_out.Text.Substring(0, input_out.Text.Length - 1);
            }
            else
            {
                string input = input_in.Text;
                string[] split_bi = input.Split(' ');
                BigInteger[] bi = new BigInteger[split_bi.Length];
                for (int i = 0; i < split_bi.Length; ++i)
                {
                    bi[i] = BigInteger.Parse(split_bi[i]);
                }
                char[] o = rsa.Decrypt(bi);
                input_out.Text = "";
                foreach (char item in o)
                { 
                    input_out.Text += item.ToString();
                }
            }
        }
    }
}
