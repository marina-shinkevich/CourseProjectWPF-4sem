using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppCourse.Views;

namespace WpfAppCourse.ViewModels
{
    public class AdminReservsViewModel:ViewModel
    {
        private ObservableCollection<Reserv> _reservs;
        public ObservableCollection<Reserv> Reservs
        {
            get => _reservs;
            set { _reservs = value; OnPropertyChanged(); }
        }
        private Reserv _selectedReserv;
        public Reserv SelectedReserv
        {
            get => _selectedReserv;
            set
            {
                _selectedReserv = value;
                OnPropertyChanged();

                // Обновляем доступность команды
                (DeleteReservCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Date { get; set; }
        public string Time { get; set; }
        public string Master_Id { get; set; }
        public string Service_Id { get; set; }
        public string User_Id { get; set; }
     

        public ICommand DeleteReservCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public AdminReservsViewModel()
        {
            LoadReservs();

            DeleteReservCommand = new RelayCommand(
     () => DeleteReserv(SelectedReserv),
     () => SelectedReserv != null
 );

            SaveChangesCommand = new RelayCommand(SaveChanges);
            
        }

        private void LoadReservs()
        {
            using (var db = new AppContext())
                Reservs = new ObservableCollection<Reserv>(db.Reserv.ToList());
        }
        private void DeleteReserv(Reserv reserv)
        {
            try

            {
                if (reserv == null)
                {
                    MessageBox.Show("Пожалуйста, выберите запись для удаления.",
                                  "Удаление",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Вы уверены, что хотите удалить \"{reserv.ID}\"?",
                                           "Подтверждение удаления",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppContext())
                    {
                        var reservToDelete = db.Reserv.Find(reserv.ID);
                        if (reservToDelete != null)
                        {
                            db.Reserv.Remove(reservToDelete);
                            db.SaveChanges();
                            MessageBox.Show("Запись удалена.",
                                          "Удалено",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);

                            // Обновляем список услуг
                            LoadReservs(); 
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
    using (var db = new AppContext())
    {
                // Загружаем оригинальные значения резервов из БД
                // Получаем ID всех резервов, которые редактируем
                var reservIds = Reservs.Select(r => r.ID).ToList();

                // Загружаем из БД исходные записи для отката в случае ошибки
                var originalReservs = db.Reserv
                    .Where(r => reservIds.Contains(r.ID))
                    .ToList();


                foreach (var reserv in Reservs)
        {
            var dbReserv = db.Reserv.FirstOrDefault(s => s.ID == reserv.ID);
            if (dbReserv != null)
            {
                // Валидация ID мастера
                if (!db.MainSpecialists.Any(m => m.ID == reserv.Master_Id))
                {
                    MessageBox.Show($"Мастер с ID {reserv.Master_Id} не существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    RevertChanges(originalReservs);
                    return;
                }

                // Валидация ID услуги
                if (!db.MainServices.Any(s => s.ID == reserv.Service_Id))
                {
                    MessageBox.Show($"Услуга с ID {reserv.Service_Id} не существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    RevertChanges(originalReservs);
                    return;
                }

                // Валидация ID пользователя
                if (!db.AllUsers.Any(u => u.ID == reserv.User_Id))
                {
                    MessageBox.Show($"Пользователь с ID {reserv.User_Id} не существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    RevertChanges(originalReservs);
                    return;
                }

                // Проверка на дубликат
                bool isDuplicate = db.Reserv.Any(r =>
                    r.ID != reserv.ID &&
                    r.Date == reserv.Date &&
                    r.Time == reserv.Time &&
                    r.Master_Id == reserv.Master_Id &&
                    r.Service_Id == reserv.Service_Id &&
                    r.User_Id == reserv.User_Id);

                if (isDuplicate)
                {
                    MessageBox.Show(
                        $"Дублирующая запись найдена:\nДата: {reserv.Date.ToShortDateString()}\nВремя: {reserv.Time}\nМастер ID: {reserv.Master_Id}\nПользователь ID: {reserv.User_Id}\nУслуга ID: {reserv.Service_Id}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    RevertChanges(originalReservs);
                    return;
                }

                // Обновление записи
                dbReserv.Date = reserv.Date;
                dbReserv.Time = reserv.Time;
                dbReserv.Master_Id = reserv.Master_Id;
                dbReserv.Service_Id = reserv.Service_Id;
                dbReserv.User_Id = reserv.User_Id;
            }
        }

        db.SaveChanges();
        MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

// Метод возврата исходных значений в UI-коллекцию
private void RevertChanges(List<Reserv> originalReservs)
{
    foreach (var original in originalReservs)
    {
        var reserv = Reservs.FirstOrDefault(r => r.ID == original.ID);
        if (reserv != null)
        {
            reserv.Date = original.Date;
            reserv.Time = original.Time;
            reserv.Master_Id = original.Master_Id;
            reserv.Service_Id = original.Service_Id;
            reserv.User_Id = original.User_Id;
        }
    }
}




    }
}
