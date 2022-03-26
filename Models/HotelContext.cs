using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class HotelContext : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }

        public DbSet<Prijava> Prijave { get; set; }

        public DbSet<Zaposleni> Zaposlenii { get; set; }

        public DbSet<Zgrada> Zgrade {get;set;}
        public HotelContext(DbContextOptions options) : base(options) {}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions
//            modelBuilder.Entity<Prijava>()
  //                      .HasMany<Prijava>(p => p.Soba)
    //                    .WithOne(p => p.Soba);
        
         }
    }
}
