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
            foreach (var specialization in specializations)
            {
                ComboBoxSpecializations.Items.Add(new ComboBoxItem { Content = specialization.specialization_name });
            }
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
            string telephone = TBTelephone.Text;
            //роль
            string login = TBLogin.Text;
            string password = TBPassword.Text;
            string passwordRepeat = PBpassword.Password;



            //if (string.IsNullOrWhiteSpace(authorName) ||
            //    string.IsNullOrWhiteSpace(login) ||
            //    string.IsNullOrWhiteSpace(password) ||
            //    string.IsNullOrWhiteSpace(passwordRepeat) ||
            //    !dateOfBirth.HasValue ||
            //    string.IsNullOrWhiteSpace(experience) ||
            //    string.IsNullOrWhiteSpace(email) ||
            //    string.IsNullOrWhiteSpace(telephone))
            //{
            //    MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //if (ApplicationData.AppConnect.model01.Authors.Count(x => x.Login == TBLogin.Text) > 0)
            //{
            //    MessageBox.Show("Пользователь с таким логином уже существует!", "Уведомление",
            //                    MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}

            //try
            //{
            //    Authors userObj = new Authors
            //    {
            //        AuthorName = authorName,
            //        Login = login,
            //        Password = password,
            //        DateOfBirth = dateOfBirth.Value,
            //        Experience = experience,
            //        Email = email,
            //        Telephone = telephone
            //    };
            //    AppConnect.model01.Authors.Add(userObj);
            //    AppConnect.model01.SaveChanges();
            //    MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            //    TBName.Clear();
            //    TBLogin.Clear();
            //    TBPassword.Clear();
            //    PBpassword.Clear();
            //    DPDateOfBirth.SelectedDate = null;
            //    TBExperience.Clear();
            //    TBEmail.Clear();
            //    TBTelephone.Clear();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ошибка при добавлении данных: " + ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }
    }
}
