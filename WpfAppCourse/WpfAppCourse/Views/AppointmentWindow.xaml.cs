using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
       

        // Конструктор для записи через страницу процедур
        public AppointmentWindow(MainServices selectedService)
            : this(selectedService.name_of_service, selectedService.ID, null)
        {
            InitializeComponent();
            DataContext = new AppointmentViewModel(selectedService);
        }

        // Конструктор для записи через страницу специалистов
        public AppointmentWindow(MainSpecialists specialist, string serviceName)
            : this(serviceName, specialist.Service, specialist.ID)
        {
            InitializeComponent();
            DataContext = new AppointmentViewModel(specialist, serviceName);
        }

        // Основной приватный конструктор
        private AppointmentWindow(string serviceName, int serviceId, int? preselectedMasterId)
        {
            InitializeComponent();
            DataContext = new ReviewsViewModel();


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
