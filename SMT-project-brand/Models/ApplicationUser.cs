using Microsoft.AspNetCore.Identity;

namespace SMT_project_brand.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeId { get; set; } = string.Empty;

        public string? FullName { get; set; }
    }
}