using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminSpecialists.xaml
    /// </summary>
    public partial class AdminSpecialists : Page
    {
        

        public AdminSpecialists()
        {
            InitializeComponent();
            DataContext = new AdminSpecialistsViewModel();

        }

        private void ServiceBut_Click(object sender, MouseButtonEventArgs e)
        {

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminSrv();
        }

        private void AdmUsers_Click(object sender, MouseButtonEventArgs e)
        {

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminUsers();
        }

        private void Reserv_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminReservs();
        }

        private void Review_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminReviews();
        }
    }
}
