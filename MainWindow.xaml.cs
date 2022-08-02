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

namespace homework_12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void Start(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(user.Text))
            {
                user.Background = Brushes.Red;
                user.ToolTip = "Выберите пользователя";
            }
            else
            {
                user.Background = Brushes.LightGreen;
                user.ToolTip = null;
                if (password.Password=="123" && user.Text=="Консультант" || password.Password == "321" && user.Text == "Менеджер")
                {
                    WorkSpace workSpace = new WorkSpace(user.Text);
                    workSpace.Show();
                    Close();
                }
                else
                {
                    password.Background = Brushes.Red;
                    password.ToolTip = "Неверная связка логина и пароля";
                    MessageBox.Show("Неверная связка логина и пароля");
                }
            }
        }
    }
}
