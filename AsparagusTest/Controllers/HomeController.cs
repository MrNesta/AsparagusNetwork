using AsparagusTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsparagusTest.Controllers
{
    public class HomeController : Controller
    {
        UserContext db = new UserContext("DefaultConnection");

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user, bool eaten)
        {
            List<User> users = db.Users.ToList();
            List<Food> foods = db.Foods.ToList();

            foreach (var u in users)
            {
                if (u.Email == user.Email & u.Name == user.Name)
                {
                    if (eaten)
                    {
                        db.Foods.Add(new Food()
                        {
                            EatingDate = DateTime.Now,
                            UserId = u.Id,
                        });

                        db.SaveChanges();
                    }
                    return RedirectToAction("List");
                }
            }

            db.Users.Add(new User()
            {
                Name = user.Name,
                Email = user.Email
            });

            if (eaten)
            {
                db.Foods.Add(new Food()
                {
                    EatingDate = DateTime.Now,
                    UserId = db.Users.Count()
                });
            }

            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var users = from user in db.Users.ToList()
                        join food in db.Foods.ToList() on user.Id equals food.UserId
                        select user;
            return View(users.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}