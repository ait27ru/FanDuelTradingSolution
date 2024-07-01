using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanDuelSolution.API.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Range(0, 99)]
        public byte Number { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public ICollection<DepthChart> DepthCharts { get; set; } = new List<DepthChart>();

        public Player(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FullName()
        {
            return $"{LastName}, {FirstName}";
        }
    }
}
