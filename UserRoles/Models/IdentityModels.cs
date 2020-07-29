using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using EventBook.Models;
using System;

namespace UserRoles.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name ="Name")]
        public string Name { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            //modelBuilder.Entity<Decor>()
            //    .HasOptional(a => a.Category)
            //    .WithMany()
            //    .HasForeignKey(a => a.CategoryID);

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Item> Items { get; set; }
        //public DbSet<ItemsHire> ItemsHires { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Decor> Decors { get; set; }
        public DbSet<ItemsHire> ItemsHires { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}