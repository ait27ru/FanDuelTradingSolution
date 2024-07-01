using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanDuelSolution.API.Entities
{
    public class DepthChart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public int TeamId { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }
        public int PositionId { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public int PlayerId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PositionDepth { get; set; }
    }
}
