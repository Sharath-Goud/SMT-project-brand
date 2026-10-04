using System.ComponentModel.DataAnnotations;

namespace SMT_project_brand.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee ID")]
        [StringLength(
            50,
            ErrorMessage = "Employee ID cannot exceed 50 characters")]
        public string EmployeeId { get; set; } = string.Empty;


        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Employee Name")]
        [StringLength(
            100,
            ErrorMessage = "Employee Name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(
            100,
            MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare(
            "Password",
            ErrorMessage = "Password and Confirm Password do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}