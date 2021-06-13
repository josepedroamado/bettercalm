using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess.Context
{
    public class BetterCalmContext: DbContext
    {
        public DbSet<User> Administrators { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<User> Roles { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }

        public BetterCalmContext() { }
        public BetterCalmContext(DbContextOptions options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString(@"BetterCalmDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(prop => prop.Email).IsUnique();
            modelBuilder.Entity<User>().Property(prop => prop.Name).IsRequired();
            modelBuilder.Entity<User>().Property(prop => prop.Password).IsRequired();

            modelBuilder.Entity<Playlist>().Property(prop => prop.Description).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Playlist>().Property(prop => prop.Name).IsRequired();

            modelBuilder.Entity<Content>().Property(prop => prop.ArtistName).IsRequired();
            modelBuilder.Entity<Content>().Property(prop => prop.ContentUrl).IsRequired();
            modelBuilder.Entity<Content>().Property(prop => prop.ContentLength).IsRequired();
            modelBuilder.Entity<Content>().Property(prop => prop.Name).IsRequired();
            
            modelBuilder.Entity<Psychologist>().Property(prop => prop.Address).IsRequired();
            modelBuilder.Entity<Psychologist>().Property(prop => prop.FirstName).IsRequired();
            modelBuilder.Entity<Psychologist>().Property(prop => prop.LastName).IsRequired();
            modelBuilder.Entity<Psychologist>().Property(prop => prop.Format).IsRequired();

            modelBuilder.Entity<Patient>().Property(prop => prop.FirstName).IsRequired();
            modelBuilder.Entity<Patient>().Property(prop => prop.LastName).IsRequired();
            modelBuilder.Entity<Patient>().Property(prop => prop.BirthDate).IsRequired();
            modelBuilder.Entity<Patient>().Property(prop => prop.Email).IsRequired();
            modelBuilder.Entity<Patient>().Property(prop => prop.Phone).IsRequired();
        }
	}
}
