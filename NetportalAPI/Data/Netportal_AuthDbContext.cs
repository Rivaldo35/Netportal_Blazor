using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetportalAPI.Data
{
    public partial class Netportal_AuthDbContext : DbContext
    {
        public Netportal_AuthDbContext()
        {
        }

        public Netportal_AuthDbContext(DbContextOptions<Netportal_AuthDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicatie> Applicaties { get; set; } = null!;
        public virtual DbSet<Auditlog> Auditlogs { get; set; } = null!;
        public virtual DbSet<Instelling> Instellings { get; set; } = null!;
        public virtual DbSet<InstellingProfile> InstellingProfiles { get; set; } = null!;
        public virtual DbSet<InstellingType> InstellingTypes { get; set; } = null!;
        public virtual DbSet<ResetPassword> ResetPasswords { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<UserRol> UserRols { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
                var conString = config.GetConnectionString("np");
                optionsBuilder.UseSqlServer(conString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicatie>(entity =>
            {
                entity.ToTable("Applicatie");

                entity.Property(e => e.ApplicatieId).HasColumnName("applicatie_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("naam");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("omschrijving");
            });

            modelBuilder.Entity<Auditlog>(entity =>
            {
                entity.ToTable("Auditlog");

                entity.Property(e => e.AuditlogId).HasColumnName("auditlog_id");

                entity.Property(e => e.Actie)
                    .IsUnicode(false)
                    .HasColumnName("actie");

                entity.Property(e => e.ApplicatieId).HasColumnName("applicatie_id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.InstellingId).HasColumnName("instelling_id");

                entity.Property(e => e.RapportageId).HasColumnName("rapportage_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Instelling>(entity =>
            {
                entity.ToTable("Instelling");

                entity.Property(e => e.InstellingId).HasColumnName("instelling_id");

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adres");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.DatumOpgeheven)
                    .HasColumnType("date")
                    .HasColumnName("datum_opgeheven");

                entity.Property(e => e.DatumOpgericht)
                    .HasColumnType("date")
                    .HasColumnName("datum_opgericht");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Kkfnr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kkfnr");

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("naam");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("omschrijving");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.SwiftCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Swift_code");

                entity.Property(e => e.Telnr1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telnr_1");

                entity.Property(e => e.Telnr2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telnr_2");
            });

            modelBuilder.Entity<InstellingProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.ToTable("Instelling_Profile");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.InstellingId).HasColumnName("instelling_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Instelling)
                    .WithMany(p => p.InstellingProfiles)
                    .HasForeignKey(d => d.InstellingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instelling_Profile_Instelling");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.InstellingProfiles)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instelling_Profile_Instelling_Type");
            });

            modelBuilder.Entity<InstellingType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("Instelling_Type");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("omschrijving");
            });

            modelBuilder.Entity<ResetPassword>(entity =>
            {
                entity.ToTable("Reset_Password");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "IX_User")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Achternaam)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("achternaam");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FailedAttempts).HasColumnName("failed_attempts");

                entity.Property(e => e.InstellingId).HasColumnName("instelling_id");

                entity.Property(e => e.InternalUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("internal_user");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PwdChangedDate)
                    .HasColumnType("date")
                    .HasColumnName("pwd_changed_date");

                entity.Property(e => e.PwdExpDate)
                    .HasColumnType("date")
                    .HasColumnName("pwd_exp_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Voornaam)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("voornaam");

                entity.HasOne(d => d.Instelling)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.InstellingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Instelling");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK_User_Profile");

                entity.ToTable("User_Account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.ApplicatieId).HasColumnName("applicatie_id");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Applicatie)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.ApplicatieId)
                    .HasConstraintName("FK_User_Profile_Applicatie");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_User_Profile_User_Rol");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Profile_User");
            });

            modelBuilder.Entity<UserRol>(entity =>
            {
                entity.HasKey(e => e.RolId);

                entity.ToTable("User_Rol");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("omschrijving");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
