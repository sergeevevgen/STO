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
using STOContracts.ViewModels;
using STOContracts.BusinessLogicsContracts;
using Unity;

namespace STOView
{
    /// <summary>
    /// Логика взаимодействия для WorksWindow.xaml
    /// </summary>
    public partial class WorksWindow : Window
    {
        public WorksWindow()
        {
            InitializeComponent();
        }

       /* private readonly IRoomLogic _roomLogic;
        private readonly ILunchLogic _lunchLogic;
        private readonly IRoomerLogic _roomerLogic;
        SparePartViewModel _room;
        public int Id { set { id = value; } }
        private int? id;
       */
       /* public RoomWindow(IRoomLogic roomLogic, ILunchLogic lunchLogic, IRoomerLogic roomerLogic)
        {
            InitializeComponent();
            _roomLogic = roomLogic;
            _lunchLogic = lunchLogic;
            _roomerLogic = roomerLogic;
        }*/

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*var rooms = _roomLogic.Read(new RoomBindingModel
            {
                HeadwaiterId = (int)App.Headwaiter.Id
            });
            foreach (var room in rooms)
            {
                if (room.Id == id)
                {
                    _room = room;
                }
            }

            var lunches = _lunchLogic.Read(new LunchBindingModel
            {
                HeadwaiterId = (int)App.Headwaiter.Id
            });
            foreach (var lunch in lunches)
            {
                ListBoxAvaliableLunches.Items.Add(lunch);
            }

            if (_room != null)
            {
                ListBoxAvaliableLunches.Items.Clear();
                List<int> array = new List<int>();
                var listSelectedLunches = _room.RoomLunches.ToList();

                foreach (var lunch in listSelectedLunches)
                {
                    LunchViewModel current = _lunchLogic.Read(new LunchBindingModel
                    {
                        Id = lunch.Key
                    })[0];
                    ListBoxSelectedLunches.Items.Add(current);
                    array.Add(current.Id);
                }

                foreach (var lunch in lunches)
                {
                    if (!array.Contains(lunch.Id)) ListBoxAvaliableLunches.Items.Add(lunch);
                }

                TextBoxNumber.Text = _room.Number;
                TextBoxLevel.Text = _room.Level.ToString();
            }*/
        }

        private void ButtonAddLunch_Click(object sender, RoutedEventArgs e)
        {
           /* if (ListBoxAvaliableLunches.SelectedItem != null)
            {
                ListBoxSelectedLunches.Items.Add(ListBoxAvaliableLunches.SelectedItem);
                ListBoxAvaliableLunches.Items.Remove(ListBoxAvaliableLunches.SelectedItem);
            }*/
        }

        private void ButtonRemoveLunch_Click(object sender, RoutedEventArgs e)
        {
           /* if (ListBoxSelectedLunches.SelectedItem != null)
            {
                ListBoxAvaliableLunches.Items.Add(ListBoxSelectedLunches.SelectedItem);
                ListBoxSelectedLunches.Items.Remove(ListBoxSelectedLunches.SelectedItem);
            }*/
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
           /* if (string.IsNullOrEmpty(TextBoxNumber.Text))
            {
                MessageBox.Show("Введите номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLevel.Text))
            {
                MessageBox.Show("Введите этаж", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dictionary<int, string> roomLunches = new Dictionary<int, string>();
            foreach (LunchViewModel lunch in ListBoxSelectedLunches.Items)
            {
                roomLunches.Add(lunch.Id, lunch.Name);
            }

            Dictionary<int, string> roomers = new Dictionary<int, string>();
            var roomersAll = _roomerLogic.Read(new RoomerBindingModel
            {
                HeadwaiterId = (int)App.Headwaiter.Id
            });
            foreach (var roomer in roomersAll)
            {
                if (roomer.RoomId == id)
                {
                    roomers.Add(roomer.Id, roomer.FullName);
                }
            }

            try
            {
                _roomLogic.CreateOrUpdate(new RoomBindingModel
                {
                    Id = id,
                    Number = TextBoxNumber.Text,
                    Level = Convert.ToInt32(TextBoxLevel.Text),
                    HeadwaiterId = (int)App.Headwaiter.Id,
                    Roomers = roomers,
                    RoomLunches = roomLunches
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

