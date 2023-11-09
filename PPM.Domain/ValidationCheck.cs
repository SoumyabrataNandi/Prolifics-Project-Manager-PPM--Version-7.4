using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace PPM.Domain
{
    public class ValidationCheck
    {
        ConnectionString connectionString = new();
        public bool IsValidEmail(string email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(email, pattern);
        }

        public bool IsValidMobileNumber(string mobileNumber)
        {
            string pattern = @"^\d{10}$";

            return Regex.IsMatch(mobileNumber, pattern);
        }

        public bool EmployeeExists(int employeeId)
        {
            using (var context = new DbContextFile())
            {
                return context.Employees.Any(e => e.EmployeeId == employeeId);
            }
        }

        public bool ProjectExists(int projectId)
        {
            using (var context = new DbContextFile())
            {
                return context.Projects.Any(p => p.ProjectId == projectId);
            }
        }

        public bool IsRoleIdValid(int roleId)
        {
            using (var context = new DbContextFile())
            {
                return context.Roles.Any(r => r.RoleId == roleId);
            }
        }

        public bool IsValidName(string name)
        {
            if (name == null)
            {
                return false;
            }
            return true;
        }

        public bool IsIdValid(int id)
        {
            if (id ==0 || id < 0)
            {
                return false;
            }
            return true;
        }

        public bool EmployeeInProject(int employeeId)
        {
            using (var context = new DbContextFile())
            {
                var employeeValid = context.Employees.Find(employeeId)!;
                if (employeeValid == null)
                {
                    return false;
                }
                var projectId = employeeValid.ProjectId;
                return context.Projects.Any(p => p.ProjectId == projectId);
            }
        }

        public bool RoleInProject(int roleId)
        {
            using (var context = new DbContextFile())
            {
                var roleValid = context.Employees.FirstOrDefault(e => e.EmployeeRoleId == roleId)!;
                if (roleValid != null)
                {
                    var projectId = roleValid.ProjectId;
                    return context.Projects.Any(p => p.ProjectId == projectId);
                }

            }
            return false;
        }
    }
}
