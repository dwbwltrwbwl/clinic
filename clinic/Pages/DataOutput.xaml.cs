﻿using clinic.ApplicationData;
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
    /// Логика взаимодействия для DataOutput.xaml
    /// </summary>
    public partial class DataOutput : Page
    {
        private List<receptions> allReceptions;
        private int currentDoctorId;

        public DataOutput()
        {
            InitializeComponent();

            currentDoctorId = AppConnect.id_doctor;
            LoadData();
            UpdateCounter();
        }

        private void LoadData()
        {
            allReceptions = AppConnect.model01.receptions
                .Where(x => x.id_doctor == currentDoctorId)
                .Include(r => r.status)
                .ToList();

            listReceptions.ItemsSource = allReceptions;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            receptions newReception = new receptions
            {
                id_doctor = currentDoctorId
            };
            EditPage editPage = new EditPage(newReception);
            editPage.ReceptionUpdated += UpdateReceptionList;
            NavigationService.Navigate(editPage);
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (listReceptions.SelectedItem == null)
            {
                MessageBox.Show("Выберите приём!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (listReceptions.SelectedItem is receptions selectedReception)
            {
                EditPage editPage = new EditPage(selectedReception);
                editPage.ReceptionUpdated += UpdateReceptionList;
                NavigationService.Navigate(editPage);
            }
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
            var baseQuery = AppConnect.model01.receptions
                .Where(x => x.id_doctor == currentDoctorId)
                .Include(r => r.status);

            var filtered = baseQuery.AsQueryable();

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

        //private void FillComboStatus()
        //{
        //    var statuses = AppConnect.model01.status.ToList();
        //    foreach (var item in statuses) {
        //        ComboFilter.Items.Add(item.status_name);
        //    }
        //    ComboFilter.SelectedIndex = 0;
        //    MessageBox.Show("Combo filled");
        //}

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }
    }
}
