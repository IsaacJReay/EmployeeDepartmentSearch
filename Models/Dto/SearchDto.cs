namespace EmployeeDepartmentSearch.Models;

public class SearchDto
{
    public string searchPhrase { set; get; } = default!;
    public string filter { set; get; } = default!;
}