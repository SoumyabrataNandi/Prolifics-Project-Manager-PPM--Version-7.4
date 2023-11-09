using PPM.Model;


namespace PPM.Domain
{
    public class RoleRepo : IRoleRepo
    {
        public void AddRole(Role role)
        {
            using (var context = new DbContextFile())
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }

        public List<Role> ListAllRoles()
        {
            using (var context = new DbContextFile())
            {
                return context.Roles.ToList();
            }
        }

        public Role GetRole(int roleId)
        {
            using (var context = new DbContextFile())
            {
                return context.Roles.FirstOrDefault(e => e.RoleId == roleId)!;
            }
        }

        public void DeleteRole(int roleId)
        {
            using (var context = new DbContextFile())
            {
                var roleValid = context.Roles.FirstOrDefault(e => e.RoleId == roleId)!;
                context.Roles.Remove(roleValid);
                context.SaveChanges();
            }
        }
    }
}
