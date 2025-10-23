using System.Linq;
using System.Windows;
using Lecturer_system.Data;

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
                string password = PasswordBox.Password; // In a real app, hash this password

                // Find user by email
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                // For simplicity, we are not checking the password hash here.
                // In a real application, you would compare the hashed password.
                if (user != null)
                {
                    // User found, open the main window
                    MainWindow mainWindow = new MainWindow(); // This line will show an error for now
                    mainWindow.Show();
                    this.Close(); // Close the login window
                }
                else
                {
                    // User not found
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}