namespace EmployeeDepartmentSearch.Models;

public class DepartmentDto
{
    public int? Id { get; set; }
    public string DepartmentName { get; set; } = default!;
    public string Description { get; set; } = default!;
}
