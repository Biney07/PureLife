using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Pure_Life.Models;

namespace Pure_Life.ViewModel.Pacienti
{
    public class UpdatePacientiAPIViewModel
    {
        public int? Id { get; set; }

        public string? UId { get; set; }
        public string NrLeternjoftimit { get; set; }

        [Required]
        public string Emri { get; set; }

        [Required]
        public string Mbiemri { get; set; }

        public string Gjinia { get; set; }

        public DateTime? DataLindjes { get; set; }

        public string Alergji { get; set; }

        public string NrTel { get; set; }



        public int ShtetiId { get; set; }

        public string Qyteti { get; set; }

        public int? NacionalitetiId { get; set; }

        [Required]
        public string Email { get; set; }

      


    }
}
