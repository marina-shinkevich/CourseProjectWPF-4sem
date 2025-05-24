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
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminReviews.xaml
    /// </summary>
    public partial class AdminReviews : Page
    {
        public AdminReviews()
        {
            InitializeComponent();
            DataContext = new AdminReviewViewModel();
        }

        private void Users_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminUsers();
        }

        private void Service_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminSrv();
        }

        private void Masters_Click(object sender, MouseButtonEventArgs e)
        {

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminSpecialists();
        }

        private void Reserv_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadAdminReservs();
        }
    }
}
