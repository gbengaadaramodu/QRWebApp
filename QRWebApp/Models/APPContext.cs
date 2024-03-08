using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security;

namespace QRWebApp.Models
{
    public class APPContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public APPContext(DbContextOptions<APPContext> options) : base(options)
        {
            //Database.Migrate();

        }

        public APPContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();
            // optionsBuilder.UseSqlServer(connect);
            optionsBuilder.UseSqlServer(connect);

        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        //public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        //public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        //public DbSet<ApplicationUserPasswordHistory> ApplicationUserPasswordHistorys { get; set; }
        //public DbSet<Permission> Permissions { get; set; }
        //public DbSet<RolePermission> RolePermissions { get; set; }
        //public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        //public DbSet<EmailLog> EmailLogs { get; set; }
        //public DbSet<EmailTemplate> EmailTemplates { get; set; }
        //public DbSet<EmailAttachment> EmailAttachments { get; set; }
        //public DbSet<ErrorLog> ErrorLogs { get; set; }
        //// public DbSet<Application> Application { get; set; }
        //public DbSet<PortalVersion> PortalVersion { get; set; }
        //// more tables 
        //public DbSet<SystemSetting> SystemSetting { get; set; }
        //public DbSet<EmailCredential> EmailCredential { get; set; }

        ////Asset Tables
        //public DbSet<CompanyAsset> CompanyAsset { get; set; }
        //public DbSet<Department> Department { get; set; }
        //public DbSet<Category> Category { get; set; }

        ////Logistics Tables
        //public DbSet<Shippment> Shippments { get; set; }
        //public DbSet<Route> Routes { get; set; }
        //public DbSet<Package> Packages { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<PaymentLog> PaymentLogs { get; set; }

        //public DbSet<Country> Countries { get; set; }
        //public DbSet<State> States { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<ShippingModel> ShippingModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Course>().HasData(new Course
            //{
            //    Id = 1,
            //    CourseCode = "MTH101",
            //    CourseTitle = "Introduction to Algebra",
            //    CourseUnit = 2,
            //    CourseStatus = "E",
            //}); A

            //builder.Seed();

            //builder.Ignore<ApplicationUserLogin>();
            ////  builder.Ignore<ApplicationUserRole>();
            //builder.Ignore<UserLoginHistory>();

            //builder.Entity<ApplicationUser>(b =>
            //{
            //    b.HasKey(x => x.Id);
            //    b.Property(x => x.Id).ValueGeneratedOnAdd();
            //    b.HasMany(x => x.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            //});

            //builder.Entity<ApplicationUserRole>(userRole =>
            //{
            //    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            //    userRole.HasOne(ur => ur.Role)
            //        .WithMany(r => r.Users)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();

            //    userRole.HasOne(ur => ur.User)
            //        .WithMany(r => r.Roles)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();
            //});

            //  builder.Entity<ApplicationUserRole>().HasNoKey();
            //  base.OnModelCreating(builder);
        }
    }
}
