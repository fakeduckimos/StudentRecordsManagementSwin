using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Models.Entities;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.Entities.People;
using System.Reflection.Metadata;

namespace StudentRecordManagement.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<ParentContact> ParentContacts { get; set; }
        public DbSet<FormRecord> FormRecords { get; set; }
        public DbSet<Detention> DetentionRecords { get; set; }
        public DbSet<SickBay> SickBayRecords { get; set; }
        public DbSet<LatePass> LatePassRecords { get; set; }
        public DbSet<LeavePass> LeavePassRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<ParentContact>().ToTable("ParentContact");

            modelBuilder.Entity<FormRecord>().ToTable("FormRecord");
            modelBuilder.Entity<Detention>().ToTable("DetentionRecord");
            modelBuilder.Entity<SickBay>().ToTable("SickBayRecord");
            modelBuilder.Entity<LatePass>().ToTable("LatePassRecord");
            modelBuilder.Entity<LeavePass>().ToTable("LeavePassRecord");

        }
    }
}
