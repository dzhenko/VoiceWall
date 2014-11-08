namespace VoiceWall.Data.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VoiceWall.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [MaxLength(100)]
        [MinLength(2)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        [Required]
        public string LastName { get; set; }

        public string UserImage { get; set; }

        // IAuditInfo
        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // IDeletableEntity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
