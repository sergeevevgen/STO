using STOView;
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
using Unity;

namespace STOViewTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuItemWorks_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WorkWindow>();
            form.ShowDialog();
        }

        private void MenuItemSparePartsAdd_Click(object sender, RoutedEventArgs e)
        {
           /* var form = App.Container.Resolve<LinkLunchSeminarWindow>();
            form.ShowDialog();*/
        }

        private void MenuItemSpareParts_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<SparePartWindow>();
            form.ShowDialog();
        }

        private void MenuItemTimeOfWorks_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WorkTimeWindow>();
            form.ShowDialog();
        }

        private void MenuAddWorkType_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WorkTypeWindow>();
            form.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
           /* var form = App.Container.Resolve<ReportLunchWindow>();
            form.ShowDialog();*/
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            App.storekeeper = null;
            var windowSignIn = App.Container.Resolve<LoginWindow>();
            Close();
            windowSignIn.ShowDialog();
        }
    }
}
