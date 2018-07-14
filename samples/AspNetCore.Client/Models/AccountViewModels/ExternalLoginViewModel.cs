using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Client.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
