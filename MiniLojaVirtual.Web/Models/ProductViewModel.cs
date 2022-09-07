using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniLojaVirtual.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Nome")]
    public string? Name { get; set; }
    [Required]
    [Display(Name = "Preço")]
    public decimal Price { get; set; }
    [Required]
    [Display(Name = "Descrição")]
    public string? Description { get; set; }
    [Required]
    [Display(Name = "Estoque")]
    public long Stock { get; set; }
    [Required]
    [Display(Name = "Imagem")]
    public string? ImageURL { get; set; }
    [Display(Name = "Categoria")]
    public string? CategoryName { get; set; }
    [Display(Name = "Categorias")]
    public int CategoryId { get; set; }
}
