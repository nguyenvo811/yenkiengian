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
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Blogs
        //public async Task<ActionResult> Index()
        //{
        //    var blogs = db.Blogs.Include(b => b.Category);
        //    return View(await blogs.ToListAsync());
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from Blogs  where 1=1 ";
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
            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Blogs.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            data.data = from d in dt
                        from s in ids
                        where s.Id == d.Id
                        orderby s.No
                        select new
                        {
                            d.Id,
                            d.ImageUrl,
                            d.Title,
                            d.CreatedDate,
                            d.IsFeature,
                            d.IsActive,
                         
                            Category = d.Category != null ? new SubItem { Id = d.Category.Id, Title = d.Category.Title } : new SubItem()
                        };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
  

        // GET: Admin/Blogs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.BlogCategories, "Id", "Title");
           // ViewBag.Files = "[{\"fileName\":\"hung-cay-xang-vuong-quyen.jpg\",\"reName\":\"blogs-176f1d5-hung-cay-xang-vuong-quyen.jpg\",\"largePicPath\":\"/Uploads/images/blogs/blogs-176f1d5-hung-cay-xang-vuong-quyen.jpg\",\"smallPicPath\":\"/Uploads/thumbs/blogs/blogs-176f1d5-hung-cay-xang-vuong-quyen.jpg\",\"size\":57915},{\"fileName\":\"xe-nha-sai-gon-1.jpg\",\"reName\":\"blogs-46d9735-xe-nha-sai-gon-1.jpg\",\"largePicPath\":\"/Uploads/images/blogs/blogs-46d9735-xe-nha-sai-gon-1.jpg\",\"smallPicPath\":\"/Uploads/thumbs/blogs/blogs-46d9735-xe-nha-sai-gon-1.jpg\",\"size\":63863}]";
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,CategoryId,Detail,Link,ImageUrl,IsActive,IsFeature,Viewed,CreatedDate,CreatedById")] Blog blog,string Files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List< DropZoneItem>  fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        blog.BlogItems.Add(new BlogItem() { Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }

                }
                #region Sys
                
                if (!blog.Viewed.HasValue)
                    blog.Viewed = 1;
                blog.CreatedById = User.Identity.GetUserId();
                blog.CreatedDate = DateTime.Now;
                #endregion
                db.Blogs.Add(blog);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result=true,Id = blog.Id},JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.BlogCategories, "Id", "Title", blog.CategoryId);
            ViewData["Files"] = Files;
            if (Request.IsAjaxRequest())
                return PartialView("_Field", blog);
            return View(blog);
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await db.Blogs.Include("BlogItems").FirstOrDefaultAsync(i=>i.Id== id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.BlogCategories, "Id", "Title", blog.CategoryId);
            return View(blog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,CategoryId,Detail,Link,ImageUrl,IsActive,IsFeature,Viewed,CreatedDate,CreatedById")] Blog blog, string Files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        db.BlogItems.Add(new BlogItem() { BlogId=blog.Id, Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }
                #region Sys

                if (!blog.Viewed.HasValue)
                    blog.Viewed = 1;
                ModelState.Remove("CreatedById");
                ModelState.Remove("CreatedDate");
                blog.ModifiedById = User.Identity.GetUserId();
                blog.ModifiedDate = DateTime.Now;
                #endregion

                db.Entry(blog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = blog.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.BlogCategories, "Id", "Title", blog.CategoryId);
            ViewData["Files"] = Files;
            if (Request.IsAjaxRequest())
                return PartialView("_Field", blog);
            return View(blog);
        }
        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
     //   [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Blog o = await db.Blogs.FindAsync(id);
            FileDelete(o.ImageUrl);
            foreach (var img in o.BlogItems)
            {
                FileDelete(img.ImageUrl);
            }
            db.BlogItems.RemoveRange(o.BlogItems);
            db.Blogs.Remove(o);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = o.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        #region Function 
        void FileDelete(string filename)
        {
            IOHelpers.FileDelete(filename, this.RouteData.GetRequiredString("controller"));//Blogs = controlerName
        }
        #endregion
        #region Ajax
      
        [HttpPost]
        public JsonResult RemoveFile(long Id)
        {
            bool isOk = false;
            string msg = string.Empty;
            BlogItem dt = db.BlogItems.FirstOrDefault(x => x.Id == Id);
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

                    db.BlogItems.Remove(dt);
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
            BlogItem dt = db.BlogItems.Where(x => x.Id == Id).FirstOrDefault();
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
