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
                Role="District",
                //Role = "Students Manager",
                Permissions = { AppPermissions.VIEW_OWN_ADMIN_PROFILE,  AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var teachingAssistantRole = new ApplicationRole
            {
                Id = 4,
                
                Role = "School",
                //Role = "Teaching Assistant",
                Permissions = { AppPermissions.VIEW_STUDENT_PROFILES, AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var studentRole = new ApplicationRole 
            { 
                Id = 5, 
                Role="Class Room",
                //Role = "Student",
                Permissions = { AppPermissions.VIEW_OWN_STUDENT_PROFILE  } 
            };



            repository.Add(superUserRole);
            repository.Add(adminManagerRole);
            repository.Add(studentsAdministratorRole);
            repository.Add(teachingAssistantRole);
            repository.Add(studentRole);

            var superuser = new ApplicationUser
            {
                Id = 1,
                Username = "superuser"
            };

            var superadmin = new ApplicationUser
            {
                Id = 2,
                Username = "superadmin"
                
            };

            var superadmin2 = new ApplicationUser
            {
                Id = 5,
                Username = "superadmin2"
                
            };
           

            var admin = new ApplicationUser
            {
                Id = 4,
                Username = "admin"
                
            };

            var teacher = new ApplicationUser
            {
                Id = 5,
                Guid = "teacher",
                Username = "student1",   //This student is TA who can view other student's profiles
                
            };

            var student2 = new ApplicationUser
            {
                Id = 6,
                Username = "jared"//,
                //Roles = { studentRole }

            };

            var student3 = new ApplicationUser
            {
                Id = 7,
                Username = "ashok"

            };

            repository.Add(superuser);
            repository.Add(superadmin);
            repository.Add(superadmin2);
            repository.Add(admin);
            repository.Add(teacher);
            repository.Add(student2);
            repository.Add(student3);

            repository.Add(new Administrator { Id = 1, Name = "Bob Smith", User = superuser });
            repository.Add(new Administrator { Id = 2, Name = "Paul Smith", User = superadmin });
            repository.Add(new Administrator { Id = 3, Name = "Paul J. Smith", User = superadmin2 });
            repository.Add(new Administrator { Id = 4, Name = "Michael Sindhu", User = admin });
            //repository.Add(new Student { Id = 5, Guid = "AAAA", Name = "Jeff Studants", User = student1 });
            repository.Add(new Student { Id = 6, Guid = "AAAA", Name = "Jared", User = student2 });
            repository.Add(new Student { Id = 7, Guid = "BBBB", Name = "Ashok", User = student3 });

            // institutions
            var system = new Institution()
            {
                Guid = "0000000",
                ObjectType = "System",
                Level = 0
            };
            repository.Add(system);
            var scantron = new Institution()
            {
                Guid = "1111111111",
                ObjectType = "Scantron",
                ParentGuid = "0000000",
                Level = 1
            };
            repository.Add(scantron);
            var client1 = new Institution()
            {
                Guid = "ABC1",
                ObjectType = "District",
                ParentGuid = "0000000",
                Level = 1
            };
            repository.Add(client1);
            var School1 = new Institution()
            {
                Guid = "BCD2",
                ObjectType = "School",
                ParentGuid = "ABC1",
                Level = 2
            };
            repository.Add(School1);
            var Class1 = new Institution()
            {
                Guid = "FED4",
                ObjectType = "Classroom",
                ParentGuid = "BCD2",
                Level = 3
            };
            repository.Add(Class1);
            var Class2 = new Institution()
            {
                Guid = "JDH5",
                ObjectType = "SchClassroomool",
                ParentGuid = "BCD2",
                Level = 3
            };
            repository.Add(Class2);
            var School2 = new Institution()
            {
                Guid = "CDE3",
                ObjectType = "School",
                ParentGuid = "ABC1",
                Level = 2
            };
            repository.Add(School2);
            var studentSchoolAssociation = new StudentAssociation()
            {
                InstitutionGuid = "BCD2",
                StudentGuid = "AAAA"
            };
            repository.Add(studentSchoolAssociation);
            var studentClassroomAssociation = new StudentAssociation()
            {
                InstitutionGuid = "FED4",
                StudentGuid = "BBBB"
            };
            repository.Add(studentClassroomAssociation);
            var teacherSchoolPermissions = new UserPermissions()
            {
                InstitutionGuid = "BCD2",
                Roles = new List<ApplicationRole>(),
                Permissions = new List<string>(),
                UserGuid = "teacher"
            };
            teacherSchoolPermissions.Permissions.Add(AppPermissions.VIEW_STUDENT_PROFILES);
            teacherSchoolPermissions.Permissions.Add(AppPermissions.MANAGE_STUDENT_PROFILE);
            
            repository.Add(teacherSchoolPermissions);
        }
    }
}
