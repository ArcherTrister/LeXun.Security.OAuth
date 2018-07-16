using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Client.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUserRole()
            : base()
        { }
    }
}
