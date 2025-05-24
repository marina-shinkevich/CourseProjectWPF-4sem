using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Policy;
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
    public partial class AdminServices : Page
    {
        public AdminServices()
        {
            InitializeComponent();
            DataContext = new AdminServicesViewModel();
        }

        private void Masters_Click(object sender, MouseButtonEventArgs e)
        {

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminSpecialists(); 

        }

        private void Users_Click(object sender, MouseButtonEventArgs e)
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
