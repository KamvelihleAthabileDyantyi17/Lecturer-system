using Lecturer_system.Data;
using Lecturer_system.Models;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Lecturer_system
{
    public partial class SubmitClaimPage : Page
    {
        private string _selectedFilePath = null;

        public SubmitClaimPage()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Document Files|*.pdf;*.docx;*.xlsx|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedFilePath = openFileDialog.FileName;
                SelectedFileText.Text = System.IO.Path.GetFileName(_selectedFilePath);
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
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
                    // NOTE: In a real app, you would get the current user's ID after they log in.
                    // We are hard-coding it to 1 for this example.
                    UserId = 1
                };

                context.Claims.Add(newClaim);
                context.SaveChanges();

                MessageBox.Show("Claim submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Optional: Clear the form after submission
                ClaimPeriodPicker.SelectedDate = null;
                HoursWorkedTextBox.Clear();
                HourlyRateTextBox.Clear();
                NotesTextBox.Clear();
                SelectedFileText.Text = " No file selected.";
                _selectedFilePath = null;
            }
        }
    }
}