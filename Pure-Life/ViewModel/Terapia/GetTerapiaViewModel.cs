using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.ViewModel.Terapia
{
	public class GetTerapiaViewModel
	{
		public int Id { get; set; }

		public string Pacienti { get; set; }

		public string NrLeternjoftimit { get; set; }
		public string Koha { get; set; }

		public string Diagnoza { get; set; }

		public string Pershkrimi { get; set; }

		public string Barnat { get; set; }

		public string Doktori { get; set; }

		public List<string> Sherbimet { get; set; }
		public string InsertedFrom { get; set; }

		public DateTime InsertedDate { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string? ModifiedFrom { get; set; }

	}
}
