namespace WebAPI.ModelsDto;
public class ProductReadDto // FOR GET
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}