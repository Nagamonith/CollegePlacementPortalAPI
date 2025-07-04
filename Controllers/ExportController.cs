using ClosedXML.Excel;
using CollegePlacementAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ExportController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ExportController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("eligible/{companyId}")]
    public IActionResult ExportEligible(int companyId)
    {
        var company = _context.Companies.Find(companyId);
        if (company == null) return NotFound("Company not found");

        var eligible = _context.Students.ToList().Where(s =>
            s.CGPA >= company.RequiredCGPA &&
            s.Backlogs <= company.AllowedBacklogs &&
            s.PassoutYear == company.EligiblePassoutYear &&
            company.RequiredSkills.Split(',').All(skill => s.Skills.Contains(skill.Trim()))
        ).ToList();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Eligible Students");
        worksheet.Cell(1, 1).Value = "Name";
        worksheet.Cell(1, 2).Value = "Branch";
        worksheet.Cell(1, 3).Value = "CGPA";
        worksheet.Cell(1, 4).Value = "Email";
        worksheet.Cell(1, 5).Value = "Phone";

        for (int i = 0; i < eligible.Count; i++)
        {
            worksheet.Cell(i + 2, 1).Value = eligible[i].Name;
            worksheet.Cell(i + 2, 2).Value = eligible[i].Branch;
            worksheet.Cell(i + 2, 3).Value = eligible[i].CGPA;
            worksheet.Cell(i + 2, 4).Value = eligible[i].Email;
            worksheet.Cell(i + 2, 5).Value = eligible[i].Phone;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "eligible_students.xlsx");
    }
}