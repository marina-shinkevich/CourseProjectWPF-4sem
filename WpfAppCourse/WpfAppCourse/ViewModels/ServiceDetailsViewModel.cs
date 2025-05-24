using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;

using System.Windows.Media.Imaging;
using WpfAppCourse.Views;


namespace WpfAppCourse.ViewModels
{
    public class ServiceDetailsViewModel : ViewModel
    {
        private ResourceDictionary dictru;
        private ResourceDictionary dicten;
        private bool isDarkTheme;
        private MainServices _selectedService;

        public MainServices SelectedService
        {
            get => _selectedService;
            set { _selectedService = value; OnPropertyChanged(); }
        }
        public ICommand BookAppointmentCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ICommand ThemeSwitcherCommand { get; }

        // Свойства
        public string ThemeIconPath => isDarkTheme
            ? "pack://application:,,,/Resources/Images/луна.png"
            : "pack://application:,,,/Resources/Images/солнце.png";

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    OnPropertyChanged();
                    ChangeLanguage(selectedLanguage);
                }
            }
        }
        public ServiceDetailsViewModel(MainServices selectedService)
        {
            _selectedService = selectedService;
          
            BookAppointmentCommand = new RelayCommand<MainServices>(ExecuteBookAppointment);
            ChangeLanguageCommand = new RelayCommand<string>((lang) => ChangeLanguage(lang));  // Изменение языка
            ThemeSwitcherCommand = new RelayCommand(ToggleTheme);  // Переключение темы

            // Загрузка ресурсов
            dictru = new ResourceDictionary();
            dicten = new ResourceDictionary();
            dictru.Source = new Uri("pack://application:,,,/Resources/Resources.ru.xaml", UriKind.RelativeOrAbsolute);
            dicten.Source = new Uri("pack://application:,,,/Resources/Resources.en.xaml", UriKind.RelativeOrAbsolute);

            // Установка языка и темы
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Язык
            var savedLang = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(savedLang))
            {
                savedLang = "ru"; // Язык по умолчанию
                Properties.Settings.Default.Language = savedLang;
                Properties.Settings.Default.Save();
            }

            SelectedLanguage = savedLang; 


            // Тема
            string savedTheme = Properties.Settings.Default.Theme;
            if (string.IsNullOrEmpty(savedTheme))
            {
                savedTheme = "Dark";
                Properties.Settings.Default.Theme = savedTheme;
                Properties.Settings.Default.Save();
            }

            isDarkTheme = savedTheme == "Dark";
        }


        private void ChangeLanguage(string lang)
        {
            if (lang == "ru")
            {
                Application.Current.Resources.MergedDictionaries.Remove(dicten);
                Application.Current.Resources.MergedDictionaries.Add(dictru);
                Application.Current.Resources["Language"] = "ru";
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(dictru);
                Application.Current.Resources.MergedDictionaries.Add(dicten);
                Application.Current.Resources["Language"] = "en";
            }

            Properties.Settings.Default.Language = lang;
            Properties.Settings.Default.Save();
        }

        private void ToggleTheme()
        {
            isDarkTheme = !isDarkTheme;
            string theme = isDarkTheme ? "Dark" : "Light";
            Properties.Settings.Default.Theme = theme;
            Properties.Settings.Default.Save();

            var themePath = isDarkTheme ? "DarkTheme.xaml" : "LightTheme.xaml";
            ChangeTheme(themePath);

            // Уведомляем об изменении иконки темы
            OnPropertyChanged(nameof(ThemeIconPath));
        }


        private void ChangeTheme(string themePath)
        {
            var newDict = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/Resources/Themes/{themePath}", UriKind.Absolute)
            };

            // Удаляем старую тему, если она подключена
            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("/Resources/Themes/"));

            if (oldDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);

            // Добавляем новую тему
            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }
        private void ExecuteBookAppointment(MainServices service)
        {
            var window = new AppointmentWindow(service)
            {
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
        }
      

    }
}
