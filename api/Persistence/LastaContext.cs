using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class LastaContext : DbContext
    {
        public DbSet<Country> Country { get; set; }
        public DbSet<CountrySection> CountrySection { get; set; }
        public DbSet<Accommodation> Accommodation { get; set; }
        public DbSet<AccommodationType> AccommodationType { get; set; }
        public DbSet<Amenity> Amenity { get; set; }
        public DbSet<AmenityType> AmenityType { get; set; }
        public DbSet<AccommodationAmenity> AccommodationAmenity { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CitySection> CitySection { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<TourCountry> TourCountry { get; set; }
        public DbSet<TourSection> TourSection { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<DayCountry> DayCountry { get; set; }
        public DbSet<DayCity> DayCity { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ScheduleItem> ScheduleItem { get; set; }
        public DbSet<Guide> Guide { get; set; }
        public DbSet<GuideLanguage> GuideLanguage { get; set; }
        public DbSet<Traveler> Traveler { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingTraveler> BookingTraveler { get; set; }
        public DbSet<PlaceType> PlaceType { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<TransportType> TransportType { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Message> Message { get; set; }


        public LastaContext(DbContextOptions<LastaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<AccommodationAmenity>()
                .HasKey(aa => new { aa.AmenityId, aa.AccommodationId });

            modelBuilder.Entity<AccommodationAmenity>()
                .HasOne(aa => aa.Accommodation)
                .WithMany(ac => ac.AccommodationAmenity)
                .HasForeignKey(aa => aa.AccommodationId);

            modelBuilder.Entity<AccommodationAmenity>()
                .HasOne(aa => aa.Accommodation)
                .WithMany(am => am.AccommodationAmenity)
                .HasForeignKey(aa => aa.AccommodationId);

            modelBuilder.Entity<TourCountry>()
                .HasKey(tc => new { tc.TourId, tc.CountryId });

            modelBuilder.Entity<TourCountry>()
                .HasOne(tc => tc.Country)
                .WithMany(c => c.TourCountry)
                .HasForeignKey(tc => tc.CountryId);

            modelBuilder.Entity<TourCountry>()
                .HasOne(tc => tc.Tour)
                .WithMany(t => t.TourCountry)
                .HasForeignKey(tc => tc.TourId);


            modelBuilder.Entity<DayCountry>()
                .HasKey(dc => new { dc.DayId, dc.CountryId });

            modelBuilder.Entity<DayCountry>()
                .HasOne(dc => dc.Country)
                .WithMany(c => c.DayCountry)
                .HasForeignKey(dc => dc.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DayCountry>()
                .HasOne(dc => dc.Day)
                .WithMany(d => d.DayCountry)
                .HasForeignKey(dc => dc.DayId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<DayCity>()
                .HasKey(dc => new { dc.DayId, dc.CityId });

            modelBuilder.Entity<DayCity>()
                .HasOne(dc => dc.City)
                .WithMany(c => c.DayCity)
                .HasForeignKey(dc => dc.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DayCity>()
                .HasOne(dc => dc.Day)
                .WithMany(d => d.DayCity)
                .HasForeignKey(dc => dc.DayId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookingTraveler>()
                .HasKey(bt => new { bt.BookingId, bt.TravelerId });

            modelBuilder.Entity<BookingTraveler>()
                .HasOne(bt => bt.Traveler)
                .WithMany(b => b.BookingTraveler)
                .HasForeignKey(bt => bt.TravelerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookingTraveler>()
                .HasOne(bt => bt.Booking)
                .WithMany(b => b.BookingTraveler)
                .HasForeignKey(bt => bt.BookingId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<GuideLanguage>()
                .HasKey(gl => new { gl.GuideId, gl.LanguageId });

            modelBuilder.Entity<GuideLanguage>()
                .HasOne(gl => gl.Language)
                .WithMany(l => l.GuideLanguage)
                .HasForeignKey(gl => gl.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GuideLanguage>()
                .HasOne(gl => gl.Guide)
                .WithMany(g => g.GuideLanguage)
                .HasForeignKey(gl => gl.GuideId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
