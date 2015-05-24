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

namespace VectorMedianFilter.WPF.Controls
{
    /// <summary>
    /// Interaction logic for WindowButtonPanel.xaml
    /// </summary>
    public partial class WindowButtonPanel : UserControl
    {
        public WindowButtonPanel()
        {
            InitializeComponent();
        }

        public Window ParentWindow
        {
            get { return (Window)GetValue(WindowProperty); }
            set { SetValue(WindowProperty, value); }
        }

        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register("ParentWindow", typeof(Window), typeof(WindowButtonPanel), new PropertyMetadata(default(Window)));

        public Boolean RestoreButtonVisibility
        {
            get { return (Boolean)GetValue(RestoreButtonVisibilityProperty); }
            set { SetValue(RestoreButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty RestoreButtonVisibilityProperty =
            DependencyProperty.Register("RestoreButtonVisibility", typeof(Boolean), typeof(WindowButtonPanel), new PropertyMetadata());

        private void restoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParentWindow.WindowState.Equals(WindowState.Maximized))
                ParentWindow.WindowState = WindowState.Normal;
            else
                ParentWindow.WindowState = WindowState.Maximized;
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.Close();
        }
    }
}
