using CollegePlacementAPI.Data;
using CollegePlacementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public LoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Login([FromBody] PlacementOfficer request)
    {
        var user = _context.PlacementOfficers.FirstOrDefault(u =>
            u.Username == request.Username && u.Password == request.Password);

        if (user == null) return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Invalid credentials" });

        return Ok(new { message = "Login successful" });




    }
}