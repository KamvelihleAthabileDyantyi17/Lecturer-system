using System.Windows;
using System.Windows.Controls;
using Lecturer_system.Models; // <-- 1. ADD THIS to access AppSession

namespace Lecturer_system
{
    public partial class MainWindow : Window
    {
        // --- 3. FIELDS ARE NO LONGER NEEDED ---
        // We will read directly from AppSession.CurrentUser
        // private readonly string _userRole;
        // private readonly int _loggedInUserId;

        // --- 4. CONSTRUCTOR IS NOW PARAMETER-LESS ---
        public MainWindow()
        {
            InitializeComponent();

            // --- 5. ADD SAFETY CHECK ---
            // What if someone runs this window without logging in?
            if (AppSession.CurrentUser == null)
            {
                // No user is logged in. Go back to login screen.
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close(); // Close this (broken) MainWindow
                return; // Stop running the rest of the code
            }

            // If we are here, a user *is* logged in.
            SetupUIForRole();
        }

        private void SetupUIForRole()
        {
            // --- 6. READ ROLE FROM APPSESSION ---
            string role = AppSession.CurrentUser.Role;

            if (role == "Lecturer")
            {
                SubmitClaimButton.Visibility = Visibility.Visible;
                MyClaimsButton.Visibility = Visibility.Visible;
                ApproveClaimsButton.Visibility = Visibility.Collapsed;

                // Navigate to a default page for the Lecturer
                MainFrame.Navigate(new MyClaimsPage());
            }
            else if (role == "Manager") // <-- You used "Manager", so I'll use it too.
            {
                SubmitClaimButton.Visibility = Visibility.Collapsed;
                MyClaimsButton.Visibility = Visibility.Collapsed;
                ApproveClaimsButton.Visibility = Visibility.Visible;

                // Navigate to a default page for the Manager
                MainFrame.Navigate(new ApproveClaimsPage());
            }
        }

        // --- 7. UPDATE BUTTON CLICKS ---
        // The pages (SubmitClaimPage, etc.) will get the User ID
        // from the AppSession themselves, so we don't pass any parameters.

        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SubmitClaimPage()); // No parameter needed
        }

        private void MyClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MyClaimsPage()); // No parameter needed
        }

        private void ApproveClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ApproveClaimsPage()); // No parameter needed
        }
    }
}