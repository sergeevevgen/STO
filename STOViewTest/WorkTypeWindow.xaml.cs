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
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.ViewModels;
using Unity;

namespace STOView
{
    /// <summary>
    /// Логика взаимодействия для WorkTypeWindow.xaml
    /// </summary>
    public partial class WorkTypeWindow : Window
    {
        private readonly IWorkLogic _logic;
        private readonly ISparePartLogic _sparePartLogic;
        public int Id { set { id = value; } }
        private int? id;

        public WorkTypeWindow(IWorkLogic logic, ISparePartLogic sparePartLogic)
        {
            InitializeComponent();
            _logic = logic;
            _sparePartLogic = sparePartLogic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название типа работ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new WorkTypeBindingModel
                {
                    Id = id,
                    WorkName = TextBoxName.Text,
                    WorkSpareParts = new Dictionary<int, (string, decimal)>()
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

        }
    }
}
