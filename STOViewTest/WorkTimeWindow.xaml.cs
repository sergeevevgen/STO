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
using STOContracts.ViewModels;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using Unity;

namespace STOView
{
    /// <summary>
    /// Логика взаимодействия для WorkTimeWindow.xaml
    /// </summary>
    public partial class WorkTimeWindow : Window
    {
        private readonly ITimeOfWorkLogic _logic;
        public int Id { set { id = value; } }
        private int? id;

        public WorkTimeWindow(ITimeOfWorkLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxHours.Text))
            {
                MessageBox.Show("Введите время выполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new TimeOfWorkBindingModel
                {
                    Id = id,
                    Hours = Convert.ToInt32(TextBoxHours.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = _logic.Read(new TimeOfWorkBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        view.Hours = Convert.ToInt32(TextBoxHours.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }

            }
        }

    }
}
