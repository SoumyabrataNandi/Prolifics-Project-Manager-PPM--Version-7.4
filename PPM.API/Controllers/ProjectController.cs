using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PPM.Domain;
using PPM.Model;

namespace PPM.API
{

[Route("api/Projects")]
[ApiController]
public class ProjectController : ControllerBase
{
    ProjectRepo projectRepo = new();
    ValidationCheck validationCheck = new();
    [HttpGet]
    public IActionResult GetProjects()
    {
        var projects = projectRepo.ListAllProjectsWithoutEmployeeDetails();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public IActionResult GetProject(int id)
    {
        var project = projectRepo.ViewProjectDetail(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpPost]
    public IActionResult CreateProject(int projectId, string projectName, DateTime projectStartDate, DateTime projectEndDate)
    {
        if(!validationCheck.IsIdValid(projectId) || validationCheck.ProjectExists(projectId))
        {
            return new BadRequestObjectResult("You can't create a Project with this Id or This Project Id is already Present");
        }
        if(!validationCheck.IsValidName(projectName))
        {
            return new BadRequestObjectResult("Invalid Project Name");
        }
        if(projectEndDate < projectStartDate)
        {
            return new BadRequestObjectResult("Project End Date should be greater than Project Start Date");
        }
        Project project = new Project()
        {
            ProjectId = projectId,
            ProjectName = projectName,
            ProjectStartDate = projectStartDate,
            ProjectEndDate = projectEndDate
        };
        projectRepo.AddProject(project);
        return Content("Project Added Successfuly");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var project = projectRepo!.ViewProjectDetail(id);
        if (project == null)
        {
            return NotFound();
        }
        projectRepo.DeleteProject(id);
        return Content("Project Deleted Successfuly");
    }
    [HttpPost("{projectId}/employees/{employeeId}")]
    public IActionResult AddEmployeeToProject(int projectId, int employeeId)
    {
        if(!validationCheck.ProjectExists(projectId))
        {
            return new BadRequestObjectResult("This Project Id is not Present");
        }
        if(validationCheck.EmployeeInProject(employeeId))
        {
            return new BadRequestObjectResult("Employee is Already in this Project");
        }
        projectRepo.AddEmployeeToExistingProject(projectId, employeeId);
        return Content("Employee Added To Project Successfuly");
    }

    [HttpDelete("{projectId}/employees/{employeeId}")]
    public IActionResult DeleteEmployeeFromProject(int projectId, int employeeId)
    {
        if(!validationCheck.ProjectExists(projectId))
        {
            return new BadRequestObjectResult("This Project Id is not Present");
        }
        if(!validationCheck.EmployeeInProject(employeeId))
        {
            return new BadRequestObjectResult("Employee is not in this Project");
        }
        projectRepo.DeleteEmployeeFromProject(projectId, employeeId);
        return Content("Employee Deleted From Project Successfuly");
    }
}
}