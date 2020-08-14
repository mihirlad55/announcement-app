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
using Announcement_App_2.Properties;

namespace Announcement_App_2
{
    /// <summary>
    /// Interaction logic for settings.xaml
    /// </summary>
    public partial class settings : Window
    {
        public settings()
        {
            InitializeComponent();
        }

        readonly string exitIconPath = "pack://siteoforigin:,,,/Resources/exitIcon.png";
        readonly string exitIconHoverPath = "pack://siteoforigin:,,,/Resources/exitIconHover.png";
        readonly string applyIconPath = "pack://siteoforigin:,,,/Resources/applyIcon.png";
        readonly string applyIconHoverPath = "pack://siteoforigin:,,,/Resources/applyIconHover.png";


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
            Settings.Default.minimizeToTray = (bool)checkBoxMinimizeToTray.IsChecked;
            Settings.Default.showNotifications = (bool)checkBoxNotifications.IsChecked;
            Settings.Default.launchOnStartup = (bool)checkBoxStartup.IsChecked;

            Settings.Default.refreshInterval = Convert.ToInt32(txtRefreshInterval.Text);
            Settings.Default.oldAnnouncementTime = cmbOldAnnouncements.SelectedIndex + 1;
            Settings.Default.Save();
            Close();

        }

        private void txtRefreshInterval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Substring(0,1) == "D" && e.Key.ToString().Length == 2)
            {
                if (char.IsDigit(Convert.ToChar(e.Key.ToString().Substring(1, 1))))
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void rectMain_Loaded(object sender, RoutedEventArgs e)
        {
            checkBoxMinimizeToTray.IsChecked = Settings.Default.minimizeToTray;
            checkBoxNotifications.IsChecked = Settings.Default.showNotifications;
            checkBoxStartup.IsChecked = Settings.Default.launchOnStartup;

            txtRefreshInterval.Text = Settings.Default.refreshInterval.ToString();
            cmbOldAnnouncements.SelectedIndex = Settings.Default.oldAnnouncementTime - 1;
        }
    }
}
