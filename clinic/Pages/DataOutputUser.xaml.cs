using clinic.ApplicationData;
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
    /// Логика взаимодействия для DataOutputUser.xaml
    /// </summary>
    public partial class DataOutputUser : Page
    {
        private List<doctors> allDoctors;
        public DataOutputUser()
        {
            InitializeComponent();

            allDoctors = AppConnect.model01.doctors.ToList();
            listDoctors.ItemsSource = allDoctors;
            UpdateCounter();
        }

        private void MyReceptions_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateReceptionList();
        }
        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateReceptionList();
        }
        private void UpdateReceptionList()
        {

        }

        private void AddReceptions_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddReception());
        }
        private void UpdateCounter()
        {
            TextFoundCount.Text = $"Найдено: {listDoctors.Items.Count}";
        }
    }
}
