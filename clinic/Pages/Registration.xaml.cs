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

            var sites = AppConnect.model01.site.ToList();
            ComboBoxSite.ItemsSource = sites;
            ComboBoxSite.DisplayMemberPath = "site_number";
            ComboBoxSite.SelectedValuePath = "id";
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

            string first_name = TBSurname.Text;
            string last_name = TBName.Text;
            string middle_name = TBMiddleName.Text;
            DateTime? dateOfBirth = DPDateOfBirth.SelectedDate;
            string gender = TBGender.Text;
            string police = TBPolice.Text;
            var selectedSite = ComboBoxSite.SelectedItem as site;
            string telephone = TBTelephone.Text;
            string login = TBLogin.Text;
            string password = TBPassword.Text;
            string passwordRepeat = PBpassword.Password;

            if (string.IsNullOrWhiteSpace(first_name) ||
                string.IsNullOrWhiteSpace(last_name) ||
                string.IsNullOrWhiteSpace(middle_name) ||
                !dateOfBirth.HasValue ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(police) ||
                selectedSite == null ||
                string.IsNullOrWhiteSpace(telephone) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(passwordRepeat))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != passwordRepeat)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Applications.AppConnect.model01.doctors.Any(x => x.login == login) ||
                Applications.AppConnect.model01.patients.Any(x => x.login == login))
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

                patients newPatient = new patients
                {
                    first_name = first_name,
                    last_name = last_name,
                    middle_name = middle_name,
                    date_of_birth = dateOfBirth.Value,
                    gender = gender,
                    policy_number = police,
                    id_site = selectedSite.id_site,
                    telephone = telephone,
                    login = login,
                    password = password,
                    id_role = userRole.id_role
                };
                Applications.AppConnect.model01.patients.Add(newPatient);
                Applications.AppConnect.model01.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                TBName.Clear();
                TBSurname.Clear();
                TBMiddleName.Clear();
                DPDateOfBirth.SelectedDate = null;
                TBGender.Clear();
                TBPolice.Clear();
                ComboBoxSite.SelectedIndex = -1;
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
