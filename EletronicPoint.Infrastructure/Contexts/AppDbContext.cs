using EletronicPoint.Application.Services.Interfaces;
using EletronicPoint.Domain.Entities;
using EletronicPoint.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace EletronicPoint.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUser;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<WorkShift> WorkShifts => Set<WorkShift>();
        public DbSet<WorkShiftPeriod> WorkShiftPeriods => Set<WorkShiftPeriod>();
        public DbSet<TimeSheet> TimeSheets => Set<TimeSheet>();
        public DbSet<TimeSheetEntry> TimeSheetEntries => Set<TimeSheetEntry>();
        public DbSet<OvertimeRequest> OvertimeRequests => Set<OvertimeRequest>();
        public DbSet<AdjustmentRequest> AdjustmentRequests => Set<AdjustmentRequest>();
        public DbSet<AdjustmentRequestEntry> AdjustmentRequestEntries => Set<AdjustmentRequestEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUserEntity(modelBuilder);
            ConfigureWorkShiftEntity(modelBuilder);
            ConfigureWorkShiftPeriodEntity(modelBuilder);
            ConfigureTimeSheetEntity(modelBuilder);
            ConfigureTimeSheetEntryEntity(modelBuilder);
            ConfigureOvertimeRequestEntity(modelBuilder);
            ConfigureAdjustmentRequestEntity(modelBuilder);
            ConfigureAdjustmentRequestEntryEntity(modelBuilder);

            ConfigureModel(modelBuilder);
        }

        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("Id")
                    .ValueGeneratedOnAdd();

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.TenantId))
                    .IsRequired();

                modelBuilder.Entity(entityType.ClrType)
                    .HasIndex(nameof(BaseEntity.TenantId));

                ApplySoftDeleteQueryFilter(modelBuilder, entityType.ClrType);
                ApplyTenantIdQueryFilter(modelBuilder, entityType.ClrType);
            }
        }

        private void ApplySoftDeleteQueryFilter(ModelBuilder modelBuilder, Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, "DeletedAt");
            var nullConstant = Expression.Constant(null, typeof(DateTime?));
            var condition = Expression.Equal(property, nullConstant);
            var lambda = Expression.Lambda(condition, parameter);

            modelBuilder.Entity(entityType).HasQueryFilter(lambda);
        }

        private void ApplyTenantIdQueryFilter(ModelBuilder modelBuilder, Type entityType)
        {
            var param = Expression.Parameter(entityType, "e");
            var tenantProperty = Expression.Property(param, nameof(BaseEntity.TenantId));
            var tenantId = Expression.Constant(_currentUser.TenantId);
            var body = Expression.Equal(tenantProperty, tenantId);
            var lambda = Expression.Lambda(body, param);

            modelBuilder.Entity(entityType).HasQueryFilter(lambda);
        }

        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.Role)
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.HasIndex(u => new { u.TenantId, u.Email })
                    .IsUnique();

                entity.HasIndex(u => u.Role);
                entity.HasIndex(u => u.Name);

                entity.HasOne(u => u.WorkShift)
                    .WithMany(w => w.Users)
                    .HasForeignKey(u => u.WorkShiftId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureWorkShiftEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.ToTable("WorkShifts");

                entity.Property(w => w.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(w => w.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.BreakMinutes)
                    .HasDefaultValue(0);

                entity.HasMany(w => w.Periods)
                    .WithOne(p => p.WorkShift)
                    .HasForeignKey(p => p.WorkShiftId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(w => w.Users)
                    .WithOne(u => u.WorkShift)
                    .HasForeignKey(u => u.WorkShiftId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureWorkShiftPeriodEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkShiftPeriod>(entity =>
            {
                entity.ToTable("WorkShiftPeriods");

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.StartTime)
                    .IsRequired();

                entity.Property(p => p.EndTime)
                    .IsRequired();

                entity.Property(p => p.Order)
                    .IsRequired();

                entity.HasIndex(p => new { p.WorkShiftId, p.Order });

                entity.HasOne(p => p.WorkShift)
                    .WithMany(w => w.Periods)
                    .HasForeignKey(p => p.WorkShiftId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Entries)
                    .WithOne(e => e.WorkShiftPeriod)
                    .HasForeignKey(p => p.WorkShiftPeriodId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.AdjustmentEntries)
                    .WithOne(e => e.WorkShiftPeriod)
                    .HasForeignKey(p => p.WorkShiftPeriodId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureTimeSheetEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.ToTable("TimeSheets");

                entity.Property(t => t.Date)
                    .IsRequired();

                entity.Property(t => t.TotalHours)
                    .HasPrecision(4, 2);

                entity.HasIndex(t => new { t.UserId, t.Date })
                    .IsUnique();

                entity.HasOne(t => t.User)
                    .WithMany(u => u.TimeSheets)
                    .HasForeignKey(t => t.UserId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(t => t.Entries)
                    .WithOne(e => e.TimeSheet)
                    .HasForeignKey(e => e.TimeSheetId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureTimeSheetEntryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeSheetEntry>(entity =>
            {
                entity.ToTable("TimeSheetEntries");

                entity.Property(e => e.EntryTime)
                    .IsRequired();

                entity.Property(e => e.EntryDate)
                    .IsRequired();

                entity.Property(e => e.EntryType)
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.HasIndex(e => new { e.TimeSheetId, e.EntryTime });
                entity.HasIndex(e => e.WorkShiftPeriodId);
                entity.HasIndex(e => e.EntryType);

                entity.HasOne(e => e.WorkShiftPeriod)
                    .WithMany(w => w.Entries)
                    .HasForeignKey(e => e.WorkShiftPeriodId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.TimeSheet)
                    .WithMany(t => t.Entries)
                    .HasForeignKey(e => e.TimeSheetId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureOvertimeRequestEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OvertimeRequest>(entity =>
            {
                entity.ToTable("OvertimeRequests");

                entity.Property(e => e.RequestDate)
                   .IsRequired();

                entity.Property(e => e.OvertimeDate)
                   .IsRequired();

                entity.Property(e => e.StartTime)
                   .IsRequired();

                entity.Property(e => e.EndTime)
                   .IsRequired();

                entity.Property(o => o.Reason)
                    .HasMaxLength(500);

                entity.Property(o => o.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.Property(e => e.ApprovalDate);
                entity.Property(e => e.ApprovedById);

                entity.HasIndex(o => o.UserId);
                entity.HasIndex(o => new { o.UserId, o.OvertimeDate });
                entity.HasIndex(o => new { o.UserId, o.RequestDate });
                entity.HasIndex(o => new { o.UserId, o.StartTime });
                entity.HasIndex(o => new { o.UserId, o.EndTime });
                entity.HasIndex(o => o.Status);

                entity.HasOne(o => o.User)
                    .WithMany(u => u.OvertimeRequests)
                    .HasForeignKey(o => o.UserId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureAdjustmentRequestEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdjustmentRequest>(entity =>
            {
                entity.ToTable("AdjustmentRequests");

                entity.Property(e => e.RequestDate)
                   .IsRequired();

                entity.Property(e => e.AdjustmentDate)
                   .IsRequired();

                entity.Property(a => a.Reason)
                    .HasMaxLength(500);

                entity.Property(a => a.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.Property(e => e.ApprovalDate);
                entity.Property(e => e.ApprovedById);

                entity.HasIndex(a => a.UserId);
                entity.HasIndex(a => new { a.UserId, a.RequestDate });
                entity.HasIndex(a => new { a.UserId, a.AdjustmentDate });
                entity.HasIndex(a => a.Status);

                entity.HasOne(a => a.User)
                    .WithMany(u => u.AdjustmentRequests)
                    .HasForeignKey(a => a.UserId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(t => t.Entries)
                    .WithOne(a => a.Adjustment)
                    .HasForeignKey(e => e.AdjustmentId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureAdjustmentRequestEntryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdjustmentRequestEntry>(entity =>
            {
                entity.ToTable("AdjustmentRequestEntries");
                       
                entity.Property(e => e.EntryTime)
                    .IsRequired();

                entity.Property(e => e.EntryDate)
                    .IsRequired();
                
                entity.HasOne(e => e.WorkShiftPeriod)
                   .WithMany(w => w.AdjustmentEntries)
                   .HasForeignKey(e => e.WorkShiftPeriodId)
                   .HasConstraintName(null)
                   .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Adjustment)
                    .WithMany(t => t.Entries)
                    .HasForeignKey(e => e.AdjustmentId)
                    .HasConstraintName(null)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            SetTenantIds();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            SetTenantIds();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetTenantIds()
        {
            var tenantId = _currentUser.TenantId;
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.TenantId = tenantId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(BaseEntity.TenantId)).IsModified = false;
                }
            }
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.DeletedAt = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted && entry.Entity is BaseEntity softDeleteEntity)
                {
                    entry.State = EntityState.Modified;
                    softDeleteEntity.DeletedAt = DateTime.UtcNow;
                    softDeleteEntity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}