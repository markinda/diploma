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
using System.IO;

namespace КурсоваяWPF
{
    /// <summary>
    /// Логика взаимодействия для Win2.xaml
    /// </summary>
   
    public partial class Win2 : Window
    {
        MarkinD_327Entities3 markin = new MarkinD_327Entities3();
        List<Count> products;
        List<ProductGrid> result = new List<ProductGrid>();
        public Win2()
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
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            int sum1 = 0;
            Count count = dtGrid1.SelectedItem as Count;
            Product product = dtGrid1.SelectedItem as Product;
            if (count != null)
            {
                result.Add(new ProductGrid { barcode = count.Product.barcode, name = count.Product.name, count = 1, idCategories = count.Product.idCategories, price = count.Product.price });
                dtGrid2.ItemsSource = result;
                dtGrid2.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Выберите продукт, чтобы продолжить!");
            }
            foreach (var i in result)
            {
                sum1 = i.price + sum1;
            }
            TextSum.Text = Convert.ToString(sum1);
        }

        private void Button_GoBack(object sender, RoutedEventArgs e)
        {

            MainWindow win = new MainWindow();
            this.Hide();
            win.Show();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            int sum2 = 0;
            if (dtGrid2.SelectedIndex > -1)
            {
                MessageBoxResult Result = MessageBox.Show("Вы действительно хотите удалить товра?", "Вопрос", MessageBoxButton.YesNo);
                if (Result == MessageBoxResult.Yes)
                {
                    result.RemoveAt(dtGrid2.SelectedIndex);
                    dtGrid2.ItemsSource = result;
                    dtGrid2.Items.Refresh();
                    MessageBox.Show("Продукт удален!");
                    foreach (var i in result)
                    {
                        sum2 = i.price + sum2;
                    }
                    TextSum.Text = Convert.ToString(sum2);
                    
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
        private void Button_Last(object sender, RoutedEventArgs e)
        {
            string path1 = @"C:\";
            string subpath = @"store";
            DirectoryInfo dirInfo = new DirectoryInfo(path1);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
            string path = @"C:\store\product.txt";
            string barcode = "";
            string name = "";
            int count = 0;
            int price = 0;
            using (FileStream fs = File.Create(path))
            {
                foreach (var i in result)
                {
                    barcode = i.barcode;
                    name = i.name;
                    count = +i.count;
                    price = +i.price;
                    byte[] info = new UTF8Encoding(true).GetBytes("Код товара = " + barcode + " Наименование: " + name + " Кол-во: " + count + " Цена: " + price + "\n");
                    fs.Write(info, 0, info.Length);
                    Count count1 = dtGrid1.SelectedItem as Count;
                    if (count1.idProduct == i.barcode)
                    {
                        count1.count1 = count1.count1 -1;
                    }
                }
                markin.SaveChanges();
                dtGrid1.Items.Refresh();
                result.Clear();
                dtGrid2.ItemsSource = result;
                dtGrid2.Items.Refresh();
                fs.Close();
            }
            StreamReader streamReader = new StreamReader(path); 
            string str = "";
            while (!streamReader.EndOfStream) 
            {
                str += streamReader.ReadLine() + "\n"; 
            }
            MessageBox.Show("Чек: \n" + str + "\nОбщая сумма покупки = " + TextSum.Text);
            MessageBox.Show("Чек успешно сохранен!");
            TextSum.Text = "";
        }
    }
}
