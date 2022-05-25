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
using STOBusinessLogic.BusinessLogics;
using STOContracts.ViewModels;
using STOContracts.BusinessLogicsContracts;
using Unity;

namespace STOViewTest
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IEmployeeLogic _logic;
        public LoginWindow(IEmployeeLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<RegistrationWindow>();
            form.ShowDialog();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var employees = _logic.Read(null);
            EmployeeViewModel _employee = null;

            foreach (var emp in employees)
            {
                if (emp.Login == TextBoxLogin.Text && emp.Password == TextBoxPassword.Text)
                {
                    _employee = emp;
                }
            }

            if (_employee != null)
            {
                App.employee = _employee;
                var form = App.Container.Resolve<MainWindow>();
                Close();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
