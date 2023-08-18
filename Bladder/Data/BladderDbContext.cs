using Bladder.Entities;
using Bladder.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Bladder.Data;

public class BladderDbContext : AbpDbContext<BladderDbContext>
{
    public BladderDbContext(DbContextOptions<BladderDbContext> options)
        : base(options)
    {
    }

    public DbSet<BuildingMachine> Machines { get; set; }
    public DbSet<BuildingBladder> Bladders { get; set; }
    public DbSet<Finding> Finding { get; set; }
    public DbSet<BladderTransaction> BladderTransactions { get; set; }
    public DbSet<MaintenanceFinding> MaintenanceFindings { get; set; }
    public DbSet<BladderSize> BladderSizes { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<MaintenanceFinding>()
            .HasKey(mf => new { mf.MaintenanceTransactionId, mf.FindingId });

        builder.Entity<MaintenanceFinding>()
            .HasOne(mf => mf.MaintenanceTransaction)
            .WithMany(mt => mt.MaintenanceFindings)
            .HasForeignKey(mf => mf.MaintenanceTransactionId);

        builder.Entity<MaintenanceFinding>()
            .HasOne(mf => mf.Finding)
            .WithMany(f => f.MaintenanceFindings)
            .HasForeignKey(mf => mf.FindingId);

        /* Configure your own entities here */
        builder.Entity<MountTransaction>().ToTable("BladderTransactions");
        builder.Entity<DismountTransaction>().ToTable("BladderTransactions");
        builder.Entity<MaintenanceTransaction>().ToTable("BladderTransactions");
        builder.Entity<TestTransaction>().ToTable("BladderTransactions");


    }
}
