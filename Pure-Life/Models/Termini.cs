using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.Models
{
    public class Termini
    {

        public int Id { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool Status { get; set; }

        public double Price { get; set; }

        public string InsertedFrom { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedFrom { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("PacientiId")]
        public int? PacientiId { get; set; }
        [ForeignKey("StafiId")]
        public int StafiId { get; set; }

        public Pacienti Pacienti { get; set; }
        public Stafi Stafi { get; set; }
    }
}
