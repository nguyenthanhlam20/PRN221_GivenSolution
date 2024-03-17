using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;
using Q2.Response;

namespace Q2.Pages.Employee
{
    public class ListModel : PageModel
    {
        private readonly SP24_PRN221Context _context;

        public ListModel(SP24_PRN221Context context)
        {
            _context = context;
        }

        public List<EmployeeResponse> employees = new List<EmployeeResponse>();
        public Models.Employee? employee = new();
        public List<Department> departments = new List<Department>();
        public List<SkillResponse> skills = new List<SkillResponse>();
        public int DepartmentId = 0;
        public void OnGet(int departmentId, int employeeId)
        {
            departments = _context.Departments.ToList();
            DepartmentId = departmentId;

            skills = (from emp in _context.Employees
                                  join de in _context.EmployeeSkills on emp.EmployeeId equals de.EmployeeId
                                  join skil in _context.Skills on de.SkillId equals skil.SkillId
                                  where emp.EmployeeId == employeeId
                                  select new SkillResponse
                                  {
                                     SkillId = skil.SkillId,
                                     SkillName = skil.SkillName,
                                     ProficiencyLevel = de.ProficiencyLevel,
                                     AcquiredDate = de.AcquiredDate,
                                  }).ToList();
            employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);

            employees = (from emp in _context.Employees
                         join de in _context.Departments on emp.DepartmentId equals de.DepartmentId
                         select new EmployeeResponse
                         {
                             EmployeeId = emp.EmployeeId,
                             DepartmentId = de.DepartmentId,
                             EmployeeName = emp.Name,
                             DateOfBirth = emp.Dob,
                             Department = de.DepartmentName,
                             Position = emp.Position,
                             HireDate = emp.HireDate,
                         }).ToList();

            if (departmentId > 0)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId).ToList();
            }
        }
    }
}
