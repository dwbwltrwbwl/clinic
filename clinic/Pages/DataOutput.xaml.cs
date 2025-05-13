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
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //receptions newReception = new receptions();
            //EditReception editPage = new EditReception(newReception);
            //editPage.RecipeUpdated += UpdateRecipeList;
            //NavigationService.Navigate(EditPage);
        }
    }
}
