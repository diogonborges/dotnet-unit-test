using Microsoft.EntityFrameworkCore;
using Movies.Api.Models;
using System.Diagnostics.CodeAnalysis;

namespace Movies.Api.Database
{
    [ExcludeFromCodeCoverage]
    public class Context : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}
