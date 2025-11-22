using Lecturer_system.Models;
using System.Windows;

namespace Lecturer_system
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDashboard();
        }

        private void InitializeDashboard()
        {
            // SAFETY CHECK: If no one is logged in, go back to login
            if (AppSession.CurrentUser == null)
            {
                LoginWindow login = new LoginWindow();
                login.Show();
                this.Close();
                return;
            }

            string userRole = AppSession.CurrentUser.Role;

            // ==================================================
            // 1. HIDE EVERYTHING FIRST (Reset state)
            // ==================================================
            SubmitClaimButton.Visibility = Visibility.Collapsed;
            MyClaimsButton.Visibility = Visibility.Collapsed;
            ApproveClaimsButton.Visibility = Visibility.Collapsed;
            // We need to add a button for HR in your XAML too, let's assume its called 'HRButton'
            // HRButton.Visibility = Visibility.Collapsed; 

            // ==================================================
            // 2. SHOW BUTTONS BASED ON ROLE
            // ==================================================

            if (userRole == "Lecturer")
            {
                SubmitClaimButton.Visibility = Visibility.Visible;
                MyClaimsButton.Visibility = Visibility.Visible;

                // Auto-Navigate to Submit Page
                MainFrame.Navigate(new SubmitClaimPage());
            }
            else if (userRole == "Coordinator" || userRole == "Manager")
            {
                ApproveClaimsButton.Visibility = Visibility.Visible;

                // Auto-Navigate to Approval Page
                MainFrame.Navigate(new ApproveClaimsPage());
            }
            else if (userRole == "HR")
            {
                // Create this button in XAML first (I'll show you how below)
                HRDashboardButton.Visibility = Visibility.Visible;

                // Auto-Navigate to HR Page
                MainFrame.Navigate(new HRViewPage());
            }
        }

        // ==================================================
        // 3. BUTTON CLICKS
        // ==================================================

        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SubmitClaimPage());
        }

        private void MyClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            // If you haven't built this yet, just comment it out
            MainFrame.Navigate(new MyClaimsPage());
        }

        private void ApproveClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ApproveClaimsPage());
        }

        private void HRDashboardButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HRViewPage());
        }

        // OPTIONAL: Logout Button Logic
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AppSession.CurrentUser = null; // Clear session
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}