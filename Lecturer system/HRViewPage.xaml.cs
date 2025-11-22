using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore; // Needed for .Include
using Lecturer_system.Data;
using Lecturer_system.Models;

namespace Lecturer_system
{
    public partial class HRViewPage : Page
    {
        public HRViewPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadApprovedClaims();
        }

        private void LoadApprovedClaims()
        {
            using (var context = new AppDbContext())
            {
                // Show only claims that have been APPROVED
                var approvedClaims = context.Claims
                    .Include(c => c.User)
                    .Where(c => c.Status == "Approved")
                    .ToList();

                ApprovedClaimsGrid.ItemsSource = approvedClaims;
            }
        }

        private void GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int claimId)
            {
                GenerateTextInvoice(claimId);
            }
        }

        private void GenerateTextInvoice(int claimId)
        {
            using (var context = new AppDbContext())
            {
                var claim = context.Claims.Include(c => c.User).FirstOrDefault(c => c.ClaimId == claimId);
                if (claim != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Text File|*.txt", FileName = $"Invoice_{claimId}.txt" };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("INVOICE");
                        sb.AppendLine($"Lecturer: {claim.User.Name}"); // Ensure User model has 'Name' or use FirstName + LastName
                        sb.AppendLine($"Amount: R{claim.TotalAmount}");
                        sb.AppendLine("Status: PAID");

                        File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                        MessageBox.Show("Invoice Generated!");
                    }
                }
            }
        }
    }
}