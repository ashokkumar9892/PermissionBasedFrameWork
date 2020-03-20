using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace Example.StudentsManagement.PermissionBasedAuthorization
{
    public class MyPermissionsProvider
    {
        public static List<string> GetPermissions(string username)
        {
            InMemoryRepository repository = new InMemoryRepository();
            var user = repository.GetAll<Student>().Where(u => u.User.Username == username).First();
            var institution = repository.GetAll<StudentAssociation>().Where(u => u.StudentGuid == user.Guid).First();
            var permission = repository.GetAll<UserPermissions>().Where(u => u.InstitutionGuid == institution.InstitutionGuid).ToList();
            var permissions = institution != null ? permission.SelectMany(r => r.Permissions).ToList() : new List<string>();
            //var permissions = user != null ? user.Roles.SelectMany(r => r.Permissions).ToList() : new List<string>();
            //return permissions;
            return permissions;
        }

        
    }
}