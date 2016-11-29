using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cipher.Pages
{
    /// <summary>
    /// Hill.xaml 的交互逻辑
    /// </summary>
    public partial class Hill : Page
    {
        private Algorithm.Hill hill = null;

        private string _input = "";

        public Hill()
        {
            InitializeComponent();
            input_level.Text = 3.ToString();
            auto_R_Button.IsChecked = true;
            encrypt_R_button.IsChecked = true;
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

        private void input_in_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input_in.Text.Length; ++i)
            {
                if ((input_in.Text[i] <= 'z'&& input_in.Text[i] >= 'a') || (input_in.Text[i] <= 'Z'&& input_in.Text[i] >= 'A'))
                {
                    sb.Append(input_in.Text[i]);
                }
            }
            _input = sb.ToString();
            transform();
        }

        private void R_button_Checked(object sender, RoutedEventArgs e)
        {
            if (auto_R_Button.IsChecked == true)
            {
                transform();
            }
            else
            {
                transform();
            }
        }

        private void recreate_matrix_Click(object sender, RoutedEventArgs e)
        {
            if (auto_R_Button.IsChecked == true)
            {
                create_matrix();
                transform();
            }
            else
            {
                int level = Convert.ToInt32(input_level.Text);
                long[][] input_matrix = Algorithm.Hill.getNewMatrix(level);
                foreach(TextBox tb in matrix_grid.Children)
                {
                    int i = (int)tb.GetValue(Grid.RowProperty);
                    int j = (int)tb.GetValue(Grid.ColumnProperty);
                    input_matrix[i][j] = (Convert.ToInt32(tb.Text));
                }
                hill = new Algorithm.Hill(level, input_matrix);
                create_inverse_matrix();
                transform();
            }
            
        }

        private void clear_matrix()
        {
            matrix_grid.Children.Clear();
            matrix_grid.RowDefinitions.Clear();
            matrix_grid.ColumnDefinitions.Clear();
            inverse_matrix_grid.Children.Clear();
            inverse_matrix_grid.RowDefinitions.Clear();
            inverse_matrix_grid.ColumnDefinitions.Clear();
        }

        /// <summary>
        /// 创建矩阵后操作GUI
        /// </summary>
        private void create_matrix()
        {
            if (auto_R_Button.IsChecked == true) hill = new Algorithm.Hill(Convert.ToInt32(input_level.Text));
            else hill = new Algorithm.Hill(Convert.ToInt32(input_level.Text), Algorithm.Hill.getNewMatrix(Convert.ToInt32(input_level.Text)));
            clear_matrix();
            for (int i = 0; i < hill.Level; ++i)
            {
                RowDefinition rd1 = new RowDefinition();
                ColumnDefinition cd1 = new ColumnDefinition();
                matrix_grid.RowDefinitions.Add(rd1);
                matrix_grid.ColumnDefinitions.Add(cd1);
                RowDefinition rd2 = new RowDefinition();
                ColumnDefinition cd2 = new ColumnDefinition();
                inverse_matrix_grid.RowDefinitions.Add(rd2);
                inverse_matrix_grid.ColumnDefinitions.Add(cd2);
            }
            for (int i = 0; i < hill.Level; ++i)
            {
                for (int j = 0; j < hill.Level; ++j)
                {
                    TextBox item_matrix = new TextBox();
                    item_matrix.SetValue(Grid.RowProperty, i);
                    item_matrix.SetValue(Grid.ColumnProperty, j);
                    item_matrix.Foreground = new SolidColorBrush(Colors.White);
                    //item_matrix.FontSize = 24;
                    item_matrix.SetValue(TextBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    item_matrix.SetValue(TextBox.VerticalAlignmentProperty, VerticalAlignment.Center);
                    item_matrix.Background = new SolidColorBrush(Colors.Transparent);

                    if (manual_R_Button.IsChecked == true)
                    {
                        times_create_matrix.Text = "请输入矩阵";
                        item_matrix.BorderThickness = new Thickness(1);
                    }
                    else
                    {
                        //  自动模式下只读
                        item_matrix.IsReadOnly = true;
                        item_matrix.Text = hill.Matrix[i][j].ToString();
                        item_matrix.BorderThickness = new Thickness(0);
                        times_create_matrix.Text = "矩阵次数：" + hill.Times;
                        create_inverse_matrix();
                    }
                    matrix_grid.Children.Add(item_matrix);
                }
            }
        }

        private void create_inverse_matrix()
        {
            for (int i = 0; i < hill.Level; ++i)
            {
                for (int j = 0; j < hill.Level; ++j)
                {
                    TextBlock item_inverse_matrix = new TextBlock();
                    item_inverse_matrix.SetValue(Grid.RowProperty, i);
                    item_inverse_matrix.SetValue(Grid.ColumnProperty, j);
                    item_inverse_matrix.Foreground = new SolidColorBrush(Colors.White);
                    //item_inverse_matrix.FontSize = 24;
                    item_inverse_matrix.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    item_inverse_matrix.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                    
                    item_inverse_matrix.Text = hill.InverseMatrix[i][j].ToString();
                    inverse_matrix_grid.Children.Add(item_inverse_matrix);
                }
            }
        }

        private void transform()
        {
            if (encrypt_R_button.IsChecked == true)
            {
                input_out.Text = hill.Encrypt(_input);
            }
            else
            {
                input_out.Text = hill.Decrypt(_input);
            }
        }

        private void input_level_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (auto_R_Button.IsChecked == true)
            {

            }
            else
            {
                create_matrix();
            }
            
        }

        private void manual_R_Button_Checked(object sender, RoutedEventArgs e)
        {
            create_matrix();
            if (auto_R_Button.IsChecked == true)
            {
                recreate_matrix_Button.Content = "重新生成";
                transform();
            }
            else
            {
                recreate_matrix_Button.Content = "生成逆矩阵";
                
            }
        }
    }
}
