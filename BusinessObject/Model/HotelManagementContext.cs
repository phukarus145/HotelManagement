﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BusinessObject.Model
{
    public partial class HotelManagementContext : DbContext
    {
        public HotelManagementContext()
        {
        }

        public HotelManagementContext(DbContextOptions<HotelManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<BookRoom> BookRoom { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomInBooking> RoomInBooking { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceInRoom> ServiceInRoom { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using IConfiguration to get information from json file.
            optionsBuilder.UseSqlServer("Server=MSI;Database=HotelManagement;Uid=sa;Pwd=123456789;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Avartar).HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsEmployee).HasColumnName("isEmployee");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<BookRoom>(entity =>
            {
                entity.Property(e => e.Cmnd)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("CMND");

                entity.Property(e => e.CouponId).HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.StarTime).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BookRoom)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_BookRoom_Account");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.BookRoom)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_BookRoom_Coupon");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(4000);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK_Room_RoomType");
            });

            modelBuilder.Entity<RoomInBooking>(entity =>
            {
                entity.HasKey(e => new { e.BookRoomId, e.RoomId });

                entity.HasOne(d => d.BookRoom)
                    .WithMany(p => p.RoomInBooking)
                    .HasForeignKey(d => d.BookRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomInBooking_BookRoom");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomInBooking)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomInBooking_Room");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_Service_ServiceType");
            });

            modelBuilder.Entity<ServiceInRoom>(entity =>
            {
                entity.HasKey(e => new { e.BookRoomId, e.ServiceId });

                entity.HasOne(d => d.BookRoom)
                    .WithMany(p => p.ServiceInRoom)
                    .HasForeignKey(d => d.BookRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceInRoom_BookRoom");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceInRoom)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceInRoom_Service");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}