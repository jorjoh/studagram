﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure.Storage.Table;

namespace StudaGram.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, FirstName + " " + LastName));
            return userIdentity;
        }
    }

    public class UploadedImage : TableEntity
    {
        public UploadedImage(){ }
        private UploadedImage(string partitionKey, string rowKey) : base(partitionKey, rowKey) { }
        
        public static string Images = "Images";
        public string Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime Uploaded { get; set; }
        public int Likes { get; set; }

        public static UploadedImage create() {
            return new UploadedImage("Images", Guid.NewGuid().ToString());
        }   
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static ConnectionStringHandler csh = new ConnectionStringHandler();
        
        public ApplicationDbContext()
            : base(csh.GetAzureSqlConnectionString(), throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}