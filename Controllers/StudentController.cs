using CollegePlacementAPI.Data;
using CollegePlacementAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace CollegePlacementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ------------------------
        // GET: api/Student
        // ------------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        // ------------------------
        // POST: api/Student
        // ------------------------
        [HttpPost]
        public IActionResult Add([FromForm] Student student, IFormFile Resume)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var state in ModelState)
                {
                    var key = state.Key;
                    var errors = state.Value.Errors;
                    foreach (var error in errors)
                    {
                        System.Console.WriteLine($"{key}: {error.ErrorMessage}");
                    }
                }

                return BadRequest(ModelState);
            }

            if (Resume != null)
            {
                string uploads = Path.Combine(_env.ContentRootPath, "Uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                string filePath = Path.Combine(uploads, Resume.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Resume.CopyTo(stream);
                }

                student.ResumePath = $"Uploads/{Resume.FileName}";
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }

        // ------------------------
        // PUT: api/Student/{id}
        // ------------------------
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student updated)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            student.Name = updated.Name;
            student.Branch = updated.Branch;
            student.CGPA = updated.CGPA;
            student.Phone = updated.Phone;
            student.Email = updated.Email;
            student.Skills = updated.Skills;
            student.Backlogs = updated.Backlogs;
            student.PassoutYear = updated.PassoutYear;

            _context.SaveChanges();
            return Ok(student);
        }
    }
}
