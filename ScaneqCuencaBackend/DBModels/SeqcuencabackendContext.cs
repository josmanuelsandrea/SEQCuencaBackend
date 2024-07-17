using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace ScaneqCuencaBackend.DBModels;

public partial class SeqcuencabackendContext : DbContext
{
    public SeqcuencabackendContext()
    {
    }

    public SeqcuencabackendContext(DbContextOptions<SeqcuencabackendContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BusOrder> BusOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<MaintenanceRegistry> MaintenanceRegistries { get; set; }

    public virtual DbSet<Mechanic> Mechanics { get; set; }

    public virtual DbSet<MechanicsOrder> MechanicsOrders { get; set; }

    public virtual DbSet<Notice> Notices { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=seqcuencabackend;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bus_orders_pkey");

            entity.ToTable("bus_orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DateField).HasColumnName("date_field");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.Fid).HasColumnName("fid");
            entity.Property(e => e.Isarchived).HasColumnName("isarchived");
            entity.Property(e => e.Iswarranty).HasColumnName("iswarranty");
            entity.Property(e => e.Kilometers).HasColumnName("kilometers");
            entity.Property(e => e.Storedvolume).HasColumnName("storedvolume");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.VehicleType)
                .HasMaxLength(255)
                .HasDefaultValueSql("'bus'::character varying")
                .HasColumnName("vehicle_type");

            entity.HasOne(d => d.Customer).WithMany(p => p.BusOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bus_orders_customer_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BusOrders)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("bus_orders_vehicle_id_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pk");

            entity.ToTable("customers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MaintenanceRegistry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("maintenance_registry_pkey");

            entity.ToTable("maintenance_registry");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Kilometers).HasColumnName("kilometers");
            entity.Property(e => e.MaintenanceDate).HasColumnName("maintenance_date");
            entity.Property(e => e.MaintenanceType)
                .HasMaxLength(50)
                .HasColumnName("maintenance_type");
            entity.Property(e => e.OrderFkId).HasColumnName("order_fk_id");
            entity.Property(e => e.VehicleFkId).HasColumnName("vehicle_fk_id");

            entity.HasOne(d => d.OrderFk).WithMany(p => p.MaintenanceRegistries)
                .HasForeignKey(d => d.OrderFkId)
                .HasConstraintName("maintenance_registry_order_fk_id_fkey");

            entity.HasOne(d => d.VehicleFk).WithMany(p => p.MaintenanceRegistries)
                .HasForeignKey(d => d.VehicleFkId)
                .HasConstraintName("maintenance_registry_vehicle_fk_id_fkey");
        });

        modelBuilder.Entity<Mechanic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mechanics_pkey");

            entity.ToTable("mechanics");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<MechanicsOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mechanics_orders_pkey");

            entity.ToTable("mechanics_orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
            entity.Property(e => e.WorkOrderId).HasColumnName("work_order_id");
            entity.Property(e => e.WorkOrderType)
                .HasMaxLength(255)
                .HasColumnName("work_order_type");

            entity.HasOne(d => d.Mechanic).WithMany(p => p.MechanicsOrders)
                .HasForeignKey(d => d.MechanicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mechanics_orders_mechanic_id_fkey");
        });

        modelBuilder.Entity<Notice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notices_pkey");

            entity.ToTable("notices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.NoticeDate).HasColumnName("notice_date");
            entity.Property(e => e.Resolved)
                .HasDefaultValue(false)
                .HasColumnName("resolved");
            entity.Property(e => e.Severity)
                .HasMaxLength(50)
                .HasColumnName("severity");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Notices)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notices_vehicle_id_fkey");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_pkey");

            entity.ToTable("vehicle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AxleGear)
                .HasMaxLength(255)
                .HasColumnName("axle_gear");
            entity.Property(e => e.Color)
                .HasMaxLength(255)
                .HasColumnName("color");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Engine)
                .HasMaxLength(255)
                .HasColumnName("engine");
            entity.Property(e => e.Gearbox)
                .HasMaxLength(255)
                .HasColumnName("gearbox");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .HasColumnName("model");
            entity.Property(e => e.Plate)
                .HasMaxLength(20)
                .HasColumnName("plate");
            entity.Property(e => e.RearAxleGearRatio)
                .HasPrecision(2, 2)
                .HasColumnName("rear_axle_gear_ratio");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.Vin)
                .HasMaxLength(255)
                .HasColumnName("vin");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Customer).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("vehicle_customer_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
