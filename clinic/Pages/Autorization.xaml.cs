using clinic.Applications;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void ButtonVhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var doctorObj = Applications.AppConnect.model01.doctors.FirstOrDefault(x => x.login == TBLogin.Text && x.password == PBPassword.Password);
                if (doctorObj != null)
                {
                    string fullName = $"{doctorObj.first_name} {doctorObj.last_name} {doctorObj.middle_name}";
                    var role = Applications.AppConnect.model01.roles.FirstOrDefault(r => r.id_role == doctorObj.id_role)?.role_name ?? "Не определена";
                    MessageBox.Show($"Здравствуйте, {fullName}. Ваша роль: {role}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppConnect.id_doctor = doctorObj.id_doctor;
                    NavigationService.Navigate(new DataOutput());
                    return;
                }

                var patientObj = Applications.AppConnect.model01.patients.FirstOrDefault(x => x.login == TBLogin.Text && x.password == PBPassword.Password);
                if (patientObj != null)
                {
                    string fullName = $"{patientObj.first_name} {patientObj.last_name} {patientObj.middle_name}";
                    var role = Applications.AppConnect.model01.roles.FirstOrDefault(r => r.id_role == patientObj.id_role)?.role_name ?? "Не определена";
                    MessageBox.Show($"Здравствуйте, {fullName}. Ваша роль: {role}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppConnect.id_patient = patientObj.id_patient;
                    NavigationService.Navigate(new DataOutputUser());
                    return;
                }

                MessageBox.Show("Такого пользователя нет", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Критическая ошибка приложения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRegistr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
    }
}
