using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.Models.Auth;
using CARWeb.Shared.Models.CAREntry;
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
        public DbSet<AccessRole> AccessRoles => Set<AccessRole>();

        //CAR ENTRY
        public DbSet<CARHeader> CARHeaders => Set<CARHeader>();
        public DbSet<StandardItem> StandardItems => Set<StandardItem>();
        public DbSet<NonConformityItem> NonConformityItems => Set<NonConformityItem>();
        public DbSet<DetailsOfIssue> DetailsOfIssues => Set<DetailsOfIssue>();
        public DbSet<ImmediateCorrection> ImmediateCorrections => Set<ImmediateCorrection>();
        public DbSet<EliminationNonConformity> EliminationNonConformities => Set<EliminationNonConformity>();
        public DbSet<CorrectiveAction> CorrectiveActions => Set<CorrectiveAction>();
        public DbSet<CorrectiveActionItem> CorrectiveActionItems => Set<CorrectiveActionItem>();
        public DbSet<IMVerification> IMVerifications => Set<IMVerification>();
        public DbSet<FollowUpStatus> FollowUpStatus => Set<FollowUpStatus>();
        public DbSet<StatusOfEffectiveness> StatusOfEffectiveness => Set<StatusOfEffectiveness>();
        public DbSet<ReturnComment> ReturnComments => Set<ReturnComment>();

        //MAINTENANCE
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<DSection> DSections => Set<DSection>();
        public DbSet<Standard> Standards => Set<Standard>();
        public DbSet<CARType> CARTypes => Set<CARType>();
        public DbSet<NonConformity> NonConformities => Set<NonConformity>();
        public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();


    }
}
