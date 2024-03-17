using Q2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SP24_PRN221Context _context = new SP24_PRN221Context();
        public MainWindow()
        {
            InitializeComponent();
            var deps = _context.Departments.ToList();
            var skills = _context.Skills.ToList();
            cbDeps.Items.Clear();
            foreach (var dep in deps)
            {
                cbDeps.Items.Add(dep.DepartmentName);
            }
            cbDeps.SelectedIndex = 0;

            foreach (var skill in skills)
            {
                lbSkills.Items.Add(skill.SkillName);
            }
            lbSkills.SelectionMode = SelectionMode.Multiple;
            cbPosition.Items.Clear();
            cbPosition.Items.Add("HR Manager");
            cbPosition.Items.Add("HR Specialist");
            cbPosition.Items.Add("Sales Representative");
            cbPosition.Items.Add("Sales Asociate");
            cbPosition.SelectedIndex = 0;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private Employee GetEmployee()
        {
            
            Employee e = new Employee();
            e.Name = txtName.Text;
            e.Dob = dpDob.SelectedDate;
            e.DepartmentId = int.Parse(cbDeps.SelectedIndex.ToString());
            e.Position = cbPosition.SelectedItem.ToString();
            e.HireDate = dpHireDate.SelectedDate;

            return e;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = GetEmployee();
            if (emp.Name.Length <= 3)
            {
                MessageBox.Show("Employee's name must be more than 3 characters.");
                return;
            }
            if (emp.HireDate > DateTime.Now)
            {
                MessageBox.Show("The employee's hire date must before the current date.");
                return;
            }
            List<string> skills = new List<string>();
            foreach (string item in lbSkills.SelectedItems)
            {
                skills.Add(item);
            }
            if (skills.Count == 0)
            {
                MessageBox.Show("You have not selected any skill.Do you want to add an employee without adding them to any skill");
                return;
            }
            _context.Employees.Add(emp);
            _context.SaveChanges();
            int empid = emp.EmployeeId;
            foreach (var item in skills)
            {
                Skill skill = _context.Skills.FirstOrDefault(s => s.SkillName.Equals(item));
                EmployeeSkill ek = new EmployeeSkill();
                ek.EmployeeId = empid;
                ek.SkillId = skill.SkillId;
                _context.EmployeeSkills.Add(ek);
            }
            _context.SaveChanges();
            if (cbManager.IsChecked == true)
            {
                Department d = _context.Departments.FirstOrDefault(d => d.DepartmentId == int.Parse(cbDeps.SelectedIndex.ToString()));
                d.ManagerId = empid;
                _context.Departments.Update(d);
                _context.SaveChanges();
            }
            MessageBox.Show("Adding successful!");

        }
    }
}
