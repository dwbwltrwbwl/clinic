using clinic.ApplicationData;
using clinic.Applications;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data;
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

namespace clinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private receptions reception;
        public event Action ReceptionUpdated;
        public EditPage(receptions reception)
        {
            InitializeComponent();
            this.reception = reception ?? throw new ArgumentNullException(nameof(reception));

            LoadPatients();
            LoadDoctors();
            LoadStatus();
            LoadDiagnosis();

            EditDate.SelectedDate = reception.date_reception;
            EditTime.Text = reception.time_reception?.ToString(@"hh\:mm");
            EditTreatment.Text = reception.treatment;
            EditSymptom.Text = reception.symptoms;
            EditPatient.SelectedItem = reception.patients;
            EditDoctor.SelectedItem = reception.doctors;
            EditStatus.SelectedItem = reception.status;
            EditDiagnosis.SelectedItem = reception.diagnosis;
        }
        public EditPage()
        {
            InitializeComponent();
            reception = new receptions();

            LoadPatients();
            LoadDoctors();
            LoadStatus();
            LoadDiagnosis();
        }
        private void LoadPatients()
        {
            var patients = AppConnect.model01.patients.ToList();
            EditPatient.ItemsSource = patients;
            EditPatient.DisplayMemberPath = "FullName";
        }
        private void LoadDoctors()
        {
            var doctors = AppConnect.model01.doctors.ToList();
            EditDoctor.ItemsSource = doctors;
            EditDoctor.DisplayMemberPath = "FullName";
        }
        private void LoadStatus()
        {
            var status = AppConnect.model01.status.ToList();
            EditStatus.ItemsSource = status;
            EditStatus.DisplayMemberPath = "status_name";
        }
        private void LoadDiagnosis()
        {
            var diagnosis = AppConnect.model01.diagnosis.ToList();
            EditDiagnosis.ItemsSource = diagnosis;
            EditDiagnosis.DisplayMemberPath = "diagnosis_name";
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EditPatient.SelectedItem == null)
                {
                    MessageBox.Show("Выберите пациента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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

                reception.patients = (patients)EditPatient.SelectedItem;
                reception.doctors = (doctors)EditDoctor.SelectedItem;
                reception.status = (status)EditStatus.SelectedItem;
                reception.diagnosis = (diagnosis)EditDiagnosis.SelectedItem;
                reception.date_reception = EditDate.SelectedDate.Value;
                reception.time_reception = TimeSpan.Parse(EditTime.Text);
                reception.treatment = EditTreatment.Text;
                reception.symptoms = EditSymptom.Text;

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
            NavigationService.Navigate(new DataOutput());
        }
    }
}
