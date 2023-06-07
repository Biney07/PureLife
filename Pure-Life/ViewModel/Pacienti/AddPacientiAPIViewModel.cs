using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pure_Life.ViewModel.Pacienti
{
    public class AddPacientiAPIViewModel
    {
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
        public string? Qyteti { get; set; }
        public int? NacionalitetiId { get; set; }
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
