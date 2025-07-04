namespace CollegePlacementAPI.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string RequiredSkills { get; set; }
        public decimal RequiredCGPA { get; set; }
        public string JobRole { get; set; }
        public int AllowedBacklogs { get; set; }
        public int EligiblePassoutYear { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
