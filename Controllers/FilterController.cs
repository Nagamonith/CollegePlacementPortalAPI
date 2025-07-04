using Microsoft.AspNetCore.Mvc;
using CollegePlacementAPI.Data;
using System.Linq;
using System.Collections.Generic;
using CollegePlacementAPI.Models;

namespace CollegePlacementAPI.Controllers
{
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
            if (company == null)
                return NotFound("Company not found");

            // Parse and sanitize company skills
            var companySkills = (company.RequiredSkills ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(skill => skill.Trim().ToLower())
                .ToList();

            // ✅ Use AsEnumerable to switch from SQL to in-memory for skill matching
            var eligibleStudents = _context.Students
                .AsEnumerable()
                .Where(s =>
                    s.CGPA >= company.RequiredCGPA &&
                    s.Branch.Trim().ToLower() == company.Category.Trim().ToLower() &&
                    s.PassoutYear == company.EligiblePassoutYear &&
                    s.Backlogs <= company.AllowedBacklogs &&
                    HasAtLeastOneMatchingSkill(companySkills, s.Skills)
                )
                .ToList();

            return Ok(eligibleStudents);
        }

        // ✅ This helper is used in memory (after AsEnumerable)
        private bool HasAtLeastOneMatchingSkill(List<string> companySkills, string studentSkillsRaw)
        {
            if (string.IsNullOrEmpty(studentSkillsRaw))
                return false;

            var studentSkills = studentSkillsRaw
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(skill => skill.Trim().ToLower())
                .ToList();

            return companySkills.Intersect(studentSkills).Any();
        }
    }
}
