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

namespace КурсоваяWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MarkinD_327Entities3 markin = new MarkinD_327Entities3();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Userss userspas = markin.Userss.SingleOrDefault(x => x.password == Password.Text && x.login == login.Text);
                if (userspas.password == Password.Text && userspas.login == login.Text && userspas.idRolles == 1)
                {
                    MessageBox.Show("Вы вошли как: " + userspas.Rolles.name);
                    Win4 win4 = new Win4();
                    this.Hide();
                    win4.Show();
                }
                else if(userspas.password == Password.Text && userspas.login == login.Text && userspas.idRolles == 2)
                {
                    MessageBox.Show("Вы вошли как: " + userspas.Rolles.name);
                    Win2 win2 = new Win2();
                    this.Hide();
                    win2.Show();
                }
            }
            catch
            {
                MessageBox.Show("Пользователя с таким login: " + login.Text + " и password: " + Password.Text + ". Возможны ошибки в пароле");
            }
        }
    }
}
