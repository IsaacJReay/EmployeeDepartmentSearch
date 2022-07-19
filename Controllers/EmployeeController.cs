using Microsoft.AspNetCore.Mvc;
using EmployeeDepartmentSearch.Models;
using EmployeeDepartmentSearch.Data;

namespace EmployeeDepartmentSearch.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly EmployeeDepartmentSearchDbContext _context;

    public EmployeeController(ILogger<EmployeeController> logger, EmployeeDepartmentSearchDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["EmployeeData"] = _context.Employees.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult New(EmployeeDto dto)
    {
        Employee currentEmp = new Employee
        {
            Fname = dto.Fname,
            Lname = dto.Lname,
            Gender = dto.Gender,
            Email = dto.Email,
            address = dto.address,
            DepartmentId = dto.DepartmentId
        };
        _context.Employees.Add(currentEmp);
        _context.SaveChanges();
        return Redirect("/Employee/Index");
    }

    public IActionResult Delete(string Id)
    {
        int id = Int32.Parse(Id);
        if (_context.Employees.Find(id) == null)
        {
            return BadRequest("Not Found");
        }

        Employee Employee = _context.Employees.Find(id) ?? default!;

        _context.Employees.Remove(Employee);
        _context.SaveChanges();

        return Redirect("/Employee/Index");
    }

    public IActionResult EditForm(EmployeeDto dto)
    {
        ViewData["EmployeeData"] = dto;
        return View();
    }

    [HttpPost]
    public IActionResult Edit(EmployeeDto dto)
    {
        if (_context.Employees.Find(dto.Id) == null)
        {
            return BadRequest("Not Found");
        }

        Employee currentEmployee = _context.Employees.Find(dto.Id) ?? default!;
        currentEmployee.Fname = dto.Fname;
        currentEmployee.Lname = dto.Lname;
        currentEmployee.Gender = dto.Gender;
        currentEmployee.Email = dto.Email;
        currentEmployee.address = dto.address;
        currentEmployee.DepartmentId = dto.DepartmentId;

        _context.SaveChanges();
        return Redirect("/Employee/Index");
    }

    public IActionResult Search(SearchDto dto)
    {
        List<Employee> SearchResult = new List<Employee>();

        if (dto != null)
        {
            if (dto.filter == "Id")
            {
                int QueryId;

                if (!int.TryParse(dto.searchPhrase, out QueryId))
                {
                    return BadRequest("Search isn't ID");
                }

                SearchResult = _context.Employees.Where(employee => employee.Id == QueryId).ToList();
            }
            else if (dto.filter == "Fname")
            {
                SearchResult = _context.Employees.Where(employee => employee.Fname.Contains(dto.searchPhrase)).ToList();
            }
            else if (dto.filter == "Lname")
            {
                SearchResult = _context.Employees.Where(employee => employee.Fname.Contains(dto.searchPhrase)).ToList();
            }
            else if (dto.filter == "Gender")
            {
                SearchResult = _context.Employees.Where(employee => employee.Gender.Contains(dto.searchPhrase)).ToList();
            }
            else if (dto.filter == "address")
            {
                SearchResult = _context.Employees.Where(employee => employee.Gender.Contains(dto.searchPhrase)).ToList();
            }
            else if (dto.filter == "Email")
            {
                SearchResult = _context.Employees.Where(employee => employee.address!=null && employee.address.Contains(dto.searchPhrase)).ToList();
            }
            else if (dto.filter == "DepartmentId")
            {
                int QueryId;

                if (int.TryParse(dto.searchPhrase, out QueryId))
                {
                    return BadRequest("Search isn't ID");
                }

                SearchResult = _context.Employees.Where(employee => employee.DepartmentId == QueryId).ToList();
            }
            else
            {
                int QueryId;

                if (int.TryParse(dto.searchPhrase, out QueryId))
                {
                    SearchResult = _context.Employees.Where(
                        employee => 
                        employee.Id == QueryId ||
                        employee.Fname.Contains(dto.searchPhrase) || 
                        employee.Lname.Contains(dto.searchPhrase) ||
                        employee.Gender.Contains(dto.searchPhrase) ||
                        (
                            employee.Email != null && employee.Email.Contains(dto.searchPhrase)
                        ) ||
                        (
                            employee.address !=null && employee.address.Contains(dto.searchPhrase)
                        ) ||
                        employee.DepartmentId == QueryId
                    ).ToList();
                }
                else {
                    SearchResult = _context.Employees.Where(
                        employee => 
                        employee.Fname.Contains(dto.searchPhrase) || 
                        employee.Lname.Contains(dto.searchPhrase) ||
                        employee.Gender.Contains(dto.searchPhrase) ||
                        (employee.Email != null && employee.Email.Contains(dto.searchPhrase)) ||
                        (employee.address !=null && employee.address.Contains(dto.searchPhrase))
                    ).ToList();
                }
            }
        }

        ViewData["EmployeeData"] = SearchResult;
        return View();
    }
}
