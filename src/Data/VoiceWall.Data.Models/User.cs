namespace VoiceWall.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VoiceWall.Data.Common.Models;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<Content> contents;
        private ICollection<Comment> comments;
        private ICollection<ContentView> contentViews;
        private ICollection<CommentView> commentViews;

        public User()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;

            this.contents = new HashSet<Content>();
            this.comments = new HashSet<Comment>();
            this.contentViews = new HashSet<ContentView>();
            this.commentViews = new HashSet<CommentView>();
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

        public virtual ICollection<Content> Contents
        {
            get { return this.contents; }
            set { this.contents = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<ContentView> ContentViews
        {
            get { return this.contentViews; }
            set { this.contentViews = value; }
        }

        public virtual ICollection<CommentView> CommentViews
        {
            get { return this.commentViews; }
            set { this.commentViews = value; }
        }

        // IAuditInfo
        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsHidden { get; set; }

        // IDeletableEntity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
