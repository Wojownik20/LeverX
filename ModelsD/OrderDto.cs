using System;
namespace LeverX.Models;
public class OrderDto
{
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}
