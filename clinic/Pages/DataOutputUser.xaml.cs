﻿using clinic.ApplicationData;
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
            listDoctors.ItemsSource = allDoctors;
            UpdateCounter();
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }
    }
}
