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
using System.Windows.Threading;
using Announcement_App_2.Properties;
using System.Xml;
using System.ComponentModel;

namespace Announcement_App_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        readonly string exitIconPath = "pack://siteoforigin:,,,/Resources/exitIcon.png";
        readonly string exitIconHoverPath = "pack://siteoforigin:,,,/Resources/exitIconHover.png";
        readonly string minimizeIconPath = "pack://siteoforigin:,,,/Resources/minimizeIcon.png";
        readonly string minimizeIconHoverPath = "pack://siteoforigin:,,,/Resources/minimizeIconHover.png";


        private void rectStatusBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(exitIconHoverPath, ref rectExit);
        }

        private void rectExit_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(exitIconPath, ref rectExit);
        }

        private void rectMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void rectMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(minimizeIconHoverPath, ref rectMinimize);
        }

        private void rectMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(minimizeIconPath, ref rectMinimize);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Announcement.getAnnouncementsFromDatabase();
            lblWelcome.Content = "Welcome " + Environment.UserName.Substring(0, 1).ToUpper() + Environment.UserName.Substring(1) + ",";
      //      refreshAnnouncements();
            if (string.IsNullOrEmpty(Announcement_App_2.Properties.Settings.Default.xmlDocumentPath))
            {
                Announcement_App_2.Properties.Settings.Default.xmlDocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NPSSAnnouncements\\announcements.xml";
                Announcement_App_2.Properties.Settings.Default.Save();
            }
        }

        private void btnAddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            addAnnouncement windowAddAnnouncement = new addAnnouncement();
            windowAddAnnouncement.ShowDialog();
            refreshAnnouncements();

        }

        public void refreshAnnouncements()
        {
            dataGridAnnouncements.Items.Clear();
            List<Announcement> announcementList = Announcement.getAnnouncementsFromDatabase();
            foreach (Announcement announcement in announcementList)
            {
                if (announcement.description == "")
                {
                    announcement.description = "No Description.";
                }
                dataGridAnnouncements.Items.Add(announcement);
            }
            sortDataGrid();

            foreach (DataGridColumn column in dataGridAnnouncements.Columns)
            {
                column.SortDirection = null;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshAnnouncements();
        }

        private void sortDataGrid()
        {
            dataGridAnnouncements.Items.SortDescriptions.Clear();
            dataGridAnnouncements.Items.SortDescriptions.Add(new SortDescription("date", ListSortDirection.Descending));
            dataGridAnnouncements.Items.SortDescriptions.Add(new SortDescription("time", ListSortDirection.Descending));
        }

        private void dataGridAnnouncements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridAnnouncements.SelectedItem != null)
            {
                lblDescription.Content = ((Announcement)dataGridAnnouncements.SelectedItem).description;
            }
        }

        private void btnRemoveAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            removeAnnouncement windowremoveAnnouncement = new removeAnnouncement();
            windowremoveAnnouncement.ShowDialog();
            refreshAnnouncements();
        }

        private void btnEditAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            editAnnouncementList windowEditAnnouncementList = new editAnnouncementList();
            windowEditAnnouncementList.ShowDialog();
            refreshAnnouncements();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            settings windowSettings = new settings();
            windowSettings.ShowDialog();
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
                    dataGridAnnouncements.Items.Add(announcement);
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
