namespace Pure_Life.Models.Analiza
{
    public class RezultatetEAnalizaveViewModel
    {
        public string EmriAnalizes { get; set; }

        public string EmriMbiemriPacientit { get; set; }

        public string Data { get; set; }

        public List<ListaRezultateve> ListaRezultateve { get; set; }

    }


    public class ListaRezultateve
    {
        public int TerapiaAnalizaRezultatiId { get; set; }
        public string EmriLlojitTeAnalizes { get; set; }
        public string Rezultati { get; set; }

        public string VleratReferente { get; set; }
    }
}