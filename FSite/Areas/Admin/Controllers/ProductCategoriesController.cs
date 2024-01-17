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
    public class ProductCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductCategories
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.ProductCategories.ToListAsync());
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from ProductCategories  where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and Title like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI"; }

            //if (!string.IsNullOrEmpty(model.Category))
            //{
            //    var category = db.ProductCategories.FirstOrDefault(c => c.Key == model.Category);
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
            var dt = db.ProductCategories.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
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
                            d.IsActive,
                                Parent = d.Parent != null ? new SubItem { Id = d.Parent.Id, Title = d.Parent.Title } : new SubItem()
                        };

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            ViewBag.ParentIds = db.ProductCategories.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,Link,ParentId,Index,IsActive,IsFeature")] ProductCategory ProductCategory, string Files)
        {
            
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        ProductCategory.Items.Add(new ProductCategoryItem() { Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }

                db.ProductCategories.Add(ProductCategory);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = ProductCategory.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewData["Files"] = Files;
            ViewBag.ParentIds = db.ProductCategories.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            if (Request.IsAjaxRequest())
                return PartialView("_Field", ProductCategory);

            return View(ProductCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory ProductCategory = await db.ProductCategories.Include("Items").FirstOrDefaultAsync(i => i.Id == id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentIds = db.ProductCategories.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            return View(ProductCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Title2,MetaTitle,MetaKeyword,MetaDescription,Description,Link,ParentId,Index,IsActive,IsFeature")] ProductCategory ProductCategory, string Files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Files))
                {
                    List<DropZoneItem> fs = JsonConvert.DeserializeObject<List<DropZoneItem>>(Files);
                    foreach (var f in fs)
                    {
                        db.ProductCategoryItems.Add(new ProductCategoryItem() { CategoryId = ProductCategory.Id, Title = f.reName, ImageUrl = f.largePicPath, IsActive = true });
                    }
                }
                db.Entry(ProductCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = ProductCategory.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            ViewData["Files"] = Files;
            ViewBag.ParentIds = db.ProductCategories.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            if (Request.IsAjaxRequest())
                return PartialView("_Field", ProductCategory);
            return View(ProductCategory);
        }

        // GET: Admin/ProductCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory ProductCategory = await db.ProductCategories.FindAsync(id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            return View(ProductCategory);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
          ProductCategory ProductCategory = await db.ProductCategories.Include("Items").FirstOrDefaultAsync(i => i.Id == id);
            foreach (var img in ProductCategory.Items)
            {
                FileDelete(img.ImageUrl);
            }
            db.ProductCategoryItems.RemoveRange(ProductCategory.Items);
            db.ProductCategories.Remove(ProductCategory);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = ProductCategory.Id }, JsonRequestBehavior.AllowGet);
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
            ProductCategoryItem dt = db.ProductCategoryItems.FirstOrDefault(x => x.Id == Id);
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

                    db.ProductCategoryItems.Remove(dt);
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
            ProductCategoryItem dt = db.ProductCategoryItems.Where(x => x.Id == Id).FirstOrDefault();
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

        #region Ext

        public ActionResult Ext()//ajax
        {
            var m = db.ProductCategories.ToArray();
            return View(m);
        }

        public ActionResult ExtIndex()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ExtList(int? ParentId)
        {
            return PartialView("_ExtList", ItemGet(ParentId));
        }
        public IQueryable<ProductCategory> ItemGet(int? ParentId)
        {
            return db.ProductCategories.Where(i => i.ParentId == ParentId).OrderBy(i => i.Index);
        }
        [HttpPost]
        public JsonResult SaveExt(string menu)
        {
            var lst = JsonConvert.DeserializeObject<List<MenuItemModels>>(menu);
            var mns = db.ProductCategories.ToArray();
            foreach (var m in lst)
            {
                var me = mns.FirstOrDefault(i => i.Id == m.id);
                if (me != null)
                {
                    me.Index = m._index;
                    me.ParentId = m._pId;
                }
            }
            db.SaveChanges();
            //  menus
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
