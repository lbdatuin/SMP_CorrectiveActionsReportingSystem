using CARWeb.Shared.Models.Auth;
using CARWeb.Shared.Models.CARLabel;
using CARWeb.Shared.Models.DepartmentSection;
using Microsoft.EntityFrameworkCore;

namespace CARWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        //USERS
        public DbSet<User> Users => Set<User>();
        public DbSet<UserDetails> UserDetails => Set<UserDetails>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        //MAINTENANCE
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<DSection> DSections => Set<DSection>();
        public DbSet<Standard> Standards => Set<Standard>();
        public DbSet<CARType> CARTypes => Set<CARType>();
        public DbSet<NonConformity> NonConformities => Set<NonConformity>();
        public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();


    }
}
