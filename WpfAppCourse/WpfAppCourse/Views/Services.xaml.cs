using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Page
    {
       
        public Services()
        {
            InitializeComponent();
            DataContext = new ServicesViewModel();



        }

       
    private void Home_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.LoadMainPage();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            if (image?.DataContext is MainServices serviceFromContext)
            {
                using (var db = new AppContext())
                {
                    var selectedService = db.MainServices.FirstOrDefault(s => s.ID == serviceFromContext.ID);
                    if (selectedService != null)
                    {
                        if (NavigationService != null)
                        {
                            NavigationService.Navigate(new ServiceDetails(selectedService));
                        }
                        else
                        {
                            MessageBox.Show("NavigationService is null");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти услугу в базе данных.");
                    }
                }
            }
        }
        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag != null)
            {
                string selectedLanguage = selectedItem.Tag.ToString();
                (DataContext as MainPageViewModel)?.ChangeLanguageCommand.Execute(selectedLanguage);
            }
        }


        private void ProfilImg_Click(object sender, MouseButtonEventArgs e)
        {
            
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.LoadUserProfil();
            
        }

       
        private void SpecialTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadSpecialists();
        }

        private void reviewsTextBox_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadReviews();
        }


    }
}
