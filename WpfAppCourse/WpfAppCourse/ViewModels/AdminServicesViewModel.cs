using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;


namespace WpfAppCourse.ViewModels
{
    
        public class AdminServicesViewModel : ViewModel
        {
            private ObservableCollection<MainServices> _services;
            public ObservableCollection<MainServices> Services
            {
                get => _services;
                set { _services = value; OnPropertyChanged(); }
            }
        private MainServices _selectedService;
        public MainServices SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged();

                // Обновляем доступность команды
                (DeleteServiceCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }


        public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public string Duration { get; set; }
            public string Category { get; set; }
            public string ImagePath { get; set; }

            public ICommand AddServiceCommand { get; }
            public ICommand DeleteServiceCommand { get; }
            public ICommand SaveChangesCommand { get; }
            public ICommand SelectImageCommand { get; }

            public AdminServicesViewModel()
            {
                LoadServices();
                AddServiceCommand = new RelayCommand(AddService);
            DeleteServiceCommand = new RelayCommand(
     () => DeleteService(SelectedService),
     () => SelectedService != null
 );

            SaveChangesCommand = new RelayCommand(SaveChanges);
                SelectImageCommand = new RelayCommand(SelectImage);
            }

            private void LoadServices()
            {
                using (var db = new AppContext())
                    Services = new ObservableCollection<MainServices>(db.MainServices.ToList());
            }

        private void AddService()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Price) || string.IsNullOrWhiteSpace(Duration) ||
                string.IsNullOrWhiteSpace(Category) || string.IsNullOrWhiteSpace(ImagePath))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Валидация категории
            string[] allowedCategories = { "Уход", "Инъекции", "Массаж", "Аппаратные" };
            if (!allowedCategories.Contains(Category.Trim(), StringComparer.OrdinalIgnoreCase))
            {
                MessageBox.Show("Категория должна быть одной из следующих: Уход, Инъекции, Массаж, Аппаратные.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(Price, out decimal priceVal) || !int.TryParse(Duration, out int durationVal))
            {
                MessageBox.Show("Некорректные данные для цены или длительности.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppContext())
            {
                int nextId = db.MainServices.OrderByDescending(s => s.ID).FirstOrDefault()?.ID + 1 ?? 1;

                var newService = new MainServices
                {
                    ID = nextId,
                    name_of_service = Name,
                    description = Description,
                    price = (int)priceVal,
                    durationminuts = durationVal,
                    category = Category,
                    photo = ImagePath
                };

                db.MainServices.Add(newService);
                db.SaveChanges();
                Services.Add(newService);
            }

            MessageBox.Show("Процедура добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Name = Description = Price = Duration = Category = ImagePath = string.Empty;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(Duration));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(ImagePath));
        }


        private void DeleteService(MainServices service)
        {
            try
            {
                if (service == null)
                {
                    MessageBox.Show("Пожалуйста, выберите запись для удаления.",
                                  "Удаление",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                using (var db = new AppContext())
                {
                    // Проверка на наличие записей
                    bool hasReservations = db.Reserv.Any(r => r.Service_Id == service.ID);

                    if (hasReservations)
                    {
                        MessageBox.Show("Невозможно удалить процедуру, так как на неё есть активные записи.",
                                        "Ошибка",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        return;
                    }

                    // Проверка на наличие прикреплённых мастеров
                    bool hasSpecialists = db.MainSpecialists.Any(s => s.Service == service.ID);

                    if (hasSpecialists)
                    {
                        MessageBox.Show("Невозможно удалить процедуру, так как к ней прикреплён хотя бы один специалист.",
                                        "Ошибка",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        return;
                    }

                    var result = MessageBox.Show($"Вы уверены, что хотите удалить \"{service.name_of_service}\"?",
                                               "Подтверждение удаления",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var serviceToDelete = db.MainServices.Find(service.ID);
                        if (serviceToDelete != null)
                        {
                            db.MainServices.Remove(serviceToDelete);
                            db.SaveChanges();

                            MessageBox.Show("Процедура удалена.",
                                          "Удалено",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);

                            LoadServices();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}",
                               "Ошибка",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void SaveChanges()
        {
            var allowedCategories = new List<string> { "Уход", "Инъекции", "Массаж","Аппаратные" };

            using (var db = new AppContext())
            {
                foreach (var service in Services)
                {
                    var normalizedCategory = service.category?.Trim();

                    if (!allowedCategories.Contains(normalizedCategory))
                    {
                        MessageBox.Show(
                            $"Недопустимая категория у услуги с ID {service.ID}. Разрешены только: Уход, Инъекции, Массаж, Аппаратные.",
                            "Ошибка категории",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                    var dbService = db.MainServices.FirstOrDefault(s => s.ID == service.ID);
                    if (dbService != null)
                    {
                        dbService.name_of_service = service.name_of_service;
                        dbService.description = service.description;
                        dbService.price = service.price;
                        dbService.durationminuts = service.durationminuts;
                        dbService.category = service.category;
                        dbService.photo = service.photo;
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SelectImage()
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    ImagePath = openFileDialog.FileName;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }
    
}
