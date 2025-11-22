using Lecturer_system.Data;
using Lecturer_system.Models;
using Microsoft.EntityFrameworkCore; // Needed for Include()
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lecturer_system
{
    public partial class ApproveClaimsPage : Page
    {
        // --- 1. FIELD NO LONGER NEEDED ---
        // private readonly int _managerId; 

        // --- 2. CONSTRUCTOR IS NOW PARAMETER-LESS ---
        public ApproveClaimsPage()
        {
            InitializeComponent();
            LoadPendingClaims();
        }

        private void LoadPendingClaims()
        {
            // --- 3. ADD SAFETY CHECK ---
            if (AppSession.CurrentUser == null)
            {
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

            using (var context = new AppDbContext())
            {
                // This query is perfect, it doesn't need the ID.
                var pendingClaims = context.Claims
                                           .Include(c => c.User) // Good! This gets the Lecturer's name
                                           .Where(c => c.Status == "Pending")
                                           .OrderBy(c => c.DateSubmitted)
                                           .ToList();

                PendingClaimsGrid.ItemsSource = pendingClaims;
            }
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int claimId)
            {
                UpdateClaimStatus(claimId, "Approved");
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int claimId)
            {
                UpdateClaimStatus(claimId, "Rejected");
            }
        }

        private void UpdateClaimStatus(int claimId, string newStatus)
        {
            // --- 4. ADD SAFETY CHECK (CRITICAL) ---
            // We must check if the user is logged in *before* we use their ID.
            if (AppSession.CurrentUser == null)
            {
                MessageBox.Show("Error: Session lost. Please log in again.", "Session Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Stop the update
            }

            using (var context = new AppDbContext())
            {
                var claim = context.Claims.Find(claimId);
                if (claim != null)
                {
                    claim.Status = newStatus;

                    var approval = new Approval
                    {
                        ClaimId = claimId,
                        ApprovalStatus = newStatus,
                        ApprovalDate = DateTime.Now,

                        // --- 5. GET MANAGER ID FROM APPSESSION ---
                        ApprovedByUserId = AppSession.CurrentUser.UserId, // <-- USE THE GLOBAL SESSION USER ID

                        Notes = $"Claim {newStatus.ToLower()}."
                    };
                    context.Approvals.Add(approval);

                    context.SaveChanges();

                    MessageBox.Show($"Claim {claimId} has been {newStatus.ToLower()}.", "Status Updated", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadPendingClaims(); // Refresh the list
                }
            }
        }
    }
}