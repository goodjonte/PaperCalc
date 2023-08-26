using System.ComponentModel.DataAnnotations;

namespace PaperCalc.DTOs
{
    public class NewUserDTO
    {
        [Display(Name = "User Name: ")]
        public string UserName { get; set; } = default!;

        [Display(Name = "Password: ")]
        public string Password { get; set; } = default!;

        [Display(Name = "Admin Access: ")]
        public bool Admin { get; set; }
        public string? ValidationMessage { get; set; }
    }
}
