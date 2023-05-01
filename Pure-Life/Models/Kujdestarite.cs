using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pure_Life.Models
{
    public class Kujdestarite
    {
        public int Id { get; set; }



        public DateTime Data { get; set; }
        public int Kati { get; set; }
        public string Reparti { get; set; }

        public string InsertedFrom { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("StafiId")]
        public int StafiId { get; set; }
        public Stafi Stafi { get; set; }
    }
}
