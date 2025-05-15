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
    /// Логика взаимодействия для MyReception.xaml
    /// </summary>
    public partial class MyReception : Page
    {
        private List<receptions> allReceptions;
        public MyReception()
        {
            InitializeComponent();

            LoadMyReceptions();
            UpdateCounter();
        }
        private void LoadMyReceptions()
        {
            allReceptions = AppConnect.model01.receptions
                .Where(x => x.id_patient == AppConnect.id_patient)
                .ToList();
            listReceptions.ItemsSource = allReceptions;
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
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataOutputUser());
        }
        private void UpdateCounter()
        {
            TextFoundCount.Text = $"Найдено: {listReceptions.Items.Count}";
        }
    }
}
