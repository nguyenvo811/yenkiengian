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
using System.Collections.Generic;
using FSite.Models.Enum;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace FSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class MenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Menus
        //public async Task<ActionResult> Index()
        //{
        //    var menus = db.Menus.Include(m => m.Parent);
        //    return View(await menus.ToListAsync());
        //}

        public ActionResult Index()
        {
            //https://github.com/dbushell/Nestable
            //https://gurayyarar.github.io/AdminBSBMaterialDesign/
            return View();
        }
        public ActionResult Mega()
        {
            //http://meganavbar.com/
           
            return View();
        }
        [HttpPost]
        public ActionResult Mega(string content)
        {
            

            return View();
        }
        public PartialViewResult MegaEx(string name)
        {
            //http://meganavbar.com/
         return   PartialView("Components/ex_"+name);
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from Menus  where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and Title like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI"; }
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
            var dt = db.Menus.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            data.data = from d in dt
                        from s in ids
                        where s.Id == d.Id
                        orderby s.No
                        select new
                        {
                            d.Id,
                            d.Title,
                            d.IsActive,
                            d.Index,
                            Parent = d.Parent != null ? new SubItem { Id = d.Parent.Id, Title = d.Parent.Title } : new SubItem()
                        };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Typemenu(EnumTypeMenu? type)
        {
            string sql = "";
            List<MenuTypeItem> data = new List<MenuTypeItem>();
            //if (type == EnumTypeMenu.Static)
            //{
            //    sql = @"(select 0 'Id','' 'Key',N'Trang chủ' Title) union all
            //            (select 1 'Id','lien-he.html' 'Key',N'Liên hệ' Title) union all
            //            (select 4 'Id','tin-tuc' 'Key',N'Tin tức' Title)";
            //}
            //if (type == EnumTypeMenu.Post)
            //{
            //    sql = "select Id,[Key],Title from Posts";
            //}
          
            //if (type == EnumTypeMenu.CategoryNews)
            //{
            //    sql = "select Id,[Key],Title 'Title' from BlogCategories";
            //}

            switch (type)
            {
                case EnumTypeMenu.Static:
                    sql = @"(select 0 'Id','' 'Key',N'Trang chủ' Title) union all
                        (select 1 'Id','lien-he.html' 'Key',N'Liên hệ' Title) union all
                        
                        (select 4 'Id','tin-tuc' 'Key',N'Tin tức' Title)  union all
 (select 5 'Id','san-pham' 'Key',N'Sản Phẩm' Title) union all
                            (select 6 'Id','dich-vu' 'Key',N'Dịch vụ' Title) union all
(select 7 'Id','hoi-dap' 'Key',N'Hỏi đáp' Title) 
";
                    break;
                case EnumTypeMenu.Post:
                    sql = "select Id,[Key],Title from Posts";
                    break;
                case EnumTypeMenu.CategoryNews:
                    sql = "select Id,[Key],Title 'Title' from BlogCategories";
                    break;
                case EnumTypeMenu.ServiceCate:
                    sql = "select Id,[Key],Title 'Title' from ServiceCategories";
                    break;
                case EnumTypeMenu.Service:
                    sql = "select cast(Id as int) 'Id',[Key],Title 'Title' from [Services]";
                    break;
                case EnumTypeMenu.ProductCate:
                    sql = "select Id,[Key],Title 'Title' from ProductCategories";
                    break;
              
                //case EnumTypeMenu.Agency:
                //    sql = "select cast(Id as int) 'Id',[Key],Title 'Title' from Agencys";
                //    break;
                case EnumTypeMenu.FaqCate:
                    sql = "select Id,[Key],Title 'Title' from FaqCategories";
                    break;
                case EnumTypeMenu.Faq:
                    sql = "select cast(Id as int) 'Id',[Key],Title 'Title' from Faqs";
                    break;
                default:
                   
                    break;
            }


            //if (type == EnumTypeMenu.ProductDistrict)
            //{
            //    sql = "select Id,[Code] 'Key',Title from tblDistrict";
            //}

            if (!string.IsNullOrEmpty(sql))
            {
                data = await db.Database.SqlQuery<MenuTypeItem>(sql).ToListAsync();
            }
            return PartialView("_Typemenu", data);
        }


        // GET: Admin/Menus/Create
        public ActionResult Create()
        {
            //ViewBag.ParentId = new SelectList(db.Menus, "Id", "Title");
            ViewBag.ParentIds = db.Menus.Select(m => new MenuItemModels(){id = m.Id,title = m.Title, _pId = m.ParentId,_index=m.Index}).ToArray();
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Title,Link,Description,IsActive,ParentId,Index,TypeKey,Icon")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                #region Sys
                menu.CreatedById = User.Identity.GetUserId();
                menu.CreatedDate = DateTime.Now;
                #endregion
                db.Menus.Add(menu);
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = menu.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            // ViewBag.ParentId = new SelectList(db.Menus, "Id", "Title", menu.ParentId);
            ViewBag.ParentIds = db.Menus.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            if (Request.IsAjaxRequest())
                return PartialView("_Field", menu);
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            // ViewBag.ParentId = new SelectList(db.Menus, "Id", "Title", menu.ParentId);
            ViewBag.ParentIds = db.Menus.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Title,Link,Description,IsActive,ParentId,Index,TypeKey,Icon")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                #region Sys
                ModelState.Remove("CreatedById");
                ModelState.Remove("CreatedDate");
                menu.ModifiedById = User.Identity.GetUserId();
                menu.ModifiedDate = DateTime.Now;
                #endregion
                db.Entry(menu).State = EntityState.Modified;
                await db.SaveChangesAsync(); 
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = menu.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
           // ViewBag.ParentId = new SelectList(db.Menus, "Id", "Title", menu.ParentId);
            ViewBag.ParentIds = db.Menus.Select(m => new MenuItemModels() { id = m.Id, title = m.Title, _pId = m.ParentId, _index = m.Index }).ToArray();
            if (Request.IsAjaxRequest())
                return PartialView("_Field", menu);
            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Menu menu = await db.Menus.FindAsync(id);
            db.Menus.Remove(menu);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = menu.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }

        #region Ext
      
        public ActionResult Ext()//ajax
        {
            var m = db.Menus.ToArray();
            return View(m);
        }

        public ActionResult ExtIndex()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ExtList(int? ParentId)
        {
            return PartialView("_ExtList", MenuGet(ParentId));
        }
        public IQueryable<Menu> MenuGet(int? ParentId)
        {
            return db.Menus.Where(i => i.ParentId == ParentId).OrderBy(i => i.Index);
        }
        [HttpPost]
        public JsonResult SaveExt(string menu)
        {
            var lst=JsonConvert.DeserializeObject<List<MenuItemModels>>(menu);
           var mns= db.Menus.ToArray();
            foreach (var m in lst)
            {
              var me=  mns.FirstOrDefault(i => i.Id == m.id);
                if (me!=null)
                {
                    me.Index = m._index;
                    me.ParentId = m._pId;
                }
            }
            db.SaveChanges();
          //  menus
            return Json(true,JsonRequestBehavior.AllowGet);
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
