using CollegePlacementAPI.Data;
using Microsoft.AspNetCore.Mvc;
using CollegePlacementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompanyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Companies.ToList());
    }

    [HttpPost]
    public IActionResult Add([FromBody] Company company)
    {
        _context.Companies.Add(company);
        _context.SaveChanges();
        return Ok(company);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Company updated)
    {
        var company = _context.Companies.Find(id);
        if (company == null) return NotFound();

        company.Name = updated.Name;
        company.Category = updated.Category;
        company.RequiredSkills = updated.RequiredSkills;
        company.RequiredCGPA = updated.RequiredCGPA;
        company.JobRole = updated.JobRole;
        company.AllowedBacklogs = updated.AllowedBacklogs;
        company.EligiblePassoutYear = updated.EligiblePassoutYear;
        company.Description = updated.Description;
        company.Location = updated.Location;

        _context.SaveChanges();
        return Ok(company);
    }
}