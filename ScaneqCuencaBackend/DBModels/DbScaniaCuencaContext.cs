using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScaneqCuencaBackend.DBModels;

public partial class DbScaniaCuencaContext : DbContext
{
    public DbScaniaCuencaContext(DbContextOptions<DbScaniaCuencaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BusOrder> BusOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<TruckOrder> TruckOrders { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bus_orders_pkey");

            entity.ToTable("bus_orders");

            entity.HasIndex(e => e.Fid, "bus_orders_fid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

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

        modelBuilder.Entity<TruckOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("truck_orders_pkey");

            entity.ToTable("truck_orders");

            entity.HasIndex(e => e.Fid, "truck_orders_fid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.TruckOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("truck_orders_customer_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.TruckOrders)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("truck_orders_vehicle_id_fkey");
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
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Plate)
                .HasMaxLength(20)
                .HasColumnName("plate");
            entity.Property(e => e.RearAxleGearRatio)
                .HasPrecision(2, 2)
                .HasColumnName("rear_axle_gear_ratio");
            entity.Property(e => e.Vin)
                .HasMaxLength(255)
                .HasColumnName("vin");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Customer).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicle_customer_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
