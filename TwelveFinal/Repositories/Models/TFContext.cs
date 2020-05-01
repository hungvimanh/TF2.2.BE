using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TwelveFinal.Repositories.Models
{
    public partial class TFContext : DbContext
    {
        public virtual DbSet<AspirationDAO> Aspiration { get; set; }
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<EthnicDAO> Ethnic { get; set; }
        public virtual DbSet<FormDAO> Form { get; set; }
        public virtual DbSet<HighSchoolDAO> HighSchool { get; set; }
        public virtual DbSet<MajorsDAO> Majors { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<StudentDAO> Student { get; set; }
        public virtual DbSet<SubjectGroupDAO> SubjectGroup { get; set; }
        public virtual DbSet<TownDAO> Town { get; set; }
        public virtual DbSet<UniversityDAO> University { get; set; }
        public virtual DbSet<University_MajorsDAO> University_Majors { get; set; }
        public virtual DbSet<University_Majors_SubjectGroupDAO> University_Majors_SubjectGroup { get; set; }
        public virtual DbSet<UserDAO> User { get; set; }
        public virtual DbSet<__MigrationLogDAO> __MigrationLog { get; set; }
        // Unable to generate entity type for table 'dbo.__SchemaSnapshot'. Please see the warning messages.


        public TFContext(DbContextOptions<TFContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=112.137.129.216,1699;initial catalog=TF;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspirationDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_Form");

                entity.HasOne(d => d.Majors)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_Majors");

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_SubjectGroup");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_University");
            });

            modelBuilder.Entity<DistrictDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<EthnicDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FormDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Area).HasMaxLength(10);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.ExceptLanguages).HasMaxLength(500);

                entity.Property(e => e.Languages).HasMaxLength(50);

                entity.Property(e => e.PriorityType).HasMaxLength(2);

                entity.HasOne(d => d.ClusterContest)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.ClusterContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_Province");

                entity.HasOne(d => d.RegisterPlaceOfExam)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.RegisterPlaceOfExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_HighSchool");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_Student");
            });

            modelBuilder.Entity<HighSchoolDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.HighSchools)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HighSchool_Province");
            });

            modelBuilder.Entity<MajorsDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StudentDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Identify)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.HasOne(d => d.Ethnic)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.EthnicId)
                    .HasConstraintName("FK_Student_Ethnic");

                entity.HasOne(d => d.HighSchool)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.HighSchoolId)
                    .HasConstraintName("FK_Student_HighSchool");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK_Student_Town");
            });

            modelBuilder.Entity<SubjectGroupDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TownDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Town_District");
            });

            modelBuilder.Entity<UniversityDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Website).HasMaxLength(50);
            });

            modelBuilder.Entity<University_MajorsDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.HasOne(d => d.Majors)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_Majors");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_University");
            });

            modelBuilder.Entity<University_Majors_SubjectGroupDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.University_Majors_SubjectGroups)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_SubjectGroup_SubjectGroup");

                entity.HasOne(d => d.University_Majors)
                    .WithMany(p => p.University_Majors_SubjectGroups)
                    .HasForeignKey(d => d.University_MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_SubjectGroup_University_Majors");
            });

            modelBuilder.Entity<UserDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salt).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_User_Student");
            });

            modelBuilder.Entity<__MigrationLogDAO>(entity =>
            {
                entity.HasKey(e => new { e.migration_id, e.complete_dt, e.script_checksum });

                entity.Property(e => e.script_checksum).HasMaxLength(64);

                entity.Property(e => e.applied_by)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.deployed).HasDefaultValueSql("((1))");

                entity.Property(e => e.package_version)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.release_version)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.script_filename)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.sequence_no).ValueGeneratedOnAdd();

                entity.Property(e => e.version)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}
