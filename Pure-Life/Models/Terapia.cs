using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.Models
{
    public class Terapia
    {
        public int Id { get; set; }

        public string Pershkrimi { get; set; }
        public string Diagnoza { get; set; }

        public string Barnat { get; set; }
        public string InsertedFrom { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedFrom { get; set; }

        public bool IsDeleted { get; set; }
        public int TerminiId { get; set; }
        [ForeignKey("TerminiId")]

        public Termini Termini { get; set; }

        public List<TerapiaSherbimet> TerapiaSherbimet { get; set; }



    }
}
