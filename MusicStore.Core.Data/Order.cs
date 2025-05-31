using System;
using MusicStore.Shared.Models;
namespace MusicStore.Core.Data;
public class Order : BaseModel
{
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}
