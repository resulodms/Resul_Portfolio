using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Models

{
    public class HeroViewModel
    {
        public HeroSection Hero { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? Expert { get; set; }
    }
}
