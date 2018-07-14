using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Client.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
