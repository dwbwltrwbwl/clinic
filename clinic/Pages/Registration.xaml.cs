using clinic.ApplicationData;
using clinic.Applications;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            ButtonRegist.IsEnabled = false;

            var specializations = AppConnect.model01.specializations.ToList();
            ComboBoxSpecializations.ItemsSource = specializations;
            ComboBoxSpecializations.DisplayMemberPath = "specialization_name";
            ComboBoxSpecializations.SelectedValuePath = "id";
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PBpassword.Password != TBPassword.Text)
            {
                ButtonRegist.IsEnabled = false;
                PBpassword.Background = Brushes.LightCoral;
                PBpassword.BorderBrush = Brushes.Red;
            }
            else
            {
                ButtonRegist.IsEnabled = true;
                PBpassword.Background = Brushes.LightGreen;
                PBpassword.BorderBrush = Brushes.Green;
            }
        }
        private void ButtonRegist_Click(object sender, RoutedEventArgs e)
        {

            string first_name = TBName.Text;
            string last_name = TBSurname.Text;
            string middle_name = TBMiddleName.Text;
            var selectedSpecialization = ComboBoxSpecializations.SelectedItem as specializations;
            string telephone = TBTelephone.Text;
            string login = TBLogin.Text;
            string password = TBPassword.Text;
            string passwordRepeat = PBpassword.Password;

            if (string.IsNullOrWhiteSpace(first_name) ||
                string.IsNullOrWhiteSpace(last_name) ||
                string.IsNullOrWhiteSpace(telephone) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(passwordRepeat) ||
                selectedSpecialization == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != passwordRepeat)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Applications.AppConnect.model01.doctors.Any(x => x.login == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var userRole = Applications.AppConnect.model01.roles.FirstOrDefault(r => r.role_name == "user");

                if (userRole == null)
                {
                    MessageBox.Show("Роль 'user' не найдена в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                doctors newDoctor = new doctors
                {
                    first_name = first_name,
                    last_name = last_name,
                    middle_name = middle_name,
                    id_specialization = selectedSpecialization.id_specialization,
                    telephone = telephone,
                    login = login,
                    password = password,
                    id_role = userRole.id_role
                };

                Applications.AppConnect.model01.doctors.Add(newDoctor);
                Applications.AppConnect.model01.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                TBName.Clear();
                TBSurname.Clear();
                TBMiddleName.Clear();
                ComboBoxSpecializations.SelectedIndex = -1;
                TBTelephone.Clear();
                TBLogin.Clear();
                TBPassword.Clear();
                PBpassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }
    }
}
