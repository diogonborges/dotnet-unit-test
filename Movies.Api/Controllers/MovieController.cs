using Microsoft.AspNetCore.Mvc;
using Movies.Api.Database;
using Movies.Api.Models;
using System.Linq;

namespace Movies.Api.Controllers
{
    /// <summary>
    /// Probably a good idea to not have the controller directly accessing the DBD
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly Context context;

        public MoviesController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public Movie Get(string title)
        {
            return context.Movies.FirstOrDefault(movie => movie.Title == title);
        }
    }
}
