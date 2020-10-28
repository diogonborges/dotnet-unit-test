using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public List<User> Friends { get; set; }
    }
}
