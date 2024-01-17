using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FSite.Models;
using FSite.Models.Data;
using FSite.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using FSite.Helpers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Posts
        //public async Task<ActionResult> Index()
        //{
        //    var posts = db.Posts.Include(p => p.Category);
        //    return View(await posts.ToListAsync());
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from Posts where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and  Title like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI or Location like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI)"; }
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
            var dt = db.Posts.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            data.data = from d in dt
                        from s in ids
                        where s.Id == d.Id
                        orderby s.No
                        select new
                        {
                            d.Id,
                            d.ImageUrl,
                            d.Title,
                            d.IsFeature,
                            d.IsActive,
                            Category = d.Category != null ? new SubItem { Id = d.Category.Id, Title = d.Category.Title } : new SubItem()
                        };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.PostCategories, "Id", "Title");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,CategoryId,Detail,Link,ImageUrl,IsActive,IsFeature,Viewed,CreatedDate,CreatedById,Template")] Post post,string Files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        post.Items.Add(new PostItem() { Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }
                #region Sys
                post.Viewed = 1;
                post.CreatedById = User.Identity.GetUserId();
                post.CreatedDate = DateTime.Now;
                #endregion
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = post.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewData["Files"] = Files;
            ViewBag.CategoryId = new SelectList(db.PostCategories, "Id", "Title", post.CategoryId);
            if (Request.IsAjaxRequest())
                return PartialView("_Field", post);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.Include("Items").FirstOrDefaultAsync(i => i.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.PostCategories, "Id", "Title", post.CategoryId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost ,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,CategoryId,Detail,Link,ImageUrl,IsActive,IsFeature,Viewed,CreatedDate,CreatedById,Template")] Post post, string Files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        db.PostItems.Add(new PostItem() { PostId = post.Id, Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }
                #region Sys
                ModelState.Remove("CreatedById");
                ModelState.Remove("CreatedDate");
                post.ModifiedById = User.Identity.GetUserId();
                post.ModifiedDate = DateTime.Now;
                #endregion
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync(); 
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = post.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewData["Files"] = Files;
            ViewBag.CategoryId = new SelectList(db.PostCategories, "Id", "Title", post.CategoryId);
            if (Request.IsAjaxRequest())
                return PartialView("_Field", post);
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            FileDelete(post.ImageUrl);
            foreach (var img in post.Items)
            {
                FileDelete(img.ImageUrl);
            }
            db.PostItems.RemoveRange(post.Items);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = post.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        //--------------
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
            PostItem dt = db.PostItems.FirstOrDefault(x => x.Id == Id);
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

                    db.PostItems.Remove(dt);
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
            PostItem dt = db.PostItems.Where(x => x.Id == Id).FirstOrDefault();
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
