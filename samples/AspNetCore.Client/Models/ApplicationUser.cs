using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Client.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    //public class ApplicationUser : IdentityUser
    //{
    //}
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser() { }

        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString("N");
            //this.ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Roles of this user.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<ApplicationUserRole> Roles { get; set; }

        //public ApplicationUser(string name) : this() { UserName = name; }
    }
}
