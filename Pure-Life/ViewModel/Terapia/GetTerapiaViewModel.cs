using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.ViewModel.Terapia
{
	public class GetTerapiaViewModel
	{
		public int Id { get; set; }

		public string Pacienti { get; set; }

		public int TerminiId { get; set; }

		public string NrLeternjoftimit { get; set; }
		public string Koha { get; set; }

		public string Diagnoza { get; set; }

		public string Pershkrimi { get; set; }

		public string Barnat { get; set; }

		public string Doktori { get; set; }

		public double Cmimi { get; set; }

		public List<string> Sherbimet { get; set; }

		public List<Sherbimet> SherbimetObj { get; set; }

		public string InsertedFrom { get; set; }

		public DateTime InsertedDate { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string? ModifiedFrom { get; set; }
		public List<AnalizaETerapise> Analizat { get; set; }

	}

	public class AnalizaETerapise
	{
		public int Id { get; set; }
		public string EmriAnalizes { get; set; }
		public double Cmimi { get; set; }
		public DateTime Data { get; set; }
	}
	public class Sherbimet
	{
		public int Id { get; set; }
		public string Emri { get; set; }
	}
}
