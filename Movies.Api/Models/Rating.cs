using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}