using Example.StudentsManagement.App_Start;
using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using Example.StudentsManagement.Models.Constants;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;


namespace Example.StudentsManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public List<Permissions> Permissions = new List<Permissions>();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            PermissionAuthorizationConfig.Configure();

            //For testing purpose
            InitialDataSetup();
        }

        /// <summary>
        /// Roles and users and permissions assigned to them can be changed through application.
        /// </summary>
        private void InitialDataSetup()
        {
            InMemoryRepository repository = new InMemoryRepository();

            var superUserRole = new ApplicationRole
            {
                Id = 1,
                Role = "Super User",
                Permissions = { AppPermissions.VIEW_STUDENT_PROFILES , AppPermissions.VIEW_OWN_ADMIN_PROFILE, AppPermissions.MANAGE_STUDENT_PROFILE , AppPermissions.VIEW_ADMINISTRATOR_PROFILES, AppPermissions.MANAGE_STUDENT_PROFILE, AppPermissions.MANAGE_PERMISSIONS, AppPermissions.MANAGE_ROLES, AppPermissions.MANAGE_ADMINISTRATOR_PROFILE }
            };
            var adminManagerRole = new ApplicationRole
            {
                Id = 2,
                Role = "Administrators Manager",
                Permissions = { AppPermissions.VIEW_ADMINISTRATOR_PROFILES, AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var adminManagerRole2 = new ApplicationRole
            {
                Id = 5,
                Role = "Administrators2",
                Permissions = { AppPermissions.VIEW_ADMINISTRATOR_PROFILES, AppPermissions.MANAGE_STUDENT_PROFILE , AppPermissions.VIEW_OWN_ADMIN_PROFILE, AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var studentsAdministratorRole = new ApplicationRole
            {
                Id = 3,
                Role = "Students Manager",
                Permissions = { AppPermissions.VIEW_OWN_ADMIN_PROFILE,  AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var teachingAssistantRole = new ApplicationRole
            {
                Id = 4,
                Role = "Teaching Assistant",
                Permissions = { AppPermissions.VIEW_STUDENT_PROFILES }
            };
            var studentRole = new ApplicationRole { Id = 5, Role = "Student", Permissions = { AppPermissions.VIEW_OWN_STUDENT_PROFILE } };

            repository.Add(superUserRole);
            repository.Add(adminManagerRole);
            repository.Add(studentsAdministratorRole);
            repository.Add(teachingAssistantRole);
            repository.Add(studentRole);

            var superuser = new ApplicationUser
            {
                Id = 1,
                Username = "superuser",
                Roles = { superUserRole, adminManagerRole, studentsAdministratorRole }
            };

            var superadmin = new ApplicationUser
            {
                Id = 2,
                Username = "superadmin",
                Roles = { adminManagerRole2, studentsAdministratorRole }
            };

            var superadmin2 = new ApplicationUser
            {
                Id = 5,
                Username = "superadmin2",
                Roles = { adminManagerRole2 }
            };
           

            var admin = new ApplicationUser
            {
                Id = 4,
                Username = "admin",
                Roles = { studentsAdministratorRole }
            };

            var student1 = new ApplicationUser
            {
                Id = 5,
                Username = "student1",   //This student is TA who can view other student's profiles
                Roles = { studentRole, teachingAssistantRole }
            };

            var student2 = new ApplicationUser
            {
                Id = 6,
                Username = "student2",
                Roles = { studentRole }
            };

            repository.Add(superuser);
            repository.Add(superadmin);
            repository.Add(superadmin2);
            repository.Add(admin);
            repository.Add(student1);
            repository.Add(student2);

            repository.Add(new Administrator { Id = 1, Name = "Bob Smith", User = superuser });
            repository.Add(new Administrator { Id = 2, Name = "Paul Smith", User = superadmin });
            repository.Add(new Administrator { Id = 3, Name = "Paul J. Smith", User = superadmin2 });
            repository.Add(new Administrator { Id = 4, Name = "Michael Sindhu", User = admin });
            repository.Add(new Student { Id = 5, Name = "Jeff Studants", User = student1 });
            repository.Add(new Student { Id = 6, Name = "Mitchel Studants", User = student2 });


            // adding all permissions
            superuser.Permissions.ToList().ForEach(x =>
            {

                Permissions.Add(new Permissions()
                {
                    Permission = x,
                    UserId = superuser.Id.ToString(),
                    Roles = superuser.Roles
                });
            });

            adminManagerRole.Permissions.ToList().ForEach(x =>
            {

                Permissions.Add(new Permissions()
                {
                    Permission = x,
                    UserId = adminManagerRole.Id.ToString()
                });
            });

            adminManagerRole2.Permissions.ToList().ForEach(x =>
            {

                Permissions.Add(new Permissions()
                {
                    Permission = x,
                    UserId = adminManagerRole2.Id.ToString()
                });
            });

            studentsAdministratorRole.Permissions.ToList().ForEach(x =>
            {

                Permissions.Add(new Permissions()
                {
                    Permission = x,
                    UserId = studentsAdministratorRole.Id.ToString()
                });
            });
            teachingAssistantRole.Permissions.ToList().ForEach(x =>
            {

                Permissions.Add(new Permissions()
                {
                    Permission = x,
                    UserId = teachingAssistantRole.Id.ToString()
                });
            });
            studentRole.Permissions.ToList().ForEach(x =>
            {

                Permissions.Add(new Permissions()
                {
                    Permission = x,
                    UserId = studentRole.Id.ToString()
                });
            });
            Permissions.ToList().ForEach(x =>
            {
                repository.Add(x);
            });
            
        }
    }
}
