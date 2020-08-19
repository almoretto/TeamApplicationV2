using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;



namespace TeamApplication.Data
{
    public class SementesApplicationContext : DbContext
    {
        public SementesApplicationContext(DbContextOptions<SementesApplicationContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(
                @"Server=localhost;Database=TeamAppDB;Uid=root;Pwd=;");
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Entity> Entity { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamSchedule> TeamSchedule { get; set; }
        public DbSet<TeamVolunteer> TeamVolunteer { get; set; }
        public DbSet<Volunteer> Volunteer { get; set; }

        
    }
}
