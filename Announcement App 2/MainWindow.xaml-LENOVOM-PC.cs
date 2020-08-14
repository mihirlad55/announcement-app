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

        readonly string exitIconPath = Settings.Default.exitIconPath;
        readonly string exitIconHoverPath = Settings.Default.exitIconHoverPath;
        readonly string minimizeIconPath = Settings.Default.minimizeIconPath;
        readonly string minimizeIconHoverPath = Settings.Default.minimizeIconHoverPath;

        DispatcherTimer timer = new DispatcherTimer();

        Window windowAddAnnouncement = new Window();

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
            setBrushPath(exitIconHoverPath, ref rectExit);
        }

        private void rectExit_MouseLeave(object sender, MouseEventArgs e)
        {
            setBrushPath(exitIconPath, ref rectExit);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblWelcome.Content = "Welcome " + Environment.UserName.Substring(0, 1).ToUpper() + Environment.UserName.Substring(1) + ",";
            rectMain.Width = 753;
            Width = 763;
        }
        private void setBrushPath(string path, ref Rectangle rectFill)
        {
            var imageBrush = rectFill.Fill as ImageBrush;
            imageBrush.ImageSource = new BitmapImage(new Uri(path));
            rectFill.Fill = imageBrush;
        }

        private void rectMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void rectMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            setBrushPath(minimizeIconHoverPath, ref rectMinimize);
        }

        private void rectMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            setBrushPath(minimizeIconPath, ref rectMinimize);
        }

        private void listViewAnnouncements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timer.Interval = new TimeSpan(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Width < 1080)
            {
                Width += 20;
                rectMain.Width += 20;
                Thickness newMinimizeMargin = rectMinimize.Margin;
                newMinimizeMargin.Left += 20;
                rectMinimize.Margin = newMinimizeMargin;
                Thickness newExitMargin = rectExit.Margin;
                newExitMargin.Left += 20;
                rectExit.Margin = newExitMargin;
                Thickness newDragBarMargin = rectDragBar.Margin;
                newDragBarMargin.Left += 20;
                rectDragBar.Margin = newDragBarMargin;

            }
            else
            {
                timer.Stop();
            }
        }

        private void btnAddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            var Window = windowAddAnnouncement as addAnnouncement;
            windowAddAnnouncement.ShowDialog();
        }
    }
}
