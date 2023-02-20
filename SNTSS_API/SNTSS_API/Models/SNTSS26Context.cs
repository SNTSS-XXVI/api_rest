using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SNTSS_API.Models
{
    public partial class SNTSS26Context : DbContext
    {
        public SNTSS26Context()
        {
        }

        public SNTSS26Context(DbContextOptions<SNTSS26Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Call> Calls { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Convention> Conventions { get; set; } = null!;
        public virtual DbSet<Dashboard> Dashboards { get; set; } = null!;
        public virtual DbSet<Escalafon> Escalafons { get; set; } = null!;
        public virtual DbSet<Guard> Guards { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<MessageHasUser> MessageHasUsers { get; set; } = null!;
        public virtual DbSet<MessageT> MessageTs { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Temp> Temps { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Call>(entity =>
            {
                entity.HasKey(e => e.IdCalls)
                    .HasName("PK__calls__C031D0FA5144ACFC");

                entity.ToTable("calls");

                entity.Property(e => e.IdCalls).HasColumnName("id_calls");

                entity.Property(e => e.DateCreateCalls)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("date_create_calls");

                entity.Property(e => e.DateFinallyCalls)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("date_finally_calls");

                entity.Property(e => e.PdfCalls)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("pdf_calls");

                entity.Property(e => e.TextCalls)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("text_calls");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__categori__CD54BC5A22D084B4");

                entity.ToTable("categoria");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.NombreCategoria)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nombre_categoria");
            });

            modelBuilder.Entity<Convention>(entity =>
            {
                entity.HasKey(e => e.IdConventions)
                    .HasName("PK__conventi__5BEC8E379874F5E5");

                entity.ToTable("conventions");

                entity.Property(e => e.IdConventions).HasColumnName("id_conventions");

                entity.Property(e => e.DateCreateConventions)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("date_create_conventions");

                entity.Property(e => e.PictureConventions)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("picture_conventions");

                entity.Property(e => e.TitleConventions)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("title_conventions");

                entity.Property(e => e.TypeConventions)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("type_conventions");
            });

            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.HasKey(e => e.IdDashboard)
                    .HasName("PK__dashboar__F9B2010A8B5E921C");

                entity.ToTable("dashboard");

                entity.Property(e => e.IdDashboard).HasColumnName("id_dashboard");

                entity.Property(e => e.DateDashboard)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("date_dashboard");

                entity.Property(e => e.NameDashboard)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("name_dashboard");
            });

            modelBuilder.Entity<Escalafon>(entity =>
            {
                entity.HasKey(e => e.IdEscalafon)
                    .HasName("PK__escalafo__2C62964596418C23");

                entity.ToTable("escalafon");

                entity.Property(e => e.IdEscalafon).HasColumnName("id_escalafon");

                entity.Property(e => e.CategoryEscalafon)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("category_escalafon");

                entity.Property(e => e.DateEscalafon)
                    .HasColumnType("date")
                    .HasColumnName("date_escalafon");

                entity.Property(e => e.DateUpdateEscalafon)
                    .HasColumnType("date")
                    .HasColumnName("date_update_escalafon");

                entity.Property(e => e.DayWorkedScalafon).HasColumnName("day_worked_scalafon");

                entity.Property(e => e.GrupEscalafon).HasColumnName("grup_escalafon");

                entity.Property(e => e.Matricula).HasColumnName("matricula");

                entity.Property(e => e.NumberEscalafon).HasColumnName("number_escalafon");

                entity.Property(e => e.Observaciones)
                    .HasColumnType("text")
                    .HasColumnName("observaciones");

                entity.Property(e => e.QualificationsEscalafon).HasColumnName("qualifications_escalafon");

                entity.Property(e => e.StatusEscalafon)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("status_escalafon");

                entity.Property(e => e.TypeHiringEscalafon).HasColumnName("type_hiring_escalafon");
            });

            modelBuilder.Entity<Guard>(entity =>
            {
                entity.HasKey(e => e.IdGuard)
                    .HasName("PK__guard__4DA32F9928E4A242");

                entity.ToTable("guard");

                entity.Property(e => e.IdGuard).HasColumnName("id_guard");

                entity.Property(e => e.ImgGuard)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("img_guard");

                entity.Property(e => e.JobGuard)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("job_guard");

                entity.Property(e => e.NameGuard)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("name_guard");

                entity.Property(e => e.PhoneGuard)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_guard");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.FechaLogs)
                    .HasName("PK__logs__71483DD76B52EB8E");

                entity.ToTable("logs");

                entity.Property(e => e.FechaLogs)
                    .HasColumnType("date")
                    .HasColumnName("fecha_logs");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(4000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.TypeLogs)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("type_logs");

                entity.Property(e => e.UserLogs)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("user_logs");
            });

            modelBuilder.Entity<MessageHasUser>(entity =>
            {
                entity.HasKey(e => e.IdMessageHasUser)
                    .HasName("PK__message___DA3290513C3676F6");

                entity.ToTable("message_has_user");

                entity.Property(e => e.IdMessageHasUser).HasColumnName("id_message_has_user");

                entity.Property(e => e.MessageMessageHasUser).HasColumnName("message_message_has_user");

                entity.Property(e => e.UserMessageHasUser).HasColumnName("user_message_has_user");

                entity.HasOne(d => d.MessageMessageHasUserNavigation)
                    .WithMany(p => p.MessageHasUsers)
                    .HasForeignKey(d => d.MessageMessageHasUser)
                    .HasConstraintName("FK__message_h__messa__30F848ED");

                entity.HasOne(d => d.UserMessageHasUserNavigation)
                    .WithMany(p => p.MessageHasUsers)
                    .HasForeignKey(d => d.UserMessageHasUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__message_h__user___300424B4");
            });

            modelBuilder.Entity<MessageT>(entity =>
            {
                entity.HasKey(e => e.IdMessageT)
                    .HasName("PK__messageT__0B5C2AC533FDAAA0");

                entity.ToTable("messageT");

                entity.Property(e => e.IdMessageT).HasColumnName("id_messageT");

                entity.Property(e => e.ContenidoMessageT)
                    .HasMaxLength(4000)
                    .HasColumnName("contenido_messageT");

                entity.Property(e => e.DateMessageT)
                    .HasColumnType("date")
                    .HasColumnName("date_messageT");

                entity.Property(e => e.TitleMessageT)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("title_messageT");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__rol__6ABCB5E0730A4FB2");

                entity.ToTable("rol");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.NameRol)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("name_rol");
            });

            modelBuilder.Entity<Temp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temp");

                entity.Property(e => e.CategoryEscalafon)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("category_escalafon");

                entity.Property(e => e.DateEscalafon)
                    .HasColumnType("date")
                    .HasColumnName("date_escalafon");

                entity.Property(e => e.DayWorkedScalafon).HasColumnName("day_worked_scalafon");

                entity.Property(e => e.GrupEscalafon).HasColumnName("grup_escalafon");

                entity.Property(e => e.MatrículaScalafon).HasColumnName("Matrícula_scalafon");

                entity.Property(e => e.NumberEscalafon).HasColumnName("number_escalafon");

                entity.Property(e => e.Observaciones)
                    .HasColumnType("text")
                    .HasColumnName("observaciones");

                entity.Property(e => e.QualificationsEscalafon).HasColumnName("qualifications_escalafon");

                entity.Property(e => e.StatusEscalafon)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("status_escalafon");

                entity.Property(e => e.TypeHiringEscalafon).HasColumnName("type_hiring_escalafon");

                entity.Property(e => e.UserIdEscalafon)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("user_id_escalafon");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUsers)
                    .HasName("PK__users__CBDF8EF7CC6FB450");

                entity.ToTable("users");

                entity.Property(e => e.IdUsers).HasColumnName("id_users");

                entity.Property(e => e.AdscripcionUsers)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("adscripcion_users");

                entity.Property(e => e.CategoryUsers)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("category_users");

                entity.Property(e => e.CveAdscripcionUsers)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("cve_adscripcion_users");

                entity.Property(e => e.DayJobUsers).HasColumnName("day_job_users");

                entity.Property(e => e.DirectionUsers)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("direction_users");

                entity.Property(e => e.MatriculaUsers).HasColumnName("matricula_users");

                entity.Property(e => e.NameUsers)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("name_users");

                entity.Property(e => e.ObservationsUsers)
                    .HasColumnType("text")
                    .HasColumnName("observations_users");

                entity.Property(e => e.PasswordUsers)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("password_users");

                entity.Property(e => e.PhoneUsers)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_users");

                entity.Property(e => e.RolUsers).HasColumnName("rol_users");

                entity.Property(e => e.ShiftUsers)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("shift_users");

                entity.Property(e => e.StatusUsers)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("status_users");

                entity.Property(e => e.WorkerContractUsers)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("worker_contract_users");

                entity.HasOne(d => d.RolUsersNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RolUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__users__rol_users__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
