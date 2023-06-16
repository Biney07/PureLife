using System.ComponentModel.DataAnnotations;

namespace Pure_Life.Models.Analiza
{
    public class Analiza
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Emri { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Cmimi { get; set; }

        [Required]
        public DateTime Data { get; set; }

        // Navigation property
        public ICollection<AnalizaLloji> AnalizaLlojet { get; set; }
       
    }
}