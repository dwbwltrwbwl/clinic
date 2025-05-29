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
            CBGender.Items.Add("Мужской");
            CBGender.Items.Add("Женский");
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
            string gender = CBGender.SelectedItem as string;
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
                CBGender.SelectedIndex = -1;
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
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0) &&
                e.Text != " " &&
                e.Text != "-" &&
                e.Text != "'")
            {
                e.Handled = true;
            }
        }
        private void TBTelephone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }
            string currentText = TBTelephone.Text.Replace("+7 (", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (currentText.Length >= 11)
            {
                e.Handled = true;
            }
        }

        private void TBTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string digitsOnly = new string(TBTelephone.Text.Where(char.IsDigit).ToArray());
            if (digitsOnly.Length > 0 && digitsOnly[0] != '7' && digitsOnly[0] != '8')
            {
                digitsOnly = "7" + digitsOnly;
            }
            string formattedPhone = "+7 (";
            if (digitsOnly.Length > 1)
                formattedPhone += digitsOnly.Substring(1, Math.Min(3, digitsOnly.Length - 1));
            if (digitsOnly.Length > 4)
                formattedPhone += ") " + digitsOnly.Substring(4, Math.Min(3, digitsOnly.Length - 4));
            if (digitsOnly.Length > 7)
                formattedPhone += "-" + digitsOnly.Substring(7, Math.Min(2, digitsOnly.Length - 7));
            if (digitsOnly.Length > 9)
                formattedPhone += "-" + digitsOnly.Substring(9, Math.Min(2, digitsOnly.Length - 9));
            TBTelephone.TextChanged -= TBTelephone_TextChanged;
            TBTelephone.Text = formattedPhone;
            TBTelephone.CaretIndex = formattedPhone.Length;
            TBTelephone.TextChanged += TBTelephone_TextChanged;
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

    }
}
