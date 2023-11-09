using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeRepo : IEmployeeRepo
    {
        public void AddEmployee(Employee employee)
        {
            using (var context = new DbContextFile())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        public List<Employee> ListAllEmployees()
        {
            using (var context = new DbContextFile())
            {
                return context.Employees.ToList();
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            using (var context = new DbContextFile())
            {
                return context.Employees.Find(employeeId)!;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (var context = new DbContextFile())
            {
                var employeeValid = context.Employees.Find(employeeId)!;
                context.Employees.Remove(employeeValid);
                context.SaveChanges();
            }
        }
    }
}
