﻿using Microsoft.EntityFrameworkCore;
using System;

namespace P01_HospitalDatabase
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        { }
        
        public HospitalContext(DbContextOptions options)
            :base(options)
        { }
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(50);

                entity.Property(e => e.LastName)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(50);

                entity.Property(e => e.Address)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(250);

                entity.Property(e => e.Email)
               .IsRequired()
               .IsUnicode(false)
               .HasMaxLength(80);

                entity.Property(e => e.HasInsurance)
                .HasDefaultValue(true);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

                entity.Property(e => e.Specialty)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                .IsRequired(false)
                .HasMaxLength(250)
                .IsUnicode();

                entity.Property(e => e.DoctorId)
                .IsRequired(false);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(e => e.PatientId);

                entity.HasOne(e => e.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(s => s.DoctorId);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(e => e.Comments)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                .WithMany(e => e.Diagnoses)
                .HasForeignKey(e => e.PatientId);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.PatientId,
                    e.MedicamentId
                });

                entity.HasOne(e => e.Medicament)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.PatientId);
            });
        }
    }
}
