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
        private List<string> specializations;
        public DataOutputUser()
        {
            InitializeComponent();

            allDoctors = AppConnect.model01.doctors.ToList();
            InitializeFilters();
            listDoctors.ItemsSource = allDoctors;
            UpdateCounter();
        }
        private void InitializeFilters()
        {
            specializations = allDoctors
                .Select(d => d.specializations.specialization_name)
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            ComboFilter.Items.Clear();
            ComboFilter.Items.Add("Все записи");
            foreach (var spec in specializations)
            {
                ComboFilter.Items.Add(spec);
            }
            ComboFilter.SelectedIndex = 0;
        }
        private void MyReceptions_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyReception());
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
            var filteredDoctors = allDoctors.AsQueryable();

            if (ComboFilter.SelectedItem is string selectedSpec && selectedSpec != "Все записи")
            {
                filteredDoctors = filteredDoctors
                    .Where(d => d.specializations.specialization_name == selectedSpec);
            }

            //сортировка

            listDoctors.ItemsSource = filteredDoctors.ToList();
            UpdateCounter();
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
