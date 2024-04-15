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

namespace КурсоваяWPF
{
    /// <summary>
    /// Логика взаимодействия для Win4.xaml
    /// </summary>
    public partial class Win4 : Window
    {
        MarkinD_327Entities3 markin = new MarkinD_327Entities3();
        List<Count> products;
        private void Update()
        {
            dtGrid1.ItemsSource = markin.Count.ToList();
            dtGrid1.Items.Refresh();
        }
        public Win4()
        {
            InitializeComponent();
            products = markin.Count.ToList();
            dtGrid1.ItemsSource = products;
            dtGrid1.Items.Refresh();
        }
        private void TextFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Count> filter; 
            filter = markin.Count.ToList().Where(x => x.Product.barcode.Contains(TextFilter.Text)).ToList();
            dtGrid1.ItemsSource = filter;
            dtGrid1.Items.Refresh();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            Count count = dtGrid1.SelectedItem as Count;
            Product product = dtGrid1.SelectedItem as Product;
            
            Win6 win6 = new Win6();
            if (count != null)
            { 
                win6.textname.Text = count.Product.name;
                win6.cmbCategories.SelectedIndex = count.Product.idCategories -1;
                win6.textprice.Text = Convert.ToString(count.Product.price);
                win6.textcount.Text = Convert.ToString(count.count1);
                win6.textbarcoder.Text = "Изменение запрещено!";
                win6.textbarcoder.Foreground = Brushes.Silver;

                win6.ShowDialog();

                MessageBoxResult Result = MessageBox.Show("Вы действительно хотите изменить товар товра?", "Вопрос", MessageBoxButton.YesNo);
                if (Result == MessageBoxResult.Yes)
                {
                    count.Product.name = win6.textname.Text;
                    count.Product.idCategories = win6.cmbCategories.SelectedIndex + 1;
                    count.Product.price = Convert.ToInt32(win6.textprice.Text);
                    count.count1 = Convert.ToInt32(win6.textcount.Text);
                    markin.SaveChanges();
                    dtGrid1.Items.Refresh();
                    MessageBox.Show("Продукт изменен!");
                }
                else if (Result == MessageBoxResult.No)
                {
                    MessageBox.Show("Продукт не изменен!");
                }
                this.Show();
            }
            else
            {
                MessageBox.Show("Выберите продукт, чтобы продолжить!");
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Win5 win5 = new Win5();
            this.Hide();
            win5.Show();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {

            Count item = dtGrid1.SelectedItem as Count;
            if (item != null)
            {
                Product product = item.Product;
                if (product != null)
                {
                    MessageBoxResult Result = MessageBox.Show("Вы действительно хотите удалить товра?", "Вопрос", MessageBoxButton.YesNo);
                    if (Result == MessageBoxResult.Yes)
                    {
                        markin.Count.Remove(item);
                        markin.Product.Remove(product);
                        markin.SaveChanges();
                        Update();
                        MessageBox.Show("Продукт удален!");
                    }
                    else if (Result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Продукт не удален!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите продукт, чтобы удалить!");
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт, чтобы удалить!");
            }
        }

        private void Button_GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            this.Hide();
            win.Show();
        }
    }
}
