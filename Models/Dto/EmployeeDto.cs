namespace EmployeeDepartmentSearch.Models;

public class EmployeeDto
{
    public int? Id { get; set; }
    public string Fname { get; set; } = default!;
    public string Lname { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string? Email { get; set; }
    public string? address { get; set; }
    public int? DepartmentId { get; set; }
}