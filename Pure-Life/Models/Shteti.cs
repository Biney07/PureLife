namespace Pure_Life.Models
{
    public class Shteti
    {
        public int Id { get; set; }
        public string Emri { get; set; } 
        public string InsertedFrom { get; set; } 
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }
       
        public List<Stafi> Stafi { get; set; }
    }
}
