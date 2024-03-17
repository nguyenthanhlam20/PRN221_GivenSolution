namespace Q2.Response
{
    public class EmployeeResponse
    {
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
