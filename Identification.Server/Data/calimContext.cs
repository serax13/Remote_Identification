using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Identification.Server.Data
{
    public partial class calimContext : DbContext
    {
        public calimContext()
        {
        }

        public calimContext(DbContextOptions<calimContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SmsSend> SmsSends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("MyConnectionCalim");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1251_CI_AS");

            modelBuilder.Entity<SmsSend>(entity =>
            {
                entity.ToTable("sms_send");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateSms)
                    .HasColumnType("datetime")
                    .HasColumnName("date_sms");

                entity.Property(e => e.Datel)
                    .HasColumnType("datetime")
                    .HasColumnName("datel")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DoDate)
                    .HasColumnType("datetime")
                    .HasColumnName("do_date");

                entity.Property(e => e.DoPozn).HasColumnName("do_pozn");

                entity.Property(e => e.KlKod).HasColumnName("kl_kod");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rass).HasColumnName("rass");

                entity.Property(e => e.RetrunStatus).HasColumnName("retrun_status");

                entity.Property(e => e.ReturnMessage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("return_message");

                entity.Property(e => e.Sms)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sms");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TipS).HasColumnName("tip_s");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
