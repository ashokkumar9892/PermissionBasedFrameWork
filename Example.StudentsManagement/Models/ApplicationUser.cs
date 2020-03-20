using System.Collections.Generic;

namespace Example.StudentsManagement.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// Set of roles assigned to a user.
        /// Application should provide capability to assign or remove roles of a user.
        /// </summary>
/*        public ICollection<ApplicationRole> Roles { get; set; }

        public ApplicationUser()
        {
            Roles = new List<ApplicationRole>(); /// Role is nothing but access level like School 1 /School2 , ClassRoom A ClassRoom B
        }*/
    }

    public class UserPermissions
    {
        public string UserGuid;
        public string InstitutionGuid;
        public List<ApplicationRole> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
 
}