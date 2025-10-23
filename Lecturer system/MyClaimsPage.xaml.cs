using Lecturer_system.Data;
using System.Linq;
using System.Windows.Controls;

namespace Lecturer_system
{
    public partial class MyClaimsPage : Page
    {
        public MyClaimsPage()
        {
            InitializeComponent();
            LoadClaims(GetClaimsGrid());
        }

        private DataGrid GetClaimsGrid()
        {
            return ClaimsGrid;
        }

        private void LoadClaims(DataGrid claimsGrid)
        {
            using (var context = new AppDbContext())
            {
                // NOTE: In a real app, you would get the current user's ID after they log in.
                // We are hard-coding it to 1 for this example, same as the submission form.
                int loggedInUserId = 1;

                var claims = context.Claims
                                    .Where(c => c.UserId == loggedInUserId)
                                    .OrderByDescending(c => c.DateSubmitted)
                                    .ToList();
                claimsGrid.ItemsSource = claims;
            }
        }
    }
}