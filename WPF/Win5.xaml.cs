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
    /// Логика взаимодействия для Win5.xaml
    /// </summary>
    public partial class Win5 : Window
    {

        MarkinD_327Entities3 markin = new MarkinD_327Entities3();

        public Win5()
        {
            InitializeComponent();
            List<Categories> categories = markin.Categories.ToList();
            
            cmbCategories.ItemsSource = categories;
            textcount.MaxLength = 2;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = markin.Product.ToList();
            Count count = new Count();
            Product product = new Product();
            if (textbarcoder.Text != null && textname.Text != null && cmbCategories.SelectedItem != null  && textprice.Text != null && textcount.Text != null) {
                product.barcode = textbarcoder.Text;
                product.name = textname.Text;
                product.idCategories = cmbCategories.SelectedIndex + 1;
                
                product.price = Convert.ToInt32(textprice.Text);
                count.count1 = Convert.ToInt32(textcount.Text);
                count.idProduct = textbarcoder.Text;
                try
                {
                    Product product1 = markin.Product.SingleOrDefault(x => x.barcode == textbarcoder.Text);
                    if (product1.barcode == textbarcoder.Text)
                    {
                        MessageBox.Show("Продукт с таким штрихкодом есть!");
                    }
                }
                catch
                {
                    markin.Product.Add(product);
                    markin.Count.Add(count);
                    markin.SaveChanges();
                    MessageBox.Show("Продукт " + textname.Text + " добавлен!");
                    Win4 win4 = new Win4();
                    this.Hide();
                    win4.Show();
                    
                }
            }
            else
            {
                MessageBox.Show("Введите все данные в поля ввода и выбора!");
            }
        }
        private void Button_GoBack(object sender, RoutedEventArgs e)
        {
            Win4 win4 = new Win4();
            this.Hide();
            win4.Show();
        }
        private void Textprice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (s < '0' || s > '9')
                e.Handled = true;
        }
        private void Textcount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (s < '0' || s > '9')
                e.Handled = true;
        }

        private void Textname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if ((s < '0' || s > '9') && ( s < 'а' || s > 'я') && (s < 'А' || s > 'Я'))
                e.Handled = true;
        }

        private void Textbarcoder_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if ((s < '0' || s > '9') && (s < 'а' || s > 'я') && (s < 'А' || s > 'Я'))
                e.Handled = true;
        }
    }
}
