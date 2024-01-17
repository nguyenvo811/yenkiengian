using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FSite.Models;
using FSite.Models.Data;
using FSite.Areas.Admin.Models;
using FSite.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace FSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class FaqsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Faqs
        //public async Task<ActionResult> Index()
        //{
        //    var Faqs = db.Faqs.Include(b => b.Category);
        //    return View(await Faqs.ToListAsync());
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from Faqs  where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and Title like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI"; }

            //if (!string.IsNullOrEmpty(model.Category))
            //{
            //    var category = db.FaqCategories.FirstOrDefault(c => c.Key == model.Category);
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
            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Faqs.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            data.data = from d in dt
                        from s in ids
                        where s.Id == d.Id
                        orderby s.No
                        select new
                        {
                            d.Id,
                            d.Title,
                            d.CreatedDate,
                            d.IsFeature,
                            d.IsActive,
                         
                            Category = d.Category != null ? new SubItem { Id = d.Category.Id, Title = d.Category.Title } : new SubItem()
                        };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
  

        // GET: Admin/Faqs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.FaqCategories, "Id", "Title");
           // ViewBag.Files = "[{\"fileName\":\"hung-cay-xang-vuong-quyen.jpg\",\"reName\":\"Faqs-176f1d5-hung-cay-xang-vuong-quyen.jpg\",\"largePicPath\":\"/Uploads/images/Faqs/Faqs-176f1d5-hung-cay-xang-vuong-quyen.jpg\",\"smallPicPath\":\"/Uploads/thumbs/Faqs/Faqs-176f1d5-hung-cay-xang-vuong-quyen.jpg\",\"size\":57915},{\"fileName\":\"xe-nha-sai-gon-1.jpg\",\"reName\":\"Faqs-46d9735-xe-nha-sai-gon-1.jpg\",\"largePicPath\":\"/Uploads/images/Faqs/Faqs-46d9735-xe-nha-sai-gon-1.jpg\",\"smallPicPath\":\"/Uploads/thumbs/Faqs/Faqs-46d9735-xe-nha-sai-gon-1.jpg\",\"size\":63863}]";
            return View();
        }

        // POST: Admin/Faqs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,CategoryId,Detail,Link,ImageUrl,IsActive,IsFeature,Viewed,CreatedDate,CreatedById")] Faq Faq,string Files)
        {
            if (ModelState.IsValid)
            {
               
                #region Sys
                
                if (!Faq.Viewed.HasValue)
                    Faq.Viewed = 1;
                Faq.CreatedById = User.Identity.GetUserId();
                Faq.CreatedDate = DateTime.Now;
                #endregion
                db.Faqs.Add(Faq);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result=true,Id = Faq.Id},JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.FaqCategories, "Id", "Title", Faq.CategoryId);
            ViewData["Files"] = Files;
            if (Request.IsAjaxRequest())
                return PartialView("_Field", Faq);
            return View(Faq);
        }

        // GET: Admin/Faqs/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq Faq = await db.Faqs.FirstOrDefaultAsync(i=>i.Id== id);
            if (Faq == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.FaqCategories, "Id", "Title", Faq.CategoryId);
            return View(Faq);
        }

        // POST: Admin/Faqs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,CategoryId,Detail,Link,IsActive,IsFeature,Viewed,CreatedDate,CreatedById")] Faq Faq, string Files)
        {
            if (ModelState.IsValid)
            {
              
                #region Sys

                if (!Faq.Viewed.HasValue)
                    Faq.Viewed = 1;
                ModelState.Remove("CreatedById");
                ModelState.Remove("CreatedDate");
                Faq.ModifiedById = User.Identity.GetUserId();
                Faq.ModifiedDate = DateTime.Now;
                #endregion

                db.Entry(Faq).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = Faq.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.FaqCategories, "Id", "Title", Faq.CategoryId);
            ViewData["Files"] = Files;
            if (Request.IsAjaxRequest())
                return PartialView("_Field", Faq);
            return View(Faq);
        }
        // POST: Admin/Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
     //   [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Faq o = await db.Faqs.FindAsync(id);
            db.Faqs.Remove(o);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = o.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        #region Function 
        void FileDelete(string filename)
        {
            IOHelpers.FileDelete(filename, this.RouteData.GetRequiredString("controller"));//Faqs = controlerName
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
