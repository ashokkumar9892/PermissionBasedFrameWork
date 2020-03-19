using System.Collections.Generic;

namespace Example.StudentsManagement.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// Set of roles assigned to a user.
        /// Application should provide capability to assign or remove roles of a user.
        /// </summary>
        public ICollection<ApplicationRole> Roles { get; set; }

        public List<string> Permissions { get; set; }

        public ApplicationUser()
        {
            Roles = new List<ApplicationRole>();
            Permissions = new List<string>();
        }
    }

    public class Permissions
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string UserId { get; set; }

        public string Permission { get; set; }
        /// <summary>
        /// Set of roles assigned to a user.
        /// Application should provide capability to assign or remove roles of a user.
        /// </summary>
        public ICollection<ApplicationRole> Roles { get; set; }

        public Permissions()
        {
            Roles = new List<ApplicationRole>();
        }
    }
}