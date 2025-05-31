namespace LeverX.WebAPI.ModelsDto;
public class EmployeeReadDto // For GET
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public decimal Salary { get; set; }
}