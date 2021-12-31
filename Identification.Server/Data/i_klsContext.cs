using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Identification.Server.Data
{
    public partial class i_klsContext : DbContext
    {
        public i_klsContext()
        {
        }

        public i_klsContext(DbContextOptions<i_klsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ILogin> ILogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("MyConnectionIbank");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1251_CI_AS");

            modelBuilder.Entity<ILogin>(entity =>
            {
                entity.ToTable("i_login", "bankadm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CollectorIn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("collector_in");

                entity.Property(e => e.CollectorOut)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("collector_out");

                entity.Property(e => e.ComboFil).HasColumnName("combo_fil");

                entity.Property(e => e.ComboKas).HasColumnName("combo_kas");

                entity.Property(e => e.DateIn)
                    .HasColumnType("datetime")
                    .HasColumnName("date_in");

                entity.Property(e => e.DateIzm)
                    .HasColumnType("datetime")
                    .HasColumnName("date_izm");

                entity.Property(e => e.FilialAll).HasColumnName("filial_all");

                entity.Property(e => e.Fio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fio");

                entity.Property(e => e.FirstEnter).HasColumnName("first_enter");

                entity.Property(e => e.HashCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hash_code")
                    .IsFixedLength(true);

                entity.Property(e => e.IsIb).HasColumnName("is_ib");

                entity.Property(e => e.IsMobile).HasColumnName("is_mobile");

                entity.Property(e => e.IsRutoken).HasColumnName("is_rutoken");

                entity.Property(e => e.IsSms).HasColumnName("is_sms");

                entity.Property(e => e.IsVipiska).HasColumnName("is_vipiska");

                entity.Property(e => e.KlKod).HasColumnName("kl_kod");

                entity.Property(e => e.KlLevel)
                    .HasColumnName("kl_level")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.KlLogin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_login");

                entity.Property(e => e.KlNom).HasColumnName("kl_nom");

                entity.Property(e => e.KlPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_password")
                    .IsFixedLength(true);

                entity.Property(e => e.KlPassword1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_password1")
                    .IsFixedLength(true);

                entity.Property(e => e.KlPassword2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_password2")
                    .IsFixedLength(true);

                entity.Property(e => e.KlPassword3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_password3")
                    .IsFixedLength(true);

                entity.Property(e => e.KlPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("kl_phone");

                entity.Property(e => e.Kodb).HasColumnName("kodb");

                entity.Property(e => e.KodbIn).HasColumnName("kodb_in");

                entity.Property(e => e.Kodc).HasColumnName("kodc");

                entity.Property(e => e.KodcIn).HasColumnName("kodc_in");

                entity.Property(e => e.Msoffice).HasColumnName("MSoffice");

                entity.Property(e => e.NumPodp).HasColumnName("num_podp");

                entity.Property(e => e.Ot999).HasColumnName("OT_999");

                entity.Property(e => e.OtCardDok).HasColumnName("OT_CARD_DOK");

                entity.Property(e => e.OtCardOst).HasColumnName("OT_CARD_OST");

                entity.Property(e => e.OtCardRem).HasColumnName("OT_CARD_REM");

                entity.Property(e => e.OtDate)
                    .HasColumnType("datetime")
                    .HasColumnName("OT_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OtFio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OT_FIO")
                    .IsFixedLength(true);

                entity.Property(e => e.OtGruppa).HasColumnName("OT_GRUPPA");

                entity.Property(e => e.OtHeader)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("OT_HEADER");

                entity.Property(e => e.OtIsys)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("OT_ISYS")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.OtKlient).HasColumnName("OT_KLIENT");

                entity.Property(e => e.OtKodkr)
                    .HasColumnName("OT_KODKR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OtKredNach).HasColumnName("OT_KRED_NACH");

                entity.Property(e => e.OtLevel)
                    .HasColumnName("OT_LEVEL")
                    .HasDefaultValueSql("((9))");

                entity.Property(e => e.OtNom)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("OT_NOM");

                entity.Property(e => e.OtOtd).HasColumnName("OT_OTD");

                entity.Property(e => e.OtPodrazd).HasColumnName("OT_PODRAZD");

                entity.Property(e => e.OtShkod).HasColumnName("OT_SHKOD");

                entity.Property(e => e.OtShkodv).HasColumnName("OT_SHKODV");

                entity.Property(e => e.OtTel)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("OT_TEL")
                    .IsFixedLength(true);

                entity.Property(e => e.OtUid)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("OT_UID")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.Otv).HasColumnName("otv");

                entity.Property(e => e.Pin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pin")
                    .IsFixedLength(true);

                entity.Property(e => e.PinCurrent)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pin_current")
                    .IsFixedLength(true);

                entity.Property(e => e.PinEnv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pin_env");

                entity.Property(e => e.PinTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pin_time");

                entity.Property(e => e.Provodka).HasColumnName("provodka");

                entity.Property(e => e.SKass).HasColumnName("s_kass");

                entity.Property(e => e.SetBlock).HasColumnName("set_block");

                entity.Property(e => e.ShowRez).HasColumnName("show_rez");

                entity.Property(e => e.Version)
                    .HasColumnType("datetime")
                    .HasColumnName("version");

                entity.Property(e => e.VklIn).HasColumnName("vkl_in");

                entity.Property(e => e.WorkAll).HasColumnName("work_all");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
