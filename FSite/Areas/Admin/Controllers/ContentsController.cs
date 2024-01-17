using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FSite.Models;
using FSite.Models.Data;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using FSite.Models.Enum;
using FSite.Helpers;
using FSite.Areas.Admin.Models;

namespace FSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class ContentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Contents
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Contents.ToListAsync());
        //}
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Get(string Status)
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

            string query = $"select @columns from(select ROW_NUMBER() OVER (ORDER BY  [{sortColumn}] {sortColumnDir}) AS No,Id from Contents  where 1=1 ";
            if (!string.IsNullOrEmpty(searchValue))
            { query += $" and (Code like N'%{searchValue}%' or  Title like N'%{searchValue}%' or Meta like N'%{searchValue}%' collate SQL_Latin1_General_CP1_CI_AI)"; }

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "0")
                    query += $" and isnull(Status,0)=0";
                else
                    query += $" and Status={Status}";
            }
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
            var ids = db.Database.SqlQuery<DataResultDecimalModels>(query.Replace("@columns", "cast(No as decimal) No,cast(Id as decimal) Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Contents.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            var eStatus=   (from EnumContentStatus o in Enum.GetValues(typeof(EnumContentStatus))
                                            select new SelectListItem
                                            {
                                                Value = ((int)o).ToString(),
                                                Text = EnumHelper<EnumContentStatus>.GetDisplayValue(o)
                                            }).ToList();

            data.data = from d in dt
                        from s in ids
                       
                        where s.Id == d.Id
                        orderby s.No
                        select new
                        {
                            d.Id,
                            d.Title,
                            d.Code,
                            d.Status,
                            d.CreatedDate,
                            d.ModifiedDate,
                            //_Status = d.Status.HasValue? eStatus.Where(st=>st.Value==d.Status.Value.ToString()).Select(st=>new SubItem() {Id=st.Value,Title=st.Text }).FirstOrDefault() : new SubItem()
                             _Status = eStatus.Where(st => st.Value ==(d.Status.HasValue?d.Status.Value:0).ToString()).Select(st => new SubItem() { Id = st.Value, Title = st.Text }).FirstOrDefault() 
                        };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        // GET: Admin/Contents/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.Contents.FindAsync(id);

            if (content == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView("_Detail", content);
            return View(content);
        }

        // GET: Admin/Contents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Title,Note,MetaType,Meta,Status,CreatedDate,CreatedById,ModifiedDate,ModifiedById")] Content content)
        {
            if (ModelState.IsValid)
            {
                #region Sys
                content.ModifiedById = User.Identity.GetUserId();
                content.ModifiedDate = DateTime.Now;
                #endregion
                db.Contents.Add(content);
                   await db.SaveChangesAsync(); 
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = content.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView("_Field", content);
            return View(content);
        }

        // GET: Admin/Contents/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.Contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Admin/Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Title,Note,MetaType,Meta,Status,CreatedDate,CreatedById,ModifiedDate,ModifiedById")] Content content)
        {
            if (ModelState.IsValid)
            {
                #region Sys
                ModelState.Remove("CreatedById");
                ModelState.Remove("CreatedDate");
                content.ModifiedById = User.Identity.GetUserId();
                content.ModifiedDate = DateTime.Now;
                #endregion
                db.Entry(content).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, Id = content.Id }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView("_Field", content);
            return View(content);
        }

        // POST: Admin/Contents/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            Content content = await db.Contents.FindAsync(id);
            db.Contents.Remove(content);
            await db.SaveChangesAsync();
            if (Request.IsAjaxRequest())
                return Json(new { result = true, Id = content.Id }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> AjaxDetail(int id)
        {
            Content content = await db.Contents.FindAsync(id);
            ViewBag.Status= (from EnumContentStatus o in Enum.GetValues(typeof(EnumContentStatus))
                           select new SelectListItem
                           {
                               Value = ((int)o).ToString(),
                               Text = EnumHelper<EnumContentStatus>.GetDisplayValue(o),
                               Selected= content.Status.HasValue? (content.Status== (int)o?true:false):false
                           }).ToList();
            return PartialView("Components/_DefaultContent", content);
        }
        [HttpPost]
        public  JsonResult AjaxUpdateStatus(int id,int Status)
        {
            Content content =  db.Contents.Find(id);
            content.Status = Status;
            content.ModifiedById = User.Identity.GetUserId();
            content.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return Json(new {result=true },JsonRequestBehavior.AllowGet);
        }
        #region Dynamic object http://blogs.quovantis.com/dynamic-models-for-mvc-view/

        public ActionResult Marks()
        {
            dynamic expando = new System.Dynamic.ExpandoObject();
            var marksModel = expando as IDictionary<string, object>;
            string studentName = "Alice";
            marksModel.Add("Name", studentName);
            marksModel.Add("Physics", "24");
            marksModel.Add("Chemistry", "45");
            marksModel.Add("Biology", "31");
            return View(marksModel);
        }
        [HttpPost]
        public ActionResult Marks(FormCollection model)
        {
            // Using the ValueProvider
            dynamic expando = new System.Dynamic.ExpandoObject();
            var marksModel = expando as IDictionary<string, object>;

            foreach (var key in model.Keys)
            {
                var value = model[key.ToString()];
                marksModel.Add(key.ToString(), value);
            }

            string Values = Newtonsoft.Json.JsonConvert.SerializeObject(marksModel);
            //DeSerializeObject
            dynamic deValues = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(Values);
            //   dynamic deValues = Newtonsoft.Json.JsonConvert.DeserializeObject(Values);

            return View(deValues);

        }

        
        #endregion
#region Demo Content
        public ActionResult Template_Create()
        {
            dynamic expando = new System.Dynamic.ExpandoObject();
            var objModel = expando as IDictionary<string, object>;
            objModel.Add("_Json", @"[
    {'name':'Bob','age':40},
    {'name':'Frank','age':15},
    {'name':'Bill','age':65},
    {'name':'Robert','age':24}
]");
            objModel.Add("_Script", @"{'tag':'table', 'children':[
    {'tag':'tbody','children':[
        {'tag':'tr','children':[
            {'tag':'td','html':'${name}'},
            {'tag':'td','html':'${age}'}
        ]}
    ]}
]}");
            return View(objModel);
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
