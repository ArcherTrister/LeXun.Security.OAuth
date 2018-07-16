using System;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Client.Models
{
    // Add profile data for application roles by adding properties to the ApplicationRole class
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Is this role will be assigned to new users as default?
        /// </summary>
        public virtual bool IsDefault { get; set; }

        //public ApplicationRole(string roleName) : this() { Name = roleName; }
    }
}
