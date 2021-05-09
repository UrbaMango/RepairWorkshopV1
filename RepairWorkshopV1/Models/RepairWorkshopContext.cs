using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepairWorkshopV1.Models
{
    public partial class RepairWorkshopContext : DbContext
    {
        public RepairWorkshopContext(DbContextOptions<RepairWorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersEmp> UsersEmp { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
        public virtual DbSet<VisitTasks> VisitTasks { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("PK_clients");

                entity.HasIndex(e => e.UserId)
                    .HasName("fkIdx_61");

                entity.Property(e => e.ClientId)
                    .HasColumnName("Client_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("Client_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyAccount).HasColumnName("Company_Account");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.RegistrationCode)
                    .HasColumnName("Registration_Code")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_60");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_employees");

                entity.HasIndex(e => e.UserId)
                    .HasName("fkIdx_72");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("Employee_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasColumnName("Account_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Standart')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_71");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_users");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersEmp>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_users_clone");

                entity.ToTable("Users_emp");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicles>(entity =>
            {
                entity.HasKey(e => e.VehicleId)
                    .HasName("PK_vehicle");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fkIdx_45");

                entity.Property(e => e.VehicleId)
                    .HasColumnName("Vehicle_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClientId)
                    .HasColumnName("Client_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_44");
            });

            modelBuilder.Entity<VisitTasks>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK_visit_tasks");

                entity.ToTable("Visit_tasks");

                entity.HasIndex(e => e.VisitId)
                    .HasName("fkIdx_84");

                entity.Property(e => e.TaskId)
                    .HasColumnName("Task_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("Employee_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OilChange).HasColumnName("Oil_change");

                entity.Property(e => e.VisitId)
                    .HasColumnName("Visit_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitTasks)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_83");
            });

            modelBuilder.Entity<Visits>(entity =>
            {
                entity.HasKey(e => e.VisitId)
                    .HasName("PK_visits");

                entity.HasIndex(e => e.VehicleId)
                    .HasName("fkIdx_67");

                entity.Property(e => e.VisitId)
                    .HasColumnName("Visit_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClientId)
                    .HasColumnName("Client_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Mileage).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Notes)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Progress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleId)
                    .HasColumnName("Vehicle_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.VisitEndDate)
                    .HasColumnName("Visit_end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VisitPrice)
                    .HasColumnName("Visit_Price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VisitStartDate)
                    .HasColumnName("Visit_start_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_66");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
