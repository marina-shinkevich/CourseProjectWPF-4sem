using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;
using WpfAppCourse.Services;
using System.Text.RegularExpressions;





namespace WpfAppCourse.ViewModels
{
    public class AppointmentViewModel : ViewModel
    {
        public ObservableCollection<MainSpecialists> AvailableMasters { get; set; } = new ObservableCollection<MainSpecialists>();
        private readonly EmailService _emailService = new EmailService();
        public MainSpecialists SelectedMaster { get => _selectedMaster; set { _selectedMaster = value; OnPropertyChanged(); } }
        public DateTime? SelectedDate { get => _selectedDate; set { _selectedDate = value; OnPropertyChanged(); } }
        public string TimeInput { get => _timeInput; set { _timeInput = value; OnPropertyChanged(); } }
       
        public int ReservationId { get; set; }
        private string _serviceName;
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        public string ServiceName
        {
            get => _serviceName;
            set
            {
                _serviceName = value;
                OnPropertyChanged(nameof(ServiceName));
            }
        }

        private int _serviceId;
        private int? _preselectedMasterId;
        private User _currentUser;
        private MainSpecialists _selectedMaster;
        private DateTime? _selectedDate;
        private string _timeInput;

        public ICommand ReserveCommand { get; }
        public ICommand CloseWindowCommand { get; set; }

        public AppointmentViewModel(MainServices selectedService)
            : this(selectedService.name_of_service, selectedService.ID, null) { }

        public AppointmentViewModel(MainSpecialists specialist, string serviceName)
            : this(serviceName, specialist.Service, specialist.ID) { }

        private AppointmentViewModel(string serviceName, int serviceId, int? preselectedMasterId)
        {
            _currentUser = App.CurrentUser;
            ServiceName = serviceName;
            _serviceId = serviceId;
            _preselectedMasterId = preselectedMasterId;
            TimeInput = "Например, 14:00";

            ReserveCommand = new RelayCommand(ExecuteReserve);
            LoadMasters();
        }

        private void LoadMasters()
        {
            using (var db = new AppContext())
            {
                var masters = db.MainSpecialists.Where(s => s.Service == _serviceId).ToList();

                

                AvailableMasters.Clear();
                foreach (var master in masters)
                    AvailableMasters.Add(master);

                if (_preselectedMasterId.HasValue)
                {
                    var match = masters.FirstOrDefault(m => m.ID == _preselectedMasterId.Value);
                    if (match != null)
                        SelectedMaster = match;
                }
            }
        }

        private async void ExecuteReserve()
        {
            if (SelectedDate == null)
            {
                MessageBox.Show("Выберите дату записи");
                return;
            }

            if (string.IsNullOrWhiteSpace(TimeInput) || TimeInput == "Например, 14:00")
            {
                MessageBox.Show("Укажите время процедуры");
                return;
            }

            if (SelectedMaster == null)
            {
                MessageBox.Show("Выберите мастера");
                return;
            }

            string trimmedTime = TimeInput.Trim();

            // Если только число — добавим ":00"
            if (Regex.IsMatch(trimmedTime, @"^\d{1,2}$"))
            {
                trimmedTime += ":00";
            }

            if (!TimeSpan.TryParse(trimmedTime, out var time))
            {
                MessageBox.Show("Некорректный формат времени. Используйте формат ЧЧ:ММ (например, 14:30)");
                return;
            }

            DateTime selectedDateTime = SelectedDate.Value.Date.Add(time);
            if (selectedDateTime < DateTime.Now)
            {
                MessageBox.Show("Нельзя записаться на прошедшую дату и время");
                return;
            }

            int masterId = SelectedMaster.ID;
            int userId = _currentUser.ID;
            int serviceId;
            int serviceDuration;

            using (var context = new AppContext())
            {
                var service = context.MainServices.FirstOrDefault(s => s.name_of_service == ServiceName);
                if (service == null)
                {
                    MessageBox.Show("Услуга не найдена");
                    return;
                }

                serviceId = service.ID;
                serviceDuration = service.durationminuts;

                var existingReservations = context.Reserv
                    .Where(r => r.Date == SelectedDate && r.Master_Id == masterId)
                    .ToList();

                TimeSpan newStart = time;
                TimeSpan newEnd = time.Add(TimeSpan.FromMinutes(serviceDuration));

                foreach (var existing in existingReservations)
                {
                    var existingService = context.MainServices.FirstOrDefault(s => s.ID == existing.Service_Id);
                    if (existingService == null)
                        continue;

                    TimeSpan existingStart = existing.Time;
                    TimeSpan existingEnd = existingStart.Add(TimeSpan.FromMinutes(existingService.durationminuts));

                    if (newStart < existingEnd && newEnd > existingStart)
                    {
                        MessageBox.Show("Выбранное время пересекается с другой записью мастера.");
                        return;
                    }
                }

                int nextId = context.Reserv.OrderByDescending(s => s.ID).FirstOrDefault()?.ID + 1 ?? 1;

                var newReservation = new Reserv
                {
                    ID = nextId,
                    Date = SelectedDate.Value,
                    Time = time,
                    Master_Id = masterId,
                    Service_Id = serviceId,
                    User_Id = userId
                };

                context.Reserv.Add(newReservation);
                context.SaveChanges();
            }
        


        MessageBox.Show("Запись успешно создана!");

            try
            {
                await _emailService.SendAppointmentConfirmationAsync(
                    _currentUser.e_mail,
                    _currentUser.name,
                    ServiceName,
                    SelectedDate.Value,
                    time,
                    $"{SelectedMaster.Name} {SelectedMaster.Surname}");

                MessageBox.Show("Письмо с подтверждением отправлено на вашу почту!");
            }
            catch (Exception ex)
            {
                string errorMessage = $"Ошибка при отправке письма: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nДетали: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMessage);
                Console.WriteLine($"Ошибка отправки email: {ex}");
            }
        }
    }
}
