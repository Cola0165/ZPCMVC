using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZPCMVC.Models;

namespace ZPCMVC.Controllers
{
    public class PoetriesController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Poetries
        public ActionResult Index(string location)
        {
            if (location != null)
            {
                ViewBag.Title = location;
                return View(db.Poetrys.Where(r => r.Location == location).ToList());
            }
            else
            {
                ViewBag.Title = "诗词";
                return View(db.Poetrys.ToList());
            }    
        }
        public ActionResult Search(string search)
        {
            ViewBag.Search = search;
            return View(db.Poetrys.Where(r => r.Name.Contains(search) || r.Dynasty.Contains(search) || r.Poet.Contains(search) || r.Cont.Contains(search)).ToList());
        }
        // GET: Poetries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poetry poetry = db.Poetrys.Find(id);
            if (poetry == null)
            {
                return HttpNotFound();
            }
            ViewBag.Last = db.Poetrys.Find(id - 1)?.Name;
            ViewBag.Next = db.Poetrys.Find(id + 1)?.Name; 
            return View(poetry);
        }
        [Authorize(Roles = "Admin")]
        // GET: Poetries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Poetries/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Dynasty,Poet,Cont,Location")] Poetry poetry)
        {
            if (ModelState.IsValid)
            {
                db.Poetrys.Add(poetry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poetry);
        }

        // GET: Poetries/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poetry poetry = db.Poetrys.Find(id);
            if (poetry == null)
            {
                return HttpNotFound();
            }
            return View(poetry);
        }

        // POST: Poetries/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Dynasty,Poet,Cont,Location")] Poetry poetry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poetry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poetry);
        }

        // GET: Poetries/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poetry poetry = db.Poetrys.Find(id);
            if (poetry == null)
            {
                return HttpNotFound();
            }
            return View(poetry);
        }

        // POST: Poetries/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poetry poetry = db.Poetrys.Find(id);
            db.Poetrys.Remove(poetry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Music()
        {
            return View();
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
