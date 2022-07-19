using Microsoft.EntityFrameworkCore;
using EmployeeDepartmentSearch.Models;

namespace EmployeeDepartmentSearch.Data;
public class EmployeeDepartmentSearchDbContext : DbContext
{
    public EmployeeDepartmentSearchDbContext(DbContextOptions<EmployeeDepartmentSearchDbContext> options) : base(options)
    {

    }

    public DbSet<Department> Departments { get; set; } = default!;
    public DbSet<Employee> Employees { get; set; } = default!;
}