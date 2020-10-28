using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public List<Actor> Actors { get; set; }
        public List<User> WatchList { get; set; }
        public List<User> Favorites { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}