using Lecturer_system.Data;
using Lecturer_system.Models;
using Microsoft.Win32;
using System;
using System.IO; // <-- 1. ADD THIS for file extension checking
using System.Linq; // <-- 2. ADD THIS for checking the extension
using System.Windows;
using System.Windows.Controls;

namespace Lecturer_system
{
    public partial class SubmitClaimPage : Page
    {
        private string _selectedFilePath = null;
        // --- 3. FIELD NO LONGER NEEDED ---
        // private readonly int _userId; // <-- We will get this from AppSession

        // --- 4. CONSTRUCTOR IS NOW PARAMETER-LESS ---
        public SubmitClaimPage()
        {
            InitializeComponent();
            // We no longer get the userId here
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                // Updated filter to be more specific to documents
                Filter = "Document Files|*.pdf;*.doc;*.docx;*.txt;*.rtf|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // --- 5. ADD FILE VALIDATION LOGIC (Lecturer's Requirement) ---
                string extension = Path.GetExtension(selectedFilePath).ToLower();

                // Define allowed extensions
                var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".txt", ".rtf" }; // Add any other doc types

                if (allowedExtensions.Contains(extension))
                {
                    // The file is a valid document. Proceed.
                    _selectedFilePath = selectedFilePath;
                    SelectedFileText.Text = Path.GetFileName(_selectedFilePath);
                }
                else
                {
                    // The file is NOT a document. Show an error.
                    MessageBox.Show("Invalid file type. Please select a document (.pdf, .doc, .docx, etc.).",
                                    "Upload Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    // Clear the invalid selection
                    _selectedFilePath = null;
                    SelectedFileText.Text = " No file selected.";
                }
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // --- 6. ADD SAFETY CHECK ---
            // What if the user session is lost?
            if (AppSession.CurrentUser == null)
            {
                MessageBox.Show("Error: You are not logged in. Returning to login screen.", "Session Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // This is a bit advanced, but it's the 'right' way to handle this.
                // It finds the main window and tells it to go back to Login.
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    LoginWindow login = new LoginWindow();
                    login.Show();
                    mainWindow.Close();
                }
                return;
            }

            // --- Input Validation ---
            if (ClaimPeriodPicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a claim period.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(HoursWorkedTextBox.Text, out decimal hours) || !decimal.TryParse(HourlyRateTextBox.Text, out decimal rate))
            {
                MessageBox.Show("Please enter valid numbers for hours and rate.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // --- Save to Database ---
            using (var context = new AppDbContext())
            {
                var newClaim = new Claim
                {
                    ClaimPeriod = ClaimPeriodPicker.SelectedDate.Value,
                    HoursWorked = hours,
                    HourlyRate = rate,
                    TotalAmount = hours * rate,
                    Status = "Pending",
                    DateSubmitted = DateTime.Now,
                    SupportingDocPath = _selectedFilePath,

                    // --- 7. GET USER ID FROM APPSESSION ---
                    UserId = AppSession.CurrentUser.UserId // <-- USE THE GLOBAL SESSION USER ID
                };

                context.Claims.Add(newClaim);
                context.SaveChanges();

                MessageBox.Show("Claim submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Optional: Clear the form after submission
                ClaimPeriodPicker.SelectedDate = null;
                HoursWorkedTextBox.Clear();
                HourlyRateTextBox.Clear();
                // NotesTextBox.Clear(); // You have a NotesTextBox in your XAML? If so, uncomment this.
                SelectedFileText.Text = " No file selected.";
                _selectedFilePath = null;
            }
        }
    }
}