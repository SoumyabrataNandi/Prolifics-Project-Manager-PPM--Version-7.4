using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PPM.Domain;
using PPM.Model;

namespace PPM.API
{

[Route("api/Employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    EmployeeRepo employeeRepo = new();
    ValidationCheck validationCheck = new();
    [HttpGet]
    public IActionResult GetEmployees()
    {
        var projects = employeeRepo!.ListAllEmployees();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        var employee = employeeRepo!.GetEmployee(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployee(int id, string firstName, string lastName, string address, string email, long mobileNumber,int roleId)
    {
        if(!validationCheck.IsIdValid(id) || validationCheck.EmployeeExists(id))
        {
            return new BadRequestObjectResult("You can't create a Employee with this Id or This Employee Id is already Present");
        }
        if(!validationCheck.IsValidName(firstName))
        {
            return new BadRequestObjectResult("Invalid First Name");
        }
        if(!validationCheck.IsValidName(lastName))
        {
            return new BadRequestObjectResult("Invalid Last Name");
        }
        if(!validationCheck.IsValidEmail(email))
        {
            return new BadRequestObjectResult("...Invalid Email Format... Please Enter Valid Email...");
        }
        if(!validationCheck.IsValidMobileNumber(mobileNumber.ToString()))
        {
            return new BadRequestObjectResult("...Invalid Mobile Number Format... Please Enter Valid Mobile Number...");
        }
        if(!validationCheck.IsValidName(address))
        {
            return new BadRequestObjectResult("Please Don't Put Address as Blank ");
        }
        if(!validationCheck.IsRoleIdValid(roleId))
        {
            return new BadRequestObjectResult("...Invalid Role Id... Please Enter Valid Role Id...");
        }

        Employee employee = new();
        
        employee.EmployeeId = id;
        employee.EmployeeFirstName = firstName;
        employee.EmployeeLastName = lastName;
        employee.EmployeeAddress = address;
        employee.EmployeeEmail = email;
        employee.EmployeeMobileNumber = mobileNumber;
        employee.EmployeeRoleId = roleId;

        employeeRepo!.AddEmployee(employee);
        return Content("Employee Added Successfuly");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = employeeRepo!.GetEmployee(id);
        if (employee == null)
        {
            return NotFound();
        }
        if(validationCheck.EmployeeInProject(id))
        {
            return new BadRequestObjectResult("Employee is already in Project You can't delete it");
        }
        employeeRepo.DeleteEmployee(id);
        return Content("Employee Deleted Successfuly");
    }
}
}