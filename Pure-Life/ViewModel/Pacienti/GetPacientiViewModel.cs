using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pure_Life.ViewModel.Pacienti
{
	public class GetPacientiViewModel
	{
		public int Id { get; set; }

		public string? UId { get; set; }
		public string? NrLeternjoftimit { get; set; }
		public string Emri { get; set; }

		public string Mbiemri { get; set; }
		public string? Gjinia { get; set; }
		public DateTime? DataLindjes { get; set; }
		public string? Alergji { get; set; }
		public string? NrTel { get; set; }
		public bool MembershipStatus { get; set; }
		public int? ShtetiId { get; set; }
		public string? Qyteti { get; set; }
		public int? NacionalitetiId { get; set; }
		public string Email { get; set; }
		public DateTime InsertedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedFrom { get; set; }
		public bool IsDeleted { get; set; }
	
	}
}
