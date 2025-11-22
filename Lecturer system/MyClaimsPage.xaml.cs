using Lecturer_system.Data;
using Lecturer_system.Models; // <-- 1. ADD THIS to access AppSession
using System.Linq;
using System.Windows; // <-- 2. ADD THIS for MessageBox
using System.Windows.Controls;

namespace Lecturer_system
{
    public partial class MyClaimsPage : Page
    {
        // --- 3. FIELD NO LONGER NEEDED ---
        // private readonly int _userId; 

        // --- 4. CONSTRUCTOR IS NOW PARAMETER-LESS ---
        public MyClaimsPage()
        {
            InitializeComponent();
            LoadClaims();
        }

        private void LoadClaims()
        {
            // --- 5. ADD SAFETY CHECK ---
            if (AppSession.CurrentUser == null)
            {
                // This should rarely happen, but it's safe to check.
                MessageBox.Show("Error: You are not logged in. Returning to login screen.", "Session Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Navigate back to login
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    LoginWindow login = new LoginWindow();
                    login.Show();
                    mainWindow.Close();
                }
                return;
            }

            // --- 6. GET USER ID FROM APPSESSION ---
            int loggedInUserId = AppSession.CurrentUser.UserId; // <-- USE THE GLOBAL SESSION USER ID

            using (var context = new AppDbContext())
            {
                var claims = context.Claims
                                     .Where(c => c.UserId == loggedInUserId) // This line now works perfectly
                                     .OrderByDescending(c => c.DateSubmitted)
                                     .ToList();

                ClaimsGrid.ItemsSource = claims;
            }
        }
    }
}