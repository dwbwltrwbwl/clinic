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
                var userObj = Applications.AppConnect.model01.doctors.FirstOrDefault(x => x.login == TBLogin.Text && x.password == PBPassword.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string fullName = $"{userObj.first_name} {userObj.last_name} {userObj.middle_name}";
                    var role = Applications.AppConnect.model01.roles.FirstOrDefault(r => r.id_role == userObj.id_role)?.role_name ?? "Не определена";
                    MessageBox.Show($"Здравствуйте, {fullName}. Ваша роль: {role}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppConnect.id_doctor = userObj.id_doctor;
                    NavigationService.Navigate(new DataOutput());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString(), "Критическая ошибка приложения", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonRegistr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
    }
}
