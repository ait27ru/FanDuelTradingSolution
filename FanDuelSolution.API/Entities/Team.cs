using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanDuelSolution.API.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string? Abbreviation { get; set; }

        public ICollection<DepthChart> DepthCharts { get; set; } = new List<DepthChart>();

        public Team(string name)
        {
            Name = name;
        }
    }
}
