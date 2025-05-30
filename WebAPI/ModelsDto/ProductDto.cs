using System;
namespace LeverX.WebAPI.ModelsDto;
public class ProductDto // FOR REST
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}

