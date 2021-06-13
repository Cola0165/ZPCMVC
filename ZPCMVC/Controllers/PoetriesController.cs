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
                return View(db.Poetrys.Where(r=>r.Location==location).ToList());
            }
            else
            {
                return View(db.Poetrys.ToList());
            }    
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
            return View(poetry);
        }

        // GET: Poetries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Poetries/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Dynasty,Poet,Cont")] Poetry poetry)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Dynasty,Poet,Cont")] Poetry poetry)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poetry poetry = db.Poetrys.Find(id);
            db.Poetrys.Remove(poetry);
            db.SaveChanges();
            return RedirectToAction("Index");
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
