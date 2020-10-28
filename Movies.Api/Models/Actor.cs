using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}