using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Database;
using Movies.Api.Models;

namespace Movies.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<Context>(options => options.UseInMemoryDatabase("Database"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<Context>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var U15291 = new User { Id = 15291, Email = "Constantin_Kuhlman15@yahoo.com" };
                var U7001 = new User { Id = 7001, Email = "Keven6@gmail.com" };
                var U51417 = new User { Id = 51417, Email = "Margaretta82@gmail.com" };
                var U62289 = new User { Id = 62289, Email = "Marquise.Borer@hotmail.com" };
                var U9250 = new User { Id = 9250, Email = "Brant16@gmail.com" };
                var U66380 = new User { Id = 66380, Email = "Douglas_Lubowitz61@hotmail.com" };
                var U34139 = new User { Id = 34139, Email = "Norwood52@gmail.com" };
                var U6146 = new User { Id = 6146, Email = "Van_Schiller@hotmail.com" };
                var U71389 = new User { Id = 71389, Email = "Jonas_Russel88@yahoo.com" };
                var U93707 = new User { Id = 93707, Email = "Orion_Mertz@hotmail.com" };

                // friends
                U15291.Friends = new List<User> { U7001, U51417, U62289 };
                U7001.Friends = new List<User> { U15291, U51417, U62289, U66380 };
                U51417.Friends = new List<User> { U15291, U7001, U9250 };
                U62289.Friends = new List<User> { U15291, U7001 };
                U9250.Friends = new List<User> { U66380, U51417 };
                U66380.Friends = new List<User> { U7001, U9250 };

                dbContext.Users.AddRange(U15291, U7001, U51417, U62289, U9250, U66380, U34139, U6146, U71389, U93707);

                // movies
                dbContext.Movies.AddRange(
                        new Movie { Id = 1, Title = "The Shawshank Redemption", Duration = "PT142M", },
                        new Movie { Id = 2, Title = "The Godfather", Duration = "PT175M" },
                        new Movie { Id = 3, Title = "The Dark Knight", Duration = "PT152M" },
                        new Movie { Id = 4, Title = "Schindler's List", Duration = "PT195M" },
                        new Movie { Id = 5, Title = "Pulp Fiction", Duration = "PT154M" },
                        new Movie { Id = 6, Title = "The Lord of the Rings: The Return of the King", Duration = "PT201M" });

                dbContext.SaveChanges();
            }
        }
    }
}
