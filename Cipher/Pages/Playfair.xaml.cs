using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cipher.Pages
{
    /// <summary>
    /// Playfair.xaml 的交互逻辑
    /// </summary>
    public partial class Playfair : Page
    {
        public Playfair()
        {
            InitializeComponent();
            gui();
        }

        private string _key;
        private string _input_word = "";

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = new Pages.MainPage();
        }

        private void transform()
        {
            if (encrypt_R_button.IsChecked == true)
            {
                input_out.Text = Algorithm.Playfair.Encrypt(input_in.Text);
            }
            else
            {
                input_out.Text = Algorithm.Playfair.Decrypt(input_in.Text);
            }
        }

        private void gui()
        {
            char[, ] key = Algorithm.Playfair.Key(_input_word);
            key_gui_grid.Children.Clear();
            for(int i = 0; i < 5; ++i)
            {
                for(int j = 0; j < 5; ++j)
                {
                    TextBlock tb = new TextBlock();
                    tb.SetValue(Grid.RowProperty, i);
                    tb.SetValue(Grid.ColumnProperty, j);
                    tb.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    tb.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                    tb.Text = key[i, j].ToString();
                    if (key[i, j].Equals('I')) tb.Text = tb.Text + "/J";
                    tb.Foreground = new SolidColorBrush(Colors.White);
                    tb.FontSize = 24;
                    key_gui_grid.Children.Add(tb);
                }
                
            }
        }

        private void R_button_Checked(object sender, RoutedEventArgs e)
        {
            transform();
        }

        private void input_word_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rgx = new Regex(@"^[A-Za-z]+$");
            string ss = ((TextBox)sender).Text;
            if (ss.Equals("") | rgx.IsMatch(ss))
            {
                _input_word = ss;
            }
            else
            {
                input_word.Text = _input_word;
                input_word.SelectionStart = input_word.Text.Length;
            }
            gui();
            transform();
        }

        private void input_in_TextChanged(object sender, TextChangedEventArgs e)
        {
            transform();
        }

        private void input_word_KeyDown(object sender, KeyEventArgs e)
        {
            //Regex rgx = new Regex(@"^[A-Za-z]$");
            //string ss = e.Key.ToString();
            //if (!rgx.IsMatch(ss) || e.Key == Key.Space || e.Key == Key.OemQuotes || e.Key == Key.ImeConvert)
            //{
            //    e.Handled = true;
            //}
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
