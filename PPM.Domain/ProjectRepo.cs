using PPM.Model;

namespace PPM.Domain
{
    public class ProjectRepo : IProjectRepo
    {
        public void AddProject(Project project)
        {
            using (var context = new DbContextFile())
            {
                context.Projects.Add(project);
                context.SaveChanges();
            }
        }

        public List<Project> ListAllProjectsWithoutEmployeeDetails()
        {
            using (var context = new DbContextFile())
            {
                return context.Projects.ToList();
            }
        }

        public void DeleteProject(int projectId)
        {
            using (var context = new DbContextFile())
            {
                var projectValid = context.Projects.FirstOrDefault(e => e.ProjectId == projectId)!;
                context.Projects.Remove(projectValid);
                context.SaveChanges();
            }
        }

        public void AddEmployeeToExistingProject(int projectId, int employeeId)
        {
            using (var context = new DbContextFile())
            {
                var employeeDetails = context.Employees.Find(employeeId)!;
                employeeDetails.ProjectId = projectId;
                context.SaveChanges();
            }
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            using (var context = new DbContextFile())
            {
                var employeeDetails = context.Employees.Find(employeeId)!;
                employeeDetails.ProjectId = 0;
                context.SaveChanges();
            }
        }

        public Project ViewProjectDetail(int projectId)
        {
            using (var context = new DbContextFile())
            {
                var projectDetails = context.Projects.Find(projectId)!;
                if (projectDetails != null)
                {
                    projectDetails.ProjectEmployees = context.Employees.Where(e => e.ProjectId == projectId).ToList();
                }
                return projectDetails!;
            }
        }
    }
}
