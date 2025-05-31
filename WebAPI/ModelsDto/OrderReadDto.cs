namespace WebAPI.ModelsDto;
public class OrderReadDto // FOR GET
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; }

}