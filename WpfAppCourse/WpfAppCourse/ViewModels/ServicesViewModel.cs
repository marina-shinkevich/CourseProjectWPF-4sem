using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using WpfAppCourse.Views;

namespace WpfAppCourse.ViewModels
{
    public class ServicesViewModel : ViewModel
    {
        public ObservableCollection<MainServices> ServicesList { get; set; }
        private ResourceDictionary dictru;
        private ResourceDictionary dicten;
        private bool isDarkTheme;
    
       
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ExecuteSearch();
            }
        }

        private bool _isSearchFocused;
        public bool IsSearchFocused
        {
            get => _isSearchFocused;
            set
            {
                _isSearchFocused = value;
                OnPropertyChanged();
                
            }
        }
        public bool ShowWatermark => !IsSearchFocused && string.IsNullOrEmpty(SearchText);
        public string WatermarkText => "Введите название процедуры...";
        public ICommand SearchCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand ResetFilterCommand { get; }
       
        public ICommand BookAppointmentCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ICommand ThemeSwitcherCommand { get; }
        public ICommand SearchGotFocusCommand { get; }
        public ICommand SearchLostFocusCommand { get; }
        // Свойства
        public string ThemeIconPath => isDarkTheme
            ? "pack://application:,,,/Resources/Images/луна.png"
            : "pack://application:,,,/Resources/Images/солнце.png";


        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>
{
    "Все",
    "Уход",
    "Инъекции",
    "Массаж",
    "Аппаратные"
};

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
               
            }
        }
        private double _maxPrice = 250;
        public double MaxPrice
        {
            get => _maxPrice;
            set
            {
                if (_maxPrice != value)
                {
                    _maxPrice = value;
                    OnPropertyChanged();
                   
                }
            }
        }

        private int _maxDuration;
        public int MaxDuration
        {
            get => _maxDuration;
            set
            {
                _maxDuration = value;
                OnPropertyChanged();
            }
        }

       

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
        

        private readonly ResourceDictionary Dictru;
        private readonly ResourceDictionary Dicten;

        public ServicesViewModel()
        {
           
            ChangeLanguageCommand = new RelayCommand<string>((lang) => ChangeLanguage(lang));  // Изменение языка
            ThemeSwitcherCommand = new RelayCommand(ToggleTheme);  // Переключение темы

            // Загрузка ресурсов
            dictru = new ResourceDictionary();
            dicten = new ResourceDictionary();
            dictru.Source = new Uri("pack://application:,,,/Resources/Resources.ru.xaml", UriKind.RelativeOrAbsolute);
            dicten.Source = new Uri("pack://application:,,,/Resources/Resources.en.xaml", UriKind.RelativeOrAbsolute);
            SearchGotFocusCommand = new RelayCommand(() => IsSearchFocused = true);
            SearchLostFocusCommand = new RelayCommand(() => IsSearchFocused = false);
            LoadSettings();

            LoadServices();
            
            SelectedCategory = "Все";
            MaxDuration = 60;
            MaxPrice = 250;
             SearchCommand = new RelayCommand(ExecuteSearch);
            FilterCommand = new RelayCommand(ExecuteFilter);
            ResetFilterCommand = new RelayCommand(ExecuteReset);
           
            ChangeLanguageCommand = new RelayCommand<string>(ExecuteLanguageChange);
            BookAppointmentCommand = new RelayCommand<MainServices>(ExecuteBookAppointment);

        }
        private void LoadServices()
        {
            using (var db = new AppContext())
            {
                ServicesList = new ObservableCollection<MainServices>(db.MainServices.ToList());
            }
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



        private void ExecuteSearch()
        {
            using (var db = new AppContext())
            {
                var filtered = string.IsNullOrWhiteSpace(SearchText)
                    ? db.MainServices.ToList() // Показываем все процедуры, если строка пуста
                    : db.MainServices
                          .Where(s => s.name_of_service.ToLower().Contains(SearchText.ToLower()))
                          .ToList();

                ServicesList = new ObservableCollection<MainServices>(filtered);
                OnPropertyChanged(nameof(ServicesList));
            }
        }


        private void ExecuteFilter()
        {
            using (var db = new AppContext())
            {
                var query = db.MainServices.AsQueryable();

                if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "Все")
                {
                    query = query.Where(p => p.category != null &&
                           p.category.Trim().Equals(SelectedCategory.Trim(), StringComparison.OrdinalIgnoreCase));
                }

                // Добавьте проверку, что MaxPrice и MaxDuration имеют допустимые значения
                if (MaxPrice > 0)
                {
                    query = query.Where(p => p.price <= MaxPrice);
                }

                if (MaxDuration > 0)
                {
                    query = query.Where(p => p.durationminuts <= MaxDuration);
                }

                ServicesList = new ObservableCollection<MainServices>(query.ToList());
                OnPropertyChanged(nameof(ServicesList));
            }
        }
        private void ExecuteReset()
        {
            // Сброс фильтров в дефолтные значения
            SelectedCategory = "Все";
            MaxPrice = 250;
            MaxDuration = 60;
            SearchText = string.Empty;

            // Уведомление UI о сбросе значений
            OnPropertyChanged(nameof(SelectedCategory));
            OnPropertyChanged(nameof(MaxPrice));
            OnPropertyChanged(nameof(MaxDuration));
            OnPropertyChanged(nameof(SearchText));

            using (var db = new AppContext())
            {
                ServicesList = new ObservableCollection<MainServices>(db.MainServices.ToList());
            }

            // Уведомляем UI о том, что список процедур обновился
            OnPropertyChanged(nameof(ServicesList));
        }



        


        private void ExecuteLanguageChange(string lang)
        {
            if (lang == "ru")
            {
                Application.Current.Resources.MergedDictionaries.Remove(Dicten);
                Application.Current.Resources.MergedDictionaries.Add(Dictru);
                Application.Current.Resources["Language"] = "ru";
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(Dictru);
                Application.Current.Resources.MergedDictionaries.Add(Dicten);
                Application.Current.Resources["Language"] = "en";
            }
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
