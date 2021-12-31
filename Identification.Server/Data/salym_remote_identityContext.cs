using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Identification.Server.Data
{
    public partial class salym_remote_identityContext : DbContext
    {
        public salym_remote_identityContext()
        {
        }

        public salym_remote_identityContext(DbContextOptions<salym_remote_identityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CheckDogkrView> CheckDogkrViews { get; set; }
        public virtual DbSet<CheckDogvkView> CheckDogvkViews { get; set; }
        public virtual DbSet<ClientView> ClientViews { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<RolesUser> RolesUsers { get; set; }
        public virtual DbSet<SavedClient> SavedClients { get; set; }
        public virtual DbSet<SavedImg> SavedImgs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("MyConnection");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CheckDogkrView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("check_dogkr_view");

                entity.Property(e => e.DgDatez)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DG_DATEZ");

                entity.Property(e => e.DgKodkl).HasColumnName("DG_KODKL");

                entity.Property(e => e.DgPozn).HasColumnName("DG_POZN");

                entity.Property(e => e.KlInn)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("kl_inn")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlNam)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("kl_nam")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlTel1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_tel1")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");
            });

            modelBuilder.Entity<CheckDogvkView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("check_dogvk_view");

                entity.Property(e => e.DvDatez)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DV_DATEZ");

                entity.Property(e => e.DvKodkl).HasColumnName("DV_KODKL");

                entity.Property(e => e.DvPozn).HasColumnName("DV_POZN");

                entity.Property(e => e.KlInn)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("kl_inn")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlKod).HasColumnName("kl_kod");

                entity.Property(e => e.KlNam)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("kl_nam")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlTel1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_tel1")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");
            });

            modelBuilder.Entity<ClientView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("client_view");

                entity.Property(e => e.CibCountry)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cib_country")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.CibOpf).HasColumnName("cib_opf");

                entity.Property(e => e.CibTdok).HasColumnName("cib_tdok");

                entity.Property(e => e.Cycle).HasColumnName("cycle");

                entity.Property(e => e.Fctroen)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("_fctroen")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fdom)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("_fdom")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fdr)
                    .HasColumnType("datetime")
                    .HasColumnName("_fdr");

                entity.Property(e => e.Filial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_filial")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Find)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("_find")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fknp)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("_fknp")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fkorpuc)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("_fkorpuc")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fkvart)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("_fkvart")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fmr)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("_fmr")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fnp)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_fnp")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fobl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_fobl")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Fraion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_fraion")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Ful)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_ful")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Grajd)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("_grajd")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.KlAdr)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("kl_adr")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlAffin).HasColumnName("KL_AFFIN");

                entity.Property(e => e.KlCfr).HasColumnName("kl_cfr");

                entity.Property(e => e.KlData)
                    .HasColumnType("datetime")
                    .HasColumnName("kl_data");

                entity.Property(e => e.KlDatareg)
                    .HasColumnType("datetime")
                    .HasColumnName("kl_datareg");

                entity.Property(e => e.KlDatevp)
                    .HasColumnType("datetime")
                    .HasColumnName("kl_datevp");

                entity.Property(e => e.KlDok)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("kl_dok")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlDokend)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("kl_dokend")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlDolgn)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("kl_dolgn")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlFam)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_kl_fam")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlFio)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("kl_fio")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlFio1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("kl_fio1")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlFio2)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("kl_fio2")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlFio3)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("kl_fio3")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlGni).HasColumnName("kl_gni");

                entity.Property(e => e.KlGr).HasColumnName("kl_gr");

                entity.Property(e => e.KlGrajd)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("_kl_grajd")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlGrup).HasColumnName("kl_grup");

                entity.Property(e => e.KlIndpdater)
                    .HasColumnType("datetime")
                    .HasColumnName("kl_indpdater");

                entity.Property(e => e.KlIndpmestreg)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("kl_indpmestreg")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlIndpnom)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("kl_indpnom")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlIndporgan)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("kl_indporgan")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlInn)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("kl_inn")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlIsys)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("kl_isys")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlKeyw)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("kl_keyw")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlKl).HasColumnName("kl_kl");

                entity.Property(e => e.KlKod).HasColumnName("kl_kod");

                entity.Property(e => e.KlKodstr).HasColumnName("kl_kodstr");

                entity.Property(e => e.KlKodter)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("kl_kodter")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlLevel).HasColumnName("kl_level");

                entity.Property(e => e.KlMector)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("kl_mector")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlMvd)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("kl_mvd")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlNam)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("kl_nam")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_kl_name")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlNational).HasColumnName("kl_national");

                entity.Property(e => e.KlNdok)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("kl_ndok")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlNom).HasColumnName("kl_nom");

                entity.Property(e => e.KlNomuch).HasColumnName("kl_nomuch");

                entity.Property(e => e.KlOkpo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("kl_okpo")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlOtch)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_kl_otch")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlOtr).HasColumnName("kl_otr");

                entity.Property(e => e.KlOtrn)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("kl_otrn")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlOtv).HasColumnName("kl_otv");

                entity.Property(e => e.KlPassw)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_passw")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlReg)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_kl_reg")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlRelig).HasColumnName("kl_relig");

                entity.Property(e => e.KlRnsf)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("kl_rnsf")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlSempolog).HasColumnName("kl_sempolog");

                entity.Property(e => e.KlSms).HasColumnName("kl_sms");

                entity.Property(e => e.KlSrdok)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("kl_srdok")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlStat).HasColumnName("kl_stat");

                entity.Property(e => e.KlTel1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_tel1")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlTel2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kl_tel2")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlTipkl).HasColumnName("kl_tipkl");

                entity.Property(e => e.KlVid)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("_kl_vid")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.KlVids).HasColumnName("kl_vids");

                entity.Property(e => e.Kldatlic)
                    .HasColumnType("datetime")
                    .HasColumnName("kldatlic");

                entity.Property(e => e.Klnomlic).HasColumnName("klnomlic");

                entity.Property(e => e.Kodb).HasColumnName("kodb");

                entity.Property(e => e.Mreg)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("_mreg")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Opf)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("_opf")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Org)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("_org")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.P482)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("p482")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Predct).HasColumnName("predct");

                entity.Property(e => e.Problem).HasColumnName("problem");

                entity.Property(e => e.Rchp)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("_rchp")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Uctroen)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("_uctroen")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Udom)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("_udom")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Uind)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("_uind")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Uknp)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("_uknp")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Ukorpuc)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("_ukorpuc")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Ukvart)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("_ukvart")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Unp)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_unp")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Uobl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_uobl")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Uraion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_uraion")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");

                entity.Property(e => e.Uul)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_uul")
                    .UseCollation("SQL_Latin1_General_CP1251_CI_AS");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.RoleUsersId, "IX_Logins_RoleUsersId");

                entity.Property(e => e.Login1).HasColumnName("Login");

                entity.HasOne(d => d.RoleUsers)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleUsersId);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_RefreshTokens_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<RolesUser>(entity =>
            {
                entity.HasKey(e => e.RoleUsersId);
            });

            modelBuilder.Entity<SavedClient>(entity =>
            {
                entity.Property(e => e.AparmentNumber).HasMaxLength(15);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreditSum).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateTime).HasMaxLength(50);

                entity.Property(e => e.District).HasMaxLength(100);

                entity.Property(e => e.Fio)
                    .HasMaxLength(250)
                    .HasColumnName("FIO");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.HouseNumber).HasMaxLength(15);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MartialStatus).HasMaxLength(15);

                entity.Property(e => e.MidName).HasMaxLength(100);

                entity.Property(e => e.Number1).HasMaxLength(20);

                entity.Property(e => e.Number2).HasMaxLength(20);

                entity.Property(e => e.PassportAparmentNumber).HasMaxLength(15);

                entity.Property(e => e.PassportCity).HasMaxLength(50);

                entity.Property(e => e.PassportDistrict).HasMaxLength(100);

                entity.Property(e => e.PassportHouseNumber).HasMaxLength(12);

                entity.Property(e => e.PassportInn).HasMaxLength(20);

                entity.Property(e => e.PassportIssuedBy).HasMaxLength(20);

                entity.Property(e => e.PassportNumber).HasMaxLength(20);

                entity.Property(e => e.PassportRegion).HasMaxLength(50);

                entity.Property(e => e.PassportSeries).HasMaxLength(20);

                entity.Property(e => e.PassportStreet).HasMaxLength(100);

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(30);
            });

            modelBuilder.Entity<SavedImg>(entity =>
            {
                entity.HasKey(e => e.ImgId);

                entity.ToTable("SavedImg");

                entity.HasIndex(e => e.ClientId, "IX_SavedImg_ClientId")
                    .IsUnique();

                entity.HasOne(d => d.Client)
                    .WithOne(p => p.SavedImg)
                    .HasForeignKey<SavedImg>(d => d.ClientId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
