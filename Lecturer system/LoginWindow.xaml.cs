using System.Linq;
using System.Windows;
using Lecturer_system.Data;
using Lecturer_system.Models; // <-- 1. ADD THIS to access AppSession

namespace Lecturer_system
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;

                // Find user by email
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                // --- START OF CHANGES ---

                // 2. CHECK THE PASSWORD.
                // We'll assume you stored plain text for testing.
                // WARNING: This is NOT secure. You must use a HASH in your real project.
                // But for now, this will work:
                if (user != null && user.PasswordHash == password)
                {
                    // 3. SAVE THE *ENTIRE* USER TO THE SESSION
                    // This is the most important part.
                    AppSession.CurrentUser = user;

                    // 4. OPEN THE MAIN WINDOW (NOW WITH *NO* PARAMETERS)
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close(); // Close the login window
                }
                else
                {
                    // User not found or password incorrect
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // --- END OF CHANGES ---
            }
        }
    }
}