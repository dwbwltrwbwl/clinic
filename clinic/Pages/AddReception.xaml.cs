using clinic.ApplicationData;
using clinic.Applications;
using System;
using System.Collections.Generic;
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
        public AddReception()
        {
            InitializeComponent();
            reception = new receptions();

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

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataOutputUser());
        }
    }
}
