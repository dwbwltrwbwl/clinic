using clinic.ApplicationData;
using clinic.Applications;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace clinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddReception.xaml
    /// </summary>
    public partial class AddReception : Page
    {
        private receptions reception;
        public event Action ReceptionUpdated;
        public AddReception()
        {
            InitializeComponent();
            reception = new receptions();
            var record = AppConnect.model01.receptions.Where(x => x.id_patient == AppConnect.id_patient).Select(x => x.id_doctor).ToList();

            LoadDoctors();
        }
        private void LoadDoctors()
        {
            var doctors = AppConnect.model01.doctors.ToList();
            EditDoctor.ItemsSource = doctors;
            EditDoctor.DisplayMemberPath = "FullName";
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EditDoctor.SelectedItem == null)
                {
                    MessageBox.Show("Выберите врача!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (EditDate.SelectedDate == null)
                {
                    MessageBox.Show("Укажите дату приёма!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                string time = EditTime.Text;
                if (string.IsNullOrWhiteSpace(time))
                {
                    MessageBox.Show("Укажите время приёма!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!DateTime.TryParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime tempTime))
                {
                    MessageBox.Show("Неверный формат времени!\nИспользуйте формат ЧЧ:мм (24-часовой)",
                                   "Ошибка",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
                    return;
                }
                TimeSpan ts = tempTime.TimeOfDay;
                if (ts.Hours < 8 || ts.Hours >= 18)
                {
                    MessageBox.Show("Приём осуществляется с 08:00 до 18:00",
                                   "Ошибка",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
                    return;
                }

                reception.id_patient = AppConnect.id_patient;
                reception.doctors = (doctors)EditDoctor.SelectedItem;
                reception.date_reception = EditDate.SelectedDate.Value.Date;
                reception.time_reception = ts;
                reception.id_status = 3;
                reception.symptoms = string.Empty;
                reception.id_diagnosis = null;
                reception.treatment = string.Empty;

                if (reception.id_reception == 0)
                {
                    AppConnect.model01.receptions.Add(reception);
                }
                else
                {
                    AppConnect.model01.Entry(reception).State = EntityState.Modified;
                }

                AppConnect.model01.SaveChanges();
                ReceptionUpdated?.Invoke();
                NavigationService.GoBack();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный формат времени! Используйте ЧЧ:ММ",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка сохранения в БД: {ex.InnerException?.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                             "Ошибка",
                              MessageBoxButton.OK,
                             MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataOutputUser());
        }
    }
}
