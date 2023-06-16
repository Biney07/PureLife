using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.Models.Analiza
{
	public class TerapiaAnalizaRezultati
	{
		public int Id { get; set; }
		[ForeignKey("TerapiaId")]
		public int TerapiaId { get; set; }

		[ForeignKey("AnalizaLlojiId")]
		public int AnalizaLlojiId { get; set; }

		public string?  Rezultati { get; set; }

		public Terapia Terapia { get; set; }
		public AnalizaLloji AnalizaLloji { get; set; }	

		



	}
}
