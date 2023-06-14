using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.Models
{
	public class TerapiaSherbimet
	{
		public int Id { get; set; }

		[ForeignKey("TerapiaId")]
		public int TerapiaId { get; set; }

		[ForeignKey("SherbimetId")]
		public int SherbimetId { get; set; }

		public Terapia Terapia { get; set; }

		public Sherbimet Sherbimet { get; set; }


	}
}
