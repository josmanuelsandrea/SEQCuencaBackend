using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScaneqCuencaBackend.DBModels;

public partial class DbScaniaCuencaContext : DbContext
{
    public DbScaniaCuencaContext()
    {
    }

    public DbScaniaCuencaContext(DbContextOptions<DbScaniaCuencaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql((new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("DB").GetValue<string>("connection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("work_orders_pkey");

            entity.ToTable("work_orders");

            entity.HasIndex(e => e.Fid, "work_orders_fid_key").IsUnique();

            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.Billquantity)
                .HasPrecision(10, 2)
                .HasColumnName("billquantity");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DateField).HasColumnName("date_field");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.Fid).HasColumnName("fid");
            entity.Property(e => e.Isarchived).HasColumnName("isarchived");
            entity.Property(e => e.Iswarranty).HasColumnName("iswarranty");
            entity.Property(e => e.Kilometers).HasColumnName("kilometers");
            entity.Property(e => e.Labourcost)
                .HasPrecision(10, 2)
                .HasColumnName("labourcost");
            entity.Property(e => e.Storedvolume).HasColumnName("storedvolume");

            entity.HasOne(d => d.Customer).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_orders_customer_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
