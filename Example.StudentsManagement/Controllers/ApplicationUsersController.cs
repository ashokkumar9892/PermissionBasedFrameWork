using System.Linq;
using System.Net;
using System.Web.Mvc;
using Example.StudentsManagement.Models;
using Example.StudentsManagement.DAL;
using AspNetMvc.Authorization.PermissionBased;
using System.Threading.Tasks;

namespace Example.StudentsManagement.Controllers
{
    [Authorize]
    [AuthorizePermission(new string[] { AppPermissions.MANAGE_USER })]
    public class ApplicationUsersController : Controller
    {
        private InMemoryRepository db = new InMemoryRepository();
        
        public async Task<ActionResult> Index()
        {
            return View(db.GetAll<ApplicationUser>());
        }        

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.GetAll<ApplicationUser>().FirstOrDefault(u => u.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Username")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                applicationUser.Id = db.GetAll<ApplicationUser>().Select(a => a.Id).Max() + 1;
                db.Add(applicationUser);
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.GetAll<ApplicationUser>().FirstOrDefault(u => u.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Username")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var user =   db.GetAll<ApplicationUser>().FirstOrDefault(u => u.Id == applicationUser.Id);
                if(user != null)
                {
                    user.Username = applicationUser.Username;
                }
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.GetAll<ApplicationUser>().FirstOrDefault(u => u.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ApplicationUser applicationUser = db.GetAll<ApplicationUser>().FirstOrDefault(u => u.Id == id);
            db.Remove(applicationUser);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
