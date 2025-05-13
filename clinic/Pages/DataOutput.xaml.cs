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
    /// Логика взаимодействия для DataOutput.xaml
    /// </summary>
    public partial class DataOutput : Page
    {
        private List<receptions> allReceptions;
        public DataOutput()
        {
            InitializeComponent();

            allReceptions = AppConnect.model01.receptions.ToList();
            listReceptions.ItemsSource = allReceptions;
            UpdateCounter();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //receptions newReception = new receptions();
            //EditReception editPage = new EditReception(newReception);
            //editPage.RecipeUpdated += UpdateRecipeList;
            //NavigationService.Navigate(EditPage);
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecipeList();
        }
        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecipeList();
        }
        private void UpdateRecipeList()
        {
            var filtered = allReceptions.AsQueryable();

            switch ((ComboFilter.SelectedItem as ComboBoxItem)?.Content.ToString())
            {
                case "Отменённые":
                    filtered = filtered.Where(r => r.status.status_name == "Отменён");
                    break;
                case "Завершенные":
                    filtered = filtered.Where(r => r.status.status_name == "Завершен");
                    break;
                case "Ожидаемые":
                    filtered = filtered.Where(r => r.status.status_name == "Ожидается");
                    break;
            }

            switch ((ComboSort.SelectedItem as ComboBoxItem)?.Content.ToString())
            {
                case "По дате":
                    filtered = filtered.OrderByDescending(r => r.date_reception);
                    break;
                case "По статусу":
                    filtered = filtered.OrderBy(r => r.status.status_name);
                    break;
            }

            listReceptions.ItemsSource = filtered.ToList();
            UpdateCounter();
        }
        private void UpdateCounter()
        {
            TextFoundCount.Text = $"Найдено: {listReceptions.Items.Count}";
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
