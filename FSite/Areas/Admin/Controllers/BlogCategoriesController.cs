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
using FSite.Helpers;
using FSite.Areas.Admin.Models;
using Newtonsoft.Json;

namespace FSite.Areas.Admin.Controllers
{
   [Authorize(Roles = "Admin,Operator")]
    public class BlogCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/BlogCategories
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.BlogCategories.ToListAsync());
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from BlogCategories  where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and Title like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI"; }

            //if (!string.IsNullOrEmpty(model.Category))
            //{
            //    var category = db.BlogCategories.FirstOrDefault(c => c.Key == model.Category);
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
            var dt = db.BlogCategories.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
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


        // GET: Admin/BlogCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,Link,Index,IsActive,IsFeature")] BlogCategory blogCategory, string Files)
        {
            
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        blogCategory.Items.Add(new BlogCategoryItem() { Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }


              
                db.BlogCategories.Add(blogCategory);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = blogCategory.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewData["Files"] = Files;
            if (Request.IsAjaxRequest())
                return PartialView("_Field", blogCategory);

            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategory blogCategory = await db.BlogCategories.Include("Items").FirstOrDefaultAsync(i => i.Id == id);
            if (blogCategory == null)
            {
                return HttpNotFound();
            }
            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,Link,Index,IsActive,IsFeature")] BlogCategory blogCategory, string Files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        db.BlogCategoryItems.Add(new BlogCategoryItem() { CategoryId = blogCategory.Id, Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }


                
                db.Entry(blogCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = blogCategory.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewData["Files"] = Files;
            if (Request.IsAjaxRequest())
                return PartialView("_Field", blogCategory);
            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategory blogCategory = await db.BlogCategories.FindAsync(id);
            if (blogCategory == null)
            {
                return HttpNotFound();
            }
            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
          BlogCategory blogCategory = await db.BlogCategories.FindAsync(id);
            foreach (var img in blogCategory.Items)
            {
                FileDelete(img.ImageUrl);
            }
            db.BlogCategoryItems.RemoveRange(blogCategory.Items);

            db.BlogCategories.Remove(blogCategory);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = blogCategory.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        #region Function 
        void FileDelete(string filename)
        {
            IOHelpers.FileDelete(filename, this.RouteData.GetRequiredString("controller"));
        }
        #endregion
        #region Ajax

        [HttpPost]
        public JsonResult RemoveFile(long Id)
        {
            bool isOk = false;
            string msg = string.Empty;
            BlogCategoryItem dt = db.BlogCategoryItems.FirstOrDefault(x => x.Id == Id);
            if (dt == null)
                msg = Resources.R_Admin.ItemNotFound;
            else
            {
                try
                {
                    /* the second step, remove the image from local storage */
                    string filename = string.Empty;
                    if (!string.IsNullOrEmpty(dt.ImageUrl))
                        FileDelete(dt.ImageUrl);

                    db.BlogCategoryItems.Remove(dt);
                    db.SaveChanges();
                    msg = Resources.R_Admin.DelTrue;
                    isOk = true;
                }
                catch (Exception ex)
                {
                    //msg = ex.Message;
                    msg = Resources.R_Admin.DelFail;
                }
            }
            return Json(new { isOk, msg, Id }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActiveFile(long Id)
        {
            bool isOk = false;
            string msg = "";
            BlogCategoryItem dt = db.BlogCategoryItems.Where(x => x.Id == Id).FirstOrDefault();
            if (dt == null)
                msg = msg = Resources.R_Admin.ItemNotFound;
            else
            {
                try
                {
                    dt.IsActive = !dt.IsActive;
                    db.SaveChanges();
                    msg = dt.IsActive == true ? Resources.R_Admin.ActiveTrue : Resources.R_Admin.ActiveFail;
                    isOk = true;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return Json(new { isOk, msg, Id }, JsonRequestBehavior.AllowGet);
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
