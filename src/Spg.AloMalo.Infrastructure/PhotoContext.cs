using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Model.RichTypes;
using Spg.AloMalo.Infrastructure.IdConverters;
using Spg.AloMalo.Infrastructure.RichTypesConverters;
using System;
using System.Reflection.Emit;

namespace Spg.AloMalo.Infrastructure
{
    // Mock EF Core
    // https://code-maze.com/ef-core-mock-dbcontext/

    // Test-Containers-Example:
    // https://testcontainers.com/guides/testing-an-aspnet-core-web-app/
    // https://github.com/testcontainers/testcontainers-dotnet/blob/develop/examples/WeatherForecast/src/WeatherForecast/DatabaseContainer.cs

    // 1. Klasse vo DbContext ableiten (NuGet-Package EF Core installieren)
    public class PhotoContext : DbContext
    {
        // 2. Sets festlegen (Aggregate Roots)
        public DbSet<Photo> Photos => Set<Photo>();
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Photographer> Photographers => Set<Photographer>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Payment> Payments => Set<Payment>();

        // 3. Konstruktoren
        public PhotoContext()
        { }
        public PhotoContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                //builder.UseSqlServer("Server=localhost\\sqlexpress;Database=PhotoDb;Integrated Security=true;MultipleActiveResultSets=True;Encrypt=False;");
                //builder.UseSqlite("Data Source=C:\\HTL\\Unterricht\\SJ2324\\4BHIF\\POS\\sj23-24-4bhif-pos-schrutek\\Spg.AloMalo\\Photo.db");
            }
        }

        // 4. Methoden ((OnConfiguring), OnModelCreating)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Value Objects
            builder.Entity<Photographer>().OwnsOne(e => e.StudioAddress);
            builder.Entity<Photographer>().OwnsOne(e => e.BusinessPhoneNumber);
            builder.Entity<Photographer>().OwnsOne(e => e.MobilePhoneNumber);
            builder.Entity<Photographer>().OwnsOne(e => e.Username);
            builder.Entity<Person>().OwnsOne(e => e.Username);
            builder.Entity<Photo>().OwnsOne(e => e.Location);

            builder.Entity<Album>().Ignore(e => e.IsValid);

            // Nested Value Objects
            // https://stackoverflow.com/questions/53652135/entity-framework-core-2-1-owned-types-and-nested-value-objects
            builder.Entity<Photographer>(p =>
            {
                p.OwnsOne(e => e.StudioAddress, a =>
                {
                    a.OwnsOne(e => e.State, state =>
                    {
                        state.Property(e => e.Name).HasColumnName("StateName");
                    });
                });
            });

            // Photographer -> EMails
            builder.Entity<Photographer>().OwnsMany(
                p => p.EMails, a =>
                {
                    a.WithOwner().HasForeignKey("PhotographerId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                });

            // Rich Types
            // https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-3/
            builder.Entity<Album>()
                .Property(p => p.Id)
                .HasConversion(new AlbumIdValueConverter())
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Entity<Photo>()
                .Property(p => p.Id)
                .HasConversion(new PhotoIdValueConverter())
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Entity<AlbumPhoto>()
                .Property(p => p.Id)
                .HasConversion(new AlbumPhotoIdValueConverter())
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Entity<Photographer>()
                .Property(p => p.Id)
                .HasConversion(new PhotographerIdValueConverter())
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Entity<Person>()
                .Property(p => p.Id)
                .HasConversion(new PersonIdValueConverter())
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();


            //builder.Entity<Person>()
            //    .Property(p => p.FirstName)
            //    .HasConversion(new PersonFirstNameConverter())
            //    .HasColumnName("FirstName");
        }
    }
}
