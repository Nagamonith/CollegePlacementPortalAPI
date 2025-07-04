using Microsoft.AspNetCore.Mvc;
using CollegePlacementAPI.Models;
using CollegePlacementAPI.Data;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class PlacementOfficerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PlacementOfficerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Registration Endpoint
    [HttpPost("register")]
    public IActionResult Register([FromBody] PlacementOfficer officer)
    {
        if (_context.PlacementOfficers.Any(o => o.Username == officer.Username))
        {
            return BadRequest("Username already exists.");
        }

        _context.PlacementOfficers.Add(officer);
        _context.SaveChanges();
        return Ok("Registration successful");
    }

    // ✅ (Optional) List all placement officers (for testing/debug)
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.PlacementOfficers.ToList());
    }
}
