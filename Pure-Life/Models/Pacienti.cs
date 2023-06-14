using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.Models
{
    public class Pacienti
    {

        public int Id { get; set; }

        public string? UId { get; set; }
        public string? NrLeternjoftimit { get; set; }
        [Required]
        public string Emri { get; set; }
        [Required]
        public string Mbiemri { get; set; }
        public string? Gjinia { get; set; }
        public DateTime? DataLindjes { get; set; }
        public string? Alergji { get; set; }
        public string? NrTel { get; set; }
        public bool MembershipStatus { get; set; }
        public int? ShtetiId { get; set; }
        [ForeignKey("ShtetiId")]

        public string? Qyteti { get; set; }
        public int? NacionalitetiId { get; set; }
        [ForeignKey("NacionalitetiId")]
    

        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

      
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }
        public bool IsDeleted { get; set; }
        public List<Termini> Terminet { get; set; }
        public Shteti Shteti { get; set; }
        public Nacionaliteti Nacionaliteti { get; set; }
    }
}
