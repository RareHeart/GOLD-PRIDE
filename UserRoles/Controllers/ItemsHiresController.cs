using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class ItemsHiresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString)
        {
            var itemHires = db.ItemsHires.Include(i => i.Category).Include(i => i.ProductType);
            if ((!string.IsNullOrEmpty(searchString)))
            {
                itemHires = itemHires.Where(s => s.ProductName.Contains(searchString));
            }
            return View(itemHires.ToList());
        }
        // GET: ItemsHires
        
        public ActionResult ItemsInventory( string searchString)
        { 
            var list = db.ItemsHires.Take(10);
            //search code test
            if ((!string.IsNullOrEmpty(searchString)))
            {
                list = list.Where(s => s.ProductName.Contains(searchString) );
            }
            return View(list.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ItemsInventoryAdmin(string searchString)
        {
            var list = db.ItemsHires.Take(10);
            if ((!string.IsNullOrEmpty(searchString)))
            {
                list = list.Where(s => s.ProductName.Contains(searchString));
            }
            return View(list.ToList());
        }
        // GET: ItemsHires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsHire itemsHire = db.ItemsHires.Find(id);
            if (itemsHire == null)
            {
                return HttpNotFound();
            }
            return View(itemsHire);
        }

        // GET: ItemsHires/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Category_Name");
            ViewBag.ProductCategoryID = new SelectList(db.ProductTypes, "ProductCategoryId", "ProductCategory_Name");
            return View();
        }

        // POST: ItemsHires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,ProductCategoryId,CategoryId,Quantity,Price,Image,ImageType")] ItemsHire itemsHire, HttpPostedFileBase image2)
        {
            if (image2 != null)
            {
                itemsHire.Image = new byte[image2.ContentLength];
                image2.InputStream.Read(itemsHire.Image, 0, image2.ContentLength);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please add image");
            }
            if (ModelState.IsValid)
            {
                db.ItemsHires.Add(itemsHire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Category_Name", itemsHire.CategoryId);
            ViewBag.ProductCategoryID = new SelectList(db.ProductTypes, "ProductCategoryId", "ProductCategory_Name", itemsHire.ProductCategoryId);
            return View(itemsHire);
        }

        // GET: ItemsHires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsHire itemsHire = db.ItemsHires.Find(id);
            if (itemsHire == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Category_Name", itemsHire.CategoryId);
            ViewBag.ProductCategoryID = new SelectList(db.ProductTypes, "ProductCategoryId", "ProductCategory_Name", itemsHire.ProductCategoryId);
            return View(itemsHire);
        }

        // POST: ItemsHires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Description,ProductCategoryId,CategoryId,Quantity,Price,Image,ImageType")] ItemsHire itemsHire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemsHire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Category_Name", itemsHire.CategoryId);
            ViewBag.ProductCategoryID = new SelectList(db.ProductTypes, "ProductCategoryId", "ProductCategory_Name", itemsHire.ProductCategoryId);
            return View(itemsHire);
        }

        // GET: ItemsHires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsHire itemsHire = db.ItemsHires.Find(id);
            if (itemsHire == null)
            {
                return HttpNotFound();
            }
            return View(itemsHire);
        }

        // POST: ItemsHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemsHire itemsHire = db.ItemsHires.Find(id);
            db.ItemsHires.Remove(itemsHire);
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
