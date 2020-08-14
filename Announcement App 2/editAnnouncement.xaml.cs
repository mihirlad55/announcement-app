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

namespace Announcement_App_2
{
    /// <summary>
    /// Interaction logic for editAnnouncement.xaml
    /// </summary>
    public partial class editAnnouncement : Window
    {

        readonly string exitIconPath = "pack://siteoforigin:,,,/Resources/exitIcon.png";
        readonly string exitIconHoverPath = "pack://siteoforigin:,,,/Resources/exitIconHover.png";
        readonly string applyIconPath = "pack://siteoforigin:,,,/Resources/applyIcon.png";
        readonly string applyIconHoverPath = "pack://siteoforigin:,,,/Resources/applyIconHover.png";

        readonly string xmlDocumentPath = Announcement_App_2.Properties.Settings.Default.xmlDocumentPath;

        Announcement selectedAnnouncement;

        public editAnnouncement()
        {
            InitializeComponent();
        }

        public editAnnouncement(Announcement announcement)
        {
            selectedAnnouncement = announcement;
            InitializeComponent();
        }

        private void rectMain_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Announcement.subjects subject in Enum.GetValues(typeof(Announcement.subjects)).Cast<Announcement.subjects>())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = subject.ToString();
                cmbSubject.Items.Add(item);
            }

            txtTitle.Text = selectedAnnouncement.title;
            txtLocation.Text = selectedAnnouncement.location;
            txtDescription.Text = selectedAnnouncement.description;
            datePicker.Text = selectedAnnouncement.deadline;
            cmbSubject.Text = selectedAnnouncement.subject.ToString();
        }

        private void rectDragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void rectExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void rectExit_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(exitIconPath, ref rectExit);
        }

        private void rectExit_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(exitIconHoverPath, ref rectExit);
        }

        private void circleApply_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(applyIconHoverPath, ref circleApply);
        }

        private void circleApply_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(applyIconPath, ref circleApply);
        }

        private void circleApply_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            saveAnnouncement();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveAnnouncement();
            }
        }

        private void saveAnnouncement()
        {
            if (txtTitle.Text.Trim() != "")
            {
                if (Announcement.deleteAnnouncement(selectedAnnouncement, false) == 0 && Announcement.addAnnouncementToDatabase(new Announcement(
                    Environment.UserName.Substring(0, 1).ToUpper() + Environment.UserName.Substring(1, Environment.UserName.Length - 1),
                    txtTitle.Text,
                    DateTime.Now.ToShortDateString(),
                    DateTime.Now.ToShortTimeString(),
                    datePicker.Text,
                    txtLocation.Text,
                    txtDescription.Text,
                    cmbSubject.Text), false) == 0)
                {
                    MessageBox.Show("Announcement saved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Error adding announcement", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Announcement must have a title.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
