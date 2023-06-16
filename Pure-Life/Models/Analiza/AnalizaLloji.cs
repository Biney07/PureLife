namespace Pure_Life.Models.Analiza
{
    public class AnalizaLloji
    {public int Id { get; set; }
        public int AnalizaId { get; set; } // Foreign key
        public Analiza Analiza { get; set; } // Navigation property

        public int LlojiId { get; set; } // Foreign key
        public Lloji Lloji { get; set; } // Navigation property
		public ICollection<TerapiaAnalizaRezultati> TerapiaAnalizaRezultati { get; set; }
	}
}