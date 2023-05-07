using Pure_Life.Models.Analiza;

namespace Pure_Life.ViewModel.Analizat
{
    public class AnalizaCreateViewModel
    {
        public string Emri { get; set; }
        public double Cmimi { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public List<Lloji> Llojet { get; set; }
        public List<int> SelectedLlojet { get; set; }
    }


}
