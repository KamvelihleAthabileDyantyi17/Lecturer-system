using System;
using System.Diagnostics; // <--- CRITICAL: Needed to open files
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore; // <--- CRITICAL: Needed for .Include()
using Lecturer_system.Data;
using Lecturer_system.Models;

namespace Lecturer_system
{
    public partial class ApproveClaimsPage : Page
    {
        public ApproveClaimsPage()
        {
            InitializeComponent();
            LoadPendingClaims();
        }

        // 1. LOAD DATA
        private void LoadPendingClaims()
        {
            // Safety Check
            if (AppSession.CurrentUser == null) return;

            using (var context = new AppDbContext())
            {
                var pendingClaims = context.Claims
                                           .Include(c => c.User) // Loads the Lecturer Name
                                           .Where(c => c.Status == "Pending")
                                           .OrderBy(c => c.DateSubmitted)
                                           .ToList();

                // Make sure your DataGrid in XAML is named 'PendingClaimsGrid'
                PendingClaimsGrid.ItemsSource = pendingClaims;
            }
        }

        // 2. VIEW DOCUMENT (The Missing Method)
        private void ViewDocButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("No document attached to this claim.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (File.Exists(filePath))
                {
                    try
                    {
                        // Launches the file (PDF/Word)
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not open file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("File not found. It may have been moved or deleted.", "File Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // 3. APPROVE BUTTON
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int claimId)
            {
                UpdateClaimStatus(claimId, "Approved");
            }
        }

        // 4. REJECT BUTTON
        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int claimId)
            {
                UpdateClaimStatus(claimId, "Rejected");
            }
        }

        // 5. UPDATE DATABASE HELPER
        private void UpdateClaimStatus(int claimId, string newStatus)
        {
            if (AppSession.CurrentUser == null)
            {
                MessageBox.Show("Session lost. Please log in again.");
                return;
            }

            using (var context = new AppDbContext())
            {
                var claim = context.Claims.Find(claimId);
                if (claim != null)
                {
                    claim.Status = newStatus;

                    // Create Approval Record for History
                    var approval = new Approval
                    {
                        ClaimId = claimId,
                        ApprovalStatus = newStatus,
                        ApprovalDate = DateTime.Now,
                        ApprovedByUserId = AppSession.CurrentUser.UserId,
                        Notes = $"Claim {newStatus} by Coordinator."
                    };
                    context.Approvals.Add(approval);

                    context.SaveChanges();

                    MessageBox.Show($"Claim {newStatus} successfully!");
                    LoadPendingClaims(); // Refresh the list
                }
            }
        }
    }
}