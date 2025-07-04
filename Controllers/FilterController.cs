using CollegePlacementAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class FilterController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public FilterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("eligible/{companyId}")]
    public IActionResult GetEligibleStudents(int companyId)
    {
        var company = _context.Companies.Find(companyId);
        if (company == null) return NotFound("Company not found");

        var eligible = _context.Students.ToList().Where(s =>
            s.CGPA >= company.RequiredCGPA &&
            s.Backlogs <= company.AllowedBacklogs &&
            s.PassoutYear == company.EligiblePassoutYear &&
            company.RequiredSkills.Split(',').All(skill => s.Skills.Contains(skill.Trim()))
        ).ToList();

        return Ok(eligible);
    }
}