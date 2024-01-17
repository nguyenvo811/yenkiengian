using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSite.Models;
using FSite.Models.Data;
using Newtonsoft.Json;
using FSite.Areas.Admin.Models;
using FSite.Helpers;

namespace FSite.Areas.Admin.Controllers
{
   [Authorize(Roles = "Admin,Operator")]
    public class ProductBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/BrandCategories
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.BrandCategories.ToListAsync());
        //}
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Get()
        {
            var request = Request;
            var draw = request.Form.GetValues("draw").FirstOrDefault();
            var start = request.Form.GetValues("start").FirstOrDefault();
            var length = request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumnIndex = request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortColumn = request.Form.GetValues("columns[" + sortColumnIndex + "][data]").FirstOrDefault();
            var sortColumnDir = request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = request.Form.GetValues("search[value]").FirstOrDefault();
            //var searchRegex = request.Form.GetValues("search[regex]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "[Id]";//set default column sort
                sortColumnDir = "desc";
            }

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from ProductBrands  where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and Title like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI"; }

            //if (!string.IsNullOrEmpty(model.Category))
            //{
            //    var category = db.BrandCategories.FirstOrDefault(c => c.Key == model.Category);
            //    if (category != null)
            //    {
            //        ViewData["category"] = category;
            //        query += " and b.[CategoryId]=" + category.Id;
            //    }
            //}

            query += ") AS b";
            //----------for paging

            DatatablesResultModels data = new DatatablesResultModels();

            data.draw = int.Parse(draw);
            data.recordsTotal = db.Database.SqlQuery<int>(query.Replace("@columns", "count(Id)")).FirstOrDefault();
            data.recordsFiltered = data.recordsTotal;

            //take is pagesize , skip is from
            int pfrom = skip + 1;
            int pto = pfrom + pageSize - 1;

            query += $" where No >= {pfrom} AND No <= {pto} order by No";
            var ids = db.Database.SqlQuery<DataResultModels>(query.Replace("@columns", "cast(No as int) No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.ProductBrands.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            data.data = from d in dt
                        from s in ids
                        where s.Id == d.Id
                        orderby s.No
                        select new
                        {
                            d.Id,
                            d.Title,
                            d.Index,
                            d.IsFeature,
                            d.IsActive
                        };

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        // GET: Admin/ServiceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BrandCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,Link,Index,IsActive,IsFeature")] ProductBrand ProductBrand)
        {
            
            if (ModelState.IsValid)
            {
             

                db.ProductBrands.Add(ProductBrand);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = ProductBrand.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
      
            if (Request.IsAjaxRequest())
                return PartialView("_Field", ProductBrand);

            return View(ProductBrand);
        }

        // GET: Admin/BrandCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand ProductBrand = await db.ProductBrands.FirstOrDefaultAsync(i => i.Id == id);
            if (ProductBrand == null)
            {
                return HttpNotFound();
            }
            return View(ProductBrand);
        }

        // POST: Admin/BrandCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,Link,Index,IsActive,IsFeature")] ProductBrand ProductBrand)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(ProductBrand).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = ProductBrand.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
         
            if (Request.IsAjaxRequest())
                return PartialView("_Field", ProductBrand);
            return View(ProductBrand);
        }

        // GET: Admin/BrandCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand ProductBrand = await db.ProductBrands.FindAsync(id);
            if (ProductBrand == null)
            {
                return HttpNotFound();
            }
            return View(ProductBrand);
        }

        // POST: Admin/BrandCategories/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
          ProductBrand ProductBrand = await db.ProductBrands.FirstOrDefaultAsync(i => i.Id == id);
           
           
            db.ProductBrands.Remove(ProductBrand);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = ProductBrand.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        #region Function 
        void FileDelete(string filename)
        {
            IOHelpers.FileDelete(filename, this.RouteData.GetRequiredString("controller"));
        }
        #endregion
       
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
