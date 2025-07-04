namespace CollegePlacementAPI.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string Name { get; set; }
        public required string Branch { get; set; }
        public decimal CGPA { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Skills { get; set; }
        public int Backlogs { get; set; }
        public int PassoutYear { get; set; }
        public string? ResumePath { get; set; }
        // Added 'required' modifier to fix CS8618  
    }
}
