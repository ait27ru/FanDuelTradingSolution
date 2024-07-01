using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanDuelSolution.API.Entities
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string PositionType { get; set; }

        public ICollection<DepthChart> DepthCharts { get; set; } = new List<DepthChart>();

        public Position(string name, string positionType)
        {
            Name = name;
            PositionType = positionType;
        }
    }
}
