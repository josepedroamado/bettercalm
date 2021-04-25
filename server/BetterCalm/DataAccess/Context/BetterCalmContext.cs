using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess.Context
{
    public class BetterCalmContext: DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> User { get; set; }

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
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Administrator>().ToTable("Administrators");
        }
	}
}
