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
    /// Логика взаимодействия для SparePartWindow.xaml
    /// </summary>
    public partial class SparePartWindow : Window
    {
        private readonly ISparePartLogic _logic;
        public int Id { set { id = value; } }
        private int? id;

        public SparePartWindow(ISparePartLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxFactoryNum.Text))
            {
                MessageBox.Show("Введите заводской номер запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Введите цену запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new SparePartBindingModel
                {
                    Id = id,
                    SparePartName = TextBoxName.Text,
                    FactoryNumber = TextBoxFactoryNum.Text,
                    Price = Convert.ToDecimal(TextBoxPrice.Text)
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
                    var view = _logic.Read(new SparePartBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.SparePartName;
                        TextBoxFactoryNum.Text = view.FactoryNumber;
                        view.Price = Convert.ToDecimal(TextBoxPrice.Text);
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
