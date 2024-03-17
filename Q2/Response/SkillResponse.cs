namespace Q2.Response
{
    public class SkillResponse
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public string? ProficiencyLevel { get; set; }
        public DateTime? AcquiredDate { get; set; }
        public string? Description { get; set; }
    }
}
