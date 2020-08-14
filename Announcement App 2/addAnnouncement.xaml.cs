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
using System.Data.Sql;
using System.Data.SqlClient;
using MySql;

namespace Announcement_App_2
{
    /// <summary>
    /// Interaction logic for addAnnouncement.xaml
    /// </summary>
    public partial class addAnnouncement : Window
    {
        public addAnnouncement()
        {
            InitializeComponent();
        }

        readonly string exitIconPath = "pack://siteoforigin:,,,/Resources/exitIcon.png";
        readonly string exitIconHoverPath = "pack://siteoforigin:,,,/Resources/exitIconHover.png";
        readonly string addIconPath = "pack://siteoforigin:,,,/Resources/addIcon.png";
        readonly string addIconHoverPath = "pack://siteoforigin:,,,/Resources/addIconHover.png";

        readonly string xmlDocumentPath = Announcement_App_2.Properties.Settings.Default.xmlDocumentPath;

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

        private void circleAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(addIconHoverPath, ref circleAdd);
        }

        private void circleAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            imageUtils.setBrushPath(addIconPath, ref circleAdd);
        }

        private void circleAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            addAnnouncementToDatabase();
            addAnnouncementToXmlFile();
        }

        private void rectMain_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Announcement.subjects subject in Enum.GetValues(typeof(Announcement.subjects)).Cast<Announcement.subjects>())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = subject.ToString();
                cmbSubject.Items.Add(item);

            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                addAnnouncementToXmlFile();
                addAnnouncementToDatabase();
            }
        }

        private void addAnnouncementToXmlFile()
        {


            //if (txtTitle.Text != "" || txtTitle.Text.Trim() != "")
            //{
            //    if (cmbSubject.SelectedIndex != 0)
            //    {

            //        Announcement.addAnnouncement(new Announcement(
            //            Environment.UserName.Substring(0, 1).ToUpper() + Environment.UserName.Substring(1, Environment.UserName.Length - 1),
            //            txtTitle.Text,
            //            DateTime.Now.ToShortDateString(),
            //            DateTime.Now.ToString("HH:mm"),
            //            datePicker.Text,
            //            txtLocation.Text,
            //            txtDescription.Text,
            //            cmbSubject.Text), true);

            //        Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please select a subject. Select general if you are unsure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Title cannot be blank. Please insert a valid title.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void addAnnouncementToDatabase()
        {


            if (txtTitle.Text != "" || txtTitle.Text.Trim() != "")
            {
                if (cmbSubject.SelectedIndex != 0)
                {

                    Announcement.addAnnouncementToDatabase(new Announcement(
                        Environment.UserName.Substring(0, 1).ToUpper() + Environment.UserName.Substring(1, Environment.UserName.Length - 1),
                        txtTitle.Text,
                        "",
                        "",
                        datePicker.Text,
                        txtLocation.Text,
                        txtDescription.Text,
                        cmbSubject.Text), true);

                    Close();
                }
                else
                {
                    MessageBox.Show("Please select a subject. Select general if you are unsure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Title cannot be blank. Please insert a valid title.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
