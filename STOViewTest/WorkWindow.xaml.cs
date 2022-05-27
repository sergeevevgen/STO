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
using STOViewTest;
using Unity;

namespace STOView
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        private readonly IWorkLogic _logic;
        private readonly IStoreKeeperLogic _storeKeeperLogic;
        private readonly ITimeOfWorkLogic _timeOfWorkLogic;
        public int Id { set { id = value; } }
        private int? id;

        public WorkWindow(IWorkLogic logic, IStoreKeeperLogic storeKeeperLogic, ITimeOfWorkLogic timeOfWorkLogic)
        {
            InitializeComponent();
            _logic = logic;
            _storeKeeperLogic = storeKeeperLogic;
            _timeOfWorkLogic = timeOfWorkLogic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Введите цену работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateWork(new CreateWorkBindingModel
                {
                    WorkName = TextBoxName.Text,
                    WorkTypeId = Convert.ToInt32(ComboBoxTypeOfWork.Text),
                    Count = 1,
                    Price = Convert.ToDecimal(TextBoxPrice.Text),
                    NetPrice = Convert.ToDecimal(TextBoxPrice.Text) + 100,
                    StoreKeeperId = (int)App.storekeeper.Id
                }) ;
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

            var list = _logic.Read(null);
            if (list != null)
            {
                ComboBoxTypeOfWork.ItemsSource = list;
            }
        }
    }
}

