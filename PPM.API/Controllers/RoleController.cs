using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PPM.Domain;
using PPM.Model;

namespace PPM.API
{

[Route("api/Roles")]
[ApiController]
public class RoleController : ControllerBase
{
    RoleRepo roleRepo = new();
    ValidationCheck validationCheck = new();
    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = roleRepo!.ListAllRoles();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public IActionResult GetRole(int id)
    {
        var role = roleRepo!.GetRole(id);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    [HttpPost]
    public IActionResult CreateRole(int id, string name)
    {
        if(!validationCheck.IsIdValid(id) || validationCheck.IsRoleIdValid(id))
        {
            return new BadRequestObjectResult("You can't create a Role with this Id or This Role Id is already Present");
        }

        if(!validationCheck.IsValidName(name))
        {
            return new BadRequestObjectResult("Invalid Name");
        }

        Role role = new();

        role.RoleId =  id;
        role.RoleName = name;

        roleRepo!.AddRole(role);
        return Content("Role Added Successfuly");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRole(int id)
    {
        var role = roleRepo!.GetRole(id);
        if (role == null)
        {
            return NotFound();
        }
        if(validationCheck.RoleInProject(id))
        {
            return new BadRequestObjectResult("Role is already in use You can't delete it");
        }
        roleRepo.DeleteRole(id);
        return Content("Role Deleted Successfuly");
    }
}
}