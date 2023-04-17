using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pure_Life.ViewModel;

public class StafiViewModel
{
    public int Id { get; set; }
    public string? NrLeternjoftimit { get; set; }
    public string Emri { get; set; }
    public string Mbiemri { get; set; }
    public string? Gjinia { get; set; }
    public DateTime? DataLindjes { get; set; }
    public string? NrLincences { get; set; }
    public string? NrTel { get; set; }
    public int RoletId { get; set; }
    public int? ShtetiId { get; set; }
    public string? Qyteti { get; set; }
    public int? NacionalitetiId { get; set; }
    public int LemiaId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string InsertedFrom { get; set; }
    public DateTime InsertedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedFrom { get; set; }
    public IFormFile PictureUrl { get; set; }
    public SelectList LemiaOptions { get; set; }
    public SelectList NacionalitetiOptions { get; set; }
    public SelectList RoletOptions { get; set; }
    public SelectList ShtetiOptions { get; set; }
}
