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
using System.Windows.Shapes;
using System.Xml;
using System.Collections;
using System.ComponentModel;

namespace Announcement_App_2
{

    public partial class removeAnnouncement : Window
    {
        readonly string exitIconPath = "pack://siteoforigin:,,,/Resources/exitIcon.png";
        readonly string exitIconHoverPath = "pack://siteoforigin:,,,/Resources/exitIconHover.png";
        readonly string removeIconPath = "pack://siteoforigin:,,,/Resources/removeIcon.png";
        readonly string removeIconHoverPath = "pack://siteoforigin:,,,/Resources/removeIconHover.png";

        readonly string xmlDocumentPath = Announcement_App_2.Properties.Settings.Default.xmlDocumentPath;



        public removeAnnouncement()
        {
            InitializeComponent();
        }


        public void refreshAnnouncements()
        {
            dataGridAnnouncements.Items.Clear();

            foreach (Announcement announcement in Announcement.getAnnouncementsFromDatabase())
            {
                if (announcement.teacherName.ToLower() == Environment.UserName.ToLower())
                {
                    dataGridAnnouncements.Items.Add(announcement);
                }
            }
            sortDataGrid();
        }
        private void rectDragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void rectExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void rectExit_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(exitIconHoverPath, ref rectExit);
        }

        private void rectExit_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(exitIconPath, ref rectExit);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshAnnouncements();
        }

        private void dataGridAnnouncements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Announcement tempAnnouncement = (Announcement)dataGridAnnouncements.SelectedItem;
            if (tempAnnouncement != null)
            {
                try
                {
                    lblDescription.Content = tempAnnouncement.description;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void circleRemove_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(removeIconHoverPath, ref circleRemove);
        }

        private void circleRemove_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(removeIconPath, ref circleRemove);
        }

        private void circleRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            deleteAnnouncement();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Delete)
            {
                deleteAnnouncement();
            }
        }

        private void deleteAnnouncement()
        {
            if (dataGridAnnouncements.SelectedItem != null)
            {
                MessageBoxResult msgResult = MessageBox.Show("Are you sure you want to remove this announcement?", "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (msgResult == MessageBoxResult.Yes)
                {
                    Announcement.deleteAnnouncement((Announcement)dataGridAnnouncements.SelectedItem, true);
                    refreshAnnouncements();
                }
            }
            else
            {
                MessageBox.Show("Please select an announcement to remove", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void sortDataGrid()
        {
            {
                dataGridAnnouncements.Items.SortDescriptions.Clear();
                dataGridAnnouncements.Items.SortDescriptions.Add(new SortDescription("date", ListSortDirection.Descending));
                dataGridAnnouncements.Items.SortDescriptions.Add(new SortDescription("time", ListSortDirection.Descending));
            }
        }


        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            addSearchWatermark();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "Search...")
            {
                dataGridAnnouncements.Items.Clear();
                string search = txtSearch.Text;
                foreach (Announcement announcement in Announcement.getAnnouncements(search))
                {
                    if (announcement.teacherName.ToLower() == Environment.UserName.ToLower())
                    {
                        dataGridAnnouncements.Items.Add(announcement);
                    }
                }
            }
            else
            {
                refreshAnnouncements();
            }
        }

        private void addSearchWatermark()
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
                txtSearch.Foreground = new SolidColorBrush(Colors.LightSlateGray);
            }
        }

        private void rectMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            addSearchWatermark();
            dataGridAnnouncements.Focus();
        }
    }
}
