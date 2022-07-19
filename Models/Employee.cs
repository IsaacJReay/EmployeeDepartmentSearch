using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDepartmentSearch.Models;

public class Employee
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50)]
    public string Fname { get; set; } = default!;

    [StringLength(50)]
    public string Lname { get; set; } = default!;

    [StringLength(15)]
    public string Gender { get; set; } = default!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? address { get; set; }

    [ForeignKey("Department")]
    public int? DepartmentId { get; set; }
    public virtual Department? Department { get; set; }

}