using System;
using System.Collections.Generic;

namespace Q2.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
            EmployeeProjects = new HashSet<EmployeeProject>();
            EmployeeSkills = new HashSet<EmployeeSkill>();
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public int? DepartmentId { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
