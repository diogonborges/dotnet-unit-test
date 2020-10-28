using System;
using Movies.Api.Database;

namespace Movies.Api.Tests
{
    /// <summary>
    /// Static class for seeding
    /// </summary>
    public static class Seeder
    {
        /// <summary>
        /// Seeds the specified database.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="migrations">The action for the migrations to run on the context.</param>
        public static void Seed(Context db, Action<Context> migrations)
        {
            migrations(db);

            db.SaveChanges(true);
        }
    }
}