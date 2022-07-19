using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDepartmentSearch.Models;

public class Department
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50)]
    public string DepartmentName { get; set; } = default!;

    [StringLength(50)]
    public string Description { get; set; } = default!;
}
