using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pure_Life.ViewModels
{
    public class EditStafiApiViewModel
    {
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }
        public string NrLeternjoftimit { get; set; }
        public string Gjinia { get; set; }
        public DateTime DataLindjes { get; set; }
        public string NrLincences { get; set; }
        public int ShtetiId { get; set; }
        public string Qyteti { get; set; }
        public int NacionalitetiId { get; set; }
        public string? PictureUrl { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }

        public static implicit operator EntityState(EditStafiApiViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
