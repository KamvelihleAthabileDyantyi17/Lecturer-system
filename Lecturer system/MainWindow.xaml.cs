using System.Windows;
using System.Windows.Controls;

namespace Lecturer_system
{
    public partial class MainWindow : Window
    {
        private readonly string _userRole;

        public MainWindow(string userRole)
        {
            InitializeComponent();
            _userRole = userRole;
            SetupUIForRole();
        }

        public MainWindow()
        {
        }

        private void SetupUIForRole()
        {
            if (_userRole == "Lecturer")
            {
                SubmitClaimButton.Visibility = Visibility.Visible;
                MyClaimsButton.Visibility = Visibility.Visible;
                ApproveClaimsButton.Visibility = Visibility.Collapsed;
            }
            else if (_userRole == "Manager")
            {
                SubmitClaimButton.Visibility = Visibility.Collapsed;
                MyClaimsButton.Visibility = Visibility.Collapsed;
                ApproveClaimsButton.Visibility = Visibility.Visible;
            }
        }

        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            // The following lines will cause errors until we create the pages.
            // MainFrame.Navigate(new SubmitClaimPage()); 
        }

        private void MyClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            // MainFrame.Navigate(new MyClaimsPage());
        }

        private void ApproveClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            // MainFrame.Navigate(new ApproveClaimsPage());
        }
    }
}