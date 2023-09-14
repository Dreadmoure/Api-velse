using System.ComponentModel.DataAnnotations;

namespace ApiØvelse.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
