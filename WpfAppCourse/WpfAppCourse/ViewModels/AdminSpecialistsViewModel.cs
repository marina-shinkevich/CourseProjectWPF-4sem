using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace WpfAppCourse.ViewModels
{
    public class AdminSpecialistsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MainSpecialists> specialists;
        private MainSpecialists selectedSpecialist;

        public ObservableCollection<MainSpecialists> Specialists
        {
            get => specialists;
            set { specialists = value; OnPropertyChanged(); }
        }

        public MainSpecialists SelectedSpecialist
        {
            get => selectedSpecialist;
            set { selectedSpecialist = value; OnPropertyChanged();

                // Обновляем доступность команды
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Experience { get; set; }
        public string Service { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand SelectImageCommand { get; }

        public AdminSpecialistsViewModel()
        {
            LoadSpecialists();

            AddCommand = new RelayCommand(AddSpecialist);
            DeleteCommand = new RelayCommand(() => DeleteSpecialist(SelectedSpecialist),
     () => SelectedSpecialist != null);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            SelectImageCommand = new RelayCommand(SelectImage);
        }

        private void LoadSpecialists()
        {
            using (var db = new AppContext())
                Specialists = new ObservableCollection<MainSpecialists>(db.MainSpecialists.ToList());
        }

        private void AddSpecialist()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(Photo) ||
                string.IsNullOrWhiteSpace(Experience) || string.IsNullOrWhiteSpace(Service))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(Experience, out int exp) || !int.TryParse(Service, out int serv))
            {
                MessageBox.Show("Опыт и код процедуры должны быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppContext())
            {
                // Проверка существования услуги с указанным ID
                var serviceExists = db.MainServices.Any(s => s.ID == serv);
                if (!serviceExists)
                {
                    MessageBox.Show("Процедуры с указанным ID не существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newSpecialist = new MainSpecialists
                {
                    ID = (db.MainSpecialists.OrderByDescending(s => s.ID).FirstOrDefault()?.ID ?? 0) + 1,
                    Name = Name,
                    Surname = Surname,
                    Description = Description,
                    Photo = Photo,
                    Experience = exp,
                    Service = serv
                };

                db.MainSpecialists.Add(newSpecialist);
                db.SaveChanges();
            }

            MessageBox.Show("Специалист добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            ClearFields();
            LoadSpecialists();
        }


        private void DeleteSpecialist(MainSpecialists specialist)
        {
            if (specialist == null) return;

            var result = MessageBox.Show($"Удалить \"{specialist.Name}\"?", "Подтверждение", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;

            using (var db = new AppContext())
            {
                // Проверка, есть ли записи в Reserv с этим специалистом
                bool hasAppointments = db.Reserv.Any(r => r.Master_Id == specialist.ID); 

                if (hasAppointments)
                {
                    MessageBox.Show("Невозможно удалить специалиста, так как у него есть активные записи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var specialistToDelete = db.MainSpecialists.FirstOrDefault(s => s.ID == specialist.ID);
                if (specialistToDelete != null)
                {
                    db.MainSpecialists.Remove(specialistToDelete);
                    db.SaveChanges();
                }
            }

            LoadSpecialists();
        }


        private void SaveChanges()
        {
            using (var db = new AppContext())
            {
                foreach (var specialist in Specialists)
                {
                    var dbSpec = db.MainSpecialists.FirstOrDefault(s => s.ID == specialist.ID);
                    if (dbSpec != null)
                    {
                        // Валидация имени
                        if (string.IsNullOrWhiteSpace(specialist.Name) || !Regex.IsMatch(specialist.Name, @"^[A-Za-zА-Яа-яЁё]+$"))
                        {
                            MessageBox.Show($"Неверное имя у специалиста ID {specialist.ID}. Имя должно содержать только буквы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Валидация фамилии
                        if (string.IsNullOrWhiteSpace(specialist.Surname) || !Regex.IsMatch(specialist.Surname, @"^[A-Za-zА-Яа-яЁё]+$"))
                        {
                            MessageBox.Show($"Неверная фамилия у специалиста ID {specialist.ID}. Фамилия должна содержать только буквы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Валидация опыта — проверяем, что >= 0 (или другое логическое условие)
                        if (specialist.Experience < 0)
                        {
                            MessageBox.Show($"Неверное значение опыта у специалиста ID {specialist.ID}. Опыт должен быть неотрицательным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }


                        // Валидация процедуры (Service) - должна существовать в базе
                        if (specialist.Service == null || !db.MainServices.Any(s => s.ID == specialist.Service))
                        {
                            MessageBox.Show($"Процедура у специалиста ID {specialist.ID} не существует в базе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Если все проверки прошли - обновляем поля
                        dbSpec.Name = specialist.Name;
                        dbSpec.Surname = specialist.Surname;
                        dbSpec.Description = specialist.Description;
                        dbSpec.Photo = specialist.Photo;
                        dbSpec.Service = specialist.Service;
                        dbSpec.Experience = specialist.Experience;
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Сохранено.", "Успех", MessageBoxButton.OK);
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
                Photo = openFileDialog.FileName;
                OnPropertyChanged(nameof(Photo));
            }
        }

        private void ClearFields()
        {
            Name = Surname = Description = Photo = Experience = Service = "";
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Photo));
            OnPropertyChanged(nameof(Experience));
            OnPropertyChanged(nameof(Service));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
