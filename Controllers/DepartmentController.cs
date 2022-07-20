using Microsoft.AspNetCore.Mvc;
using EmployeeDepartmentSearch.Models;
using EmployeeDepartmentSearch.Data;

namespace EmployeeDepartmentSearch.Controllers;

public class DepartmentController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly EmployeeDepartmentSearchDbContext _context;

    public DepartmentController(ILogger<EmployeeController> logger, EmployeeDepartmentSearchDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["DepartmentData"] = _context.Departments.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult New(DepartmentDto dto)
    {
        Department currentDept = new Department
        {
            DepartmentName = dto.DepartmentName,
            Description = dto.Description
        };
        _context.Departments.Add(currentDept);
        _context.SaveChanges();
        return Redirect("/Department/Index");
    }

    public IActionResult Delete(string Id)
    {
        int id = Int32.Parse(Id);
        if (_context.Departments.Find(id) == null)
        {
            return BadRequest("Not Found");
        }

        Department Department = _context.Departments.Find(id) ?? default!;
        _context.Departments.Remove(Department);
        _context.SaveChanges();

        return Redirect("/Department/Index");
    }

    public IActionResult EditForm(DepartmentDto dto)
    {
        ViewData["DepartmentData"] = dto;
        return View();
    }

    [HttpPost]
    public IActionResult Edit(DepartmentDto dto)
    {
        Console.WriteLine("id=" + dto.Id);
        if (_context.Departments.Find(dto.Id) == null)
        {
            return BadRequest("Not Found");
        }

        Department currentDepartment = _context.Departments.Find(dto.Id) ?? default!;
        currentDepartment.DepartmentName = dto.DepartmentName;
        currentDepartment.Description = dto.Description;

        _context.SaveChanges();
        return Redirect("/Department/Index");
    }


    public IActionResult Search(SearchDto dto)
    {
        List<Department> SearchResult = new List<Department>();

        if (dto != null)
        {
            if (dto.filter == "Id")
            {
                int QueryId;

                if (!int.TryParse(dto.searchPhrase, out QueryId))
                {
                    return BadRequest("Search isn't ID");
                }

                SearchResult = _context.Departments.Where(department => department.Id == QueryId).ToList();
            }
            else if (dto.filter == "DepartmentName")
            {
                SearchResult = _context.Departments.Where(department => department.DepartmentName.Contains(dto.searchPhrase)).ToList();
            }
            else if (dto.filter == "Description")
            {
                SearchResult = _context.Departments.Where(department => department.Description.Contains(dto.searchPhrase)).ToList();
            }
            else
            {
                int QueryId;

                if (int.TryParse(dto.searchPhrase, out QueryId))
                {
                    SearchResult = _context.Departments.Where(
                        department =>
                        department.Id == QueryId ||
                        department.DepartmentName.Contains(dto.searchPhrase) ||
                        department.Description.Contains(dto.searchPhrase)
                    ).ToList();
                }
                else
                {
                    SearchResult = _context.Departments.Where(
                        department =>
                        department.DepartmentName.Contains(dto.searchPhrase) ||
                        department.Description.Contains(dto.searchPhrase)
                    ).ToList();
                }
            }
        }

        ViewData["DepartmentData"] = SearchResult;
        return View();
    }
}
