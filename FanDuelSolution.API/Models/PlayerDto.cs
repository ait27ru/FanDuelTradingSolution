namespace FanDuelSolution.API.Models
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public byte Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int PositionDepth { get; set; }
    }
}
