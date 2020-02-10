using KennelIndexer.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace KennelIndexer.API.DbContexts
{
    public class PersonLibraryContext : DbContext
    {
        public PersonLibraryContext(DbContextOptions<PersonLibraryContext> options)
            :base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    PersonId = Guid.Parse("05C6C2AB-C63B-4C78-818F-39565D57BCF5"),
                    FirstName = "Thomas",
                    LastName = "Holmegaard"
                },
                new Person()
                {
                    PersonId = Guid.Parse("05C6C2AB-C63B-4C78-818F-39565D57ACF5"),
                    FirstName = "Nig",
                    LastName = "Bommer",
                    Address = "Lollevej 22",
                    ReasonsForBeingOnTheList = "Han var pyskopat..."

                }
                );

            modelBuilder.Entity<Picture>().HasData(
                new Picture
                {
                    PictureId = Guid.Parse("C54F132B-79AA-4986-AAB4-B0DA907CE843"),
                    PictureUri = "located/somewhere/lol.png",
                    PersonId = Guid.Parse("05C6C2AB-C63B-4C78-818F-39565D57BCF5")
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
