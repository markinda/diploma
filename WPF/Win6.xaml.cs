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
    /// Логика взаимодействия для Win6.xaml
    /// </summary>
    public partial class Win6 : Window
    {
        MarkinD_327Entities3 markin = new MarkinD_327Entities3();
        public Win6()
        {
            InitializeComponent();
            List<Categories> categories = markin.Categories.ToList();
            cmbCategories.ItemsSource = categories;
            textcount.MaxLength = 2;

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_GoBack(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
            if ((s < '0' || s > '9') && (s < 'а' || s > 'я') && (s < 'А' || s > 'Я'))
                e.Handled = true;
        }
    }
}
