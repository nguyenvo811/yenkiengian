using FSite.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FSite.Models.Enum;
using System;
using FSite.Models.Data;
using System.Data.Entity;
using FSite.Helpers;
using System.Collections.Generic;

namespace FSite.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        #region menu

        [ChildActionOnly]
        public PartialViewResult MenuTop(int? ParentId,bool? isfirst=false)
        {
            ViewBag.isfirst = isfirst;
            return PartialView("Components/_MenuTop", MenuGet(ParentId));
        }
        public IQueryable<Menu> MenuGet(int? ParentId)
        {
            return db.Menus.Where(i => i.ParentId == ParentId & i.IsActive == true).OrderBy(i => i.Index);
        }
        #endregion
       // [Route("trang-chu.html")]
        public ActionResult Index()
        {
           
            //        //var d=    db.Blogs.Include("BlogCategories");
            //            var ids = db.Database.SqlQuery<long>(@"SELECT 
            //    b.Id  
            //FROM Blogs b
            //").ToArray();
            //            var custs = db.Blogs.Where(o=>ids.Any(i=>i.Equals(o.Id))).ToList();
            //  db.Database.Attach(entity);
            HomeViewModels m = new HomeViewModels();
            m.Featureblogs = db.Blogs.Where(i => i.IsFeature == true && i.IsActive == true).OrderByDescending(i=>i.CreatedDate).Take(5).ToArray();
            //m.Featureservices = db.Services.Where(i => i.IsFeature == true && i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(10).ToArray();

            //  m.Featurefaqs = db.Faqs.Where(i => i.IsFeature == true && i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(4).ToArray();

            var cates = db.ProductCategories.Where(i=>i.IsFeature==true& i.IsActive==true& !i.ParentId.HasValue).OrderBy(i=>i.Index);

           // ViewData["Categories"] = cates;
         List<HomeProductGroupModel> groups = new List<HomeProductGroupModel>();
            groups = cates.Select(i => new HomeProductGroupModel()
            {
                Id = i.Id,
                Image = i.Items.FirstOrDefault().ImageUrl,
                Key = i.Key,
                Title = i.Title,
                Subs =i.Subs.Where(s => s.IsFeature == true & s.IsActive == true).OrderBy(s => s.Index)
                .Select(s=>new HomeProductGroupChildModel()
                {
                    Id=s.Id,Title=s.Title,Key=s.Key
                }),
                Features=db.Products
                        .Where(f=>f.IsActive==true&f.IsFeature==true& 
                        (f.CategoryId==i.Id||
                        i.Subs.Any(c=>c.Id==f.CategoryId)))
                        .OrderByDescending(f=>f.CreatedDate).Take(5),
                Lastest  = db.Products
                        .Where(f => f.IsActive == true &
                        (f.CategoryId == i.Id ||
                        i.Subs.Any(c => c.Id == f.CategoryId)))
                        .OrderByDescending(f => f.CreatedDate).Take(5)

            }).ToList();

            ViewData["Categories"] = groups;
            return View(m);
        }
       

        [Route("lien-he.html")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ContactViewModels m = new ContactViewModels();
            //m.Offices = db.Agencies.Where(i => i.IsActive == true&&i.IsCompany==true).Select(i=>new OfficeModels() {
            //    Id=i.Id,
            //    Phone=i.Phone,
            //    Title=i.Title,
            //    Email=i.Email,
            //    FullName=i.FullName,
            //    Location=i.Address,
            //    Desc=i.Description
            //}).ToList();
            #region Temp
            // m.Offices.Add(new OfficeModels()
            // {
            //     Id = 1,
            //     Title = "Phòng vé [Sài Gòn]",
            //     Location = "355 Phạm Ngũ Lão, Quận 1 HCM",
            //     Phone= "028 38 37 37 67",
            //     Email= "hieutran1409@gmail.com",
            //     IsHead = false,
            //     Lat = 10.7671498,
            //     Lng = 106.6875819
            //     ,
            //     Category = new OfficeCategoryModels() { Id = 1, Title = "Hồ chí minh" }
            // });
            // m.Offices.Add(new OfficeModels()
            // {
            //     Id = 2,
            //     Title = "Phòng hàng [Sài Gòn]",
            //     Location = "295 Trần Phú, Phường 8, Quận 5, Hồ Chí Minh",
            //     Phone = "0914 143 148",
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = false,
            //     Lat = 10.756611,
            //     Lng = 106.6714629
            //  ,
            //     Category = new OfficeCategoryModels() { Id = 1, Title = "Hồ chí minh" }
            // });
            // m.Offices.Add(new OfficeModels()
            // {
            //     Id = 3,
            //     Title = "Phòng vé [Cam Ranh]",
            //     Location = "8 Tố Hữu, Cam Thuận, Cam Ranh, Khánh Hòa",
            //     Phone = "1900 63 39 63",//0258 247 2522
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = false,
            //     Lat = 11.9188203,
            //     Lng = 109.1485988
            //     Category = new OfficeCategoryModels() { Id = 2, Title = "Khánh Hòa" }
            // });


            // m.Offices.Add(new OfficeModels()
            // {
            //     Id = 4,
            //     Title = "Phòng vé [Nha Trang]",
            //     Location = "12D Đường Hoàng Hoa Thám, Lộc Thọ, Nha Trang, Khánh Hòa",
            //     Phone = "1900 63 39 63",//0258 38 211 79 - 0258 38 222 68
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = true,
            //     Lat = 12.2486171,
            //     Lng = 109.191694
            //  ,
            //     Category = new OfficeCategoryModels() { Id = 2, Title = "Khánh Hòa" }
            // });
            // m.Offices.Add(new OfficeModels()
            // {
            //     Id = 4,
            //     Title = "Phòng vé 2 [Nha Trang]",
            //     Location = "297 Lê Hồng Phong, P. Phước Hòa.",
            //     Phone = "025 8387 2991",//0258 38 211 79 - 0258 38 222 68
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = true,
            //     Lat = 12.2486171,
            //     Lng = 109.191694
            //,
            //     Category = new OfficeCategoryModels() { Id = 2, Title = "Khánh Hòa" }
            // });

            // m.Offices.Add(new OfficeModels() { Id = 5, Title = "Phòng vé [Ninh Hòa]",
            //     Location = "55 Nguyễn Thị Ngọc Oanh, TT. Ninh Hòa, Ninh Hòa, Khánh Hòa",
            //     Phone = "025 86 289 289",
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = false, Lat = 12.4895833, Lng = 109.1263369, Category = new OfficeCategoryModels() { Id = 2, Title = "Khánh Hòa" } });
            // m.Offices.Add(new OfficeModels() { Id = 6, Title = "Phòng vé [Vạn Giã]",
            //     Location = "Bến xe Quyết Thắng, Lê Hồng Phong, Vạn Thắng, Vạn Ninh, Khánh Hòa",
            //     Phone = "025 86 538 538",
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = false, Lat = 12.7031477, Lng = 109.1929071, Category = new OfficeCategoryModels() { Id = 2, Title = "Khánh Hòa" } });
            // m.Offices.Add(new OfficeModels() { Id = 7, Title = "Phòng vé [Đà Lạt]", Location = "37 Đường Nguyễn Văn Cừ, Phường 1, Thành phố Đà Lạt, Lâm Đồng",
            //     Phone = "0263 355 6767",
            //     Email = "hieutran1409@gmail.com",
            //     IsHead = false, Lat = 11.9394628, Lng = 108.4310229, Category = new OfficeCategoryModels() { Id = 3, Title = "Lâm đồng" } });

            #endregion

            return View(m);
        }
        #region Widget / Module
    
        public PartialViewResult Widget_Slider(SliderSearchViewModel model)
        {
            // Get the enumeration's value.
         
                var _typerequest = FSite.Helpers.EnumHelper<FSite.Models.Enum.EnumSliderModules>.GetDescriptionValue(model.type);
            // Display the values.
            //    foreach (string value in Enum.GetNames(typeof(FSite.Models.Enum.EnumTypeMenu)))
            //    {
            //        // Get the enumeration's value.
            //        FSite.Models.Enum.EnumTypeMenu t =
            //            (FSite.Models.Enum.EnumTypeMenu)Enum.Parse(typeof(FSite.Models.Enum.EnumTypeMenu), value);
            //        var des = FSite.Helpers.EnumHelper<FSite.Models.Enum.EnumTypeMenu>.GetDescriptionValue(t);
            //// Display the values.
            //    }
          
            string query = string.Empty;
            switch (model.type)
            {
                case EnumSliderModules.Home:
                case EnumSliderModules.Conact:
                    query = string.Format(@"select i.* from  Sliders s
inner join SliderItems i on s.Id = i.SliderId
 where s.IsActive = 1 and i.IsActive = 1 and s.[Type]={0}
order by i.[Index]", (int)EnumSliderModules.Home);
                // m=  db.Sliders.Where(i => i.Type == model.type).SelectMany(i=>i.SliderItems.Where(j=>j.IsActive==true)).ToList();
                    break;
                case EnumSliderModules.Blog:
                    query =string.Format(@"
declare @Count int
declare  @Id int
set  @Id={0}


if @Id=0
begin
--goto _else
--print '_default:'
_default:

-- random when dont has cateid

set @Id=(select TOP 1 CategoryId FROM BlogCategoryItems ORDER BY NEWID() )

select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  BlogCategories p
inner join BlogCategoryItems i on p.Id=i.CategoryId
 where p.IsActive=1 and i.IsActive=1 and p.Id=@Id 

 end
 else
 begin 
 _else:
 --print '_else::'

 if (select count(i.id) from BlogCategoryItems i where i.CategoryId=@Id) <> 0
 begin 
 select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  BlogCategories p
inner join BlogCategoryItems i on p.Id=i.CategoryId
where i.CategoryId=@Id
 end
 else 
 begin
  --print '_else _else::'
 set @Id=0
 goto _default
  end
 end", model.Id);

                    break;
                case EnumSliderModules.Service:
                    query = string.Format(@"
declare @Count int
declare  @Id int
set  @Id={0}

if @Id=0
begin
--goto _else
--print '_default:'
_default:
-- random when dont has cateid
set @Id=(select TOP 1 CategoryId FROM ServiceCategoryItems ORDER BY NEWID() )
select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  ServiceCategories p
inner join ServiceCategoryItems i on p.Id=i.CategoryId
 where p.IsActive=1 and i.IsActive=1 and p.Id=@Id 
 end
 else
 begin 
 _else:
 --print '_else::'
 if (select count(i.id) from ServiceCategoryItems i where i.CategoryId=@Id) <> 0
 begin 
 select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  ServiceCategories p
inner join ServiceCategoryItems i on p.Id=i.CategoryId
where i.CategoryId=@Id
 end
 else 
 begin
  --print '_else _else::'
 set @Id=0
 goto _default
  end
 end

", model.Id);
                    break;
             
              
                case EnumSliderModules.Product:
                    query = string.Format(@"
declare @Count int
declare  @Id int
set  @Id={0}

if @Id=0
begin
--goto _else
--print '_default:'
_default:
-- random when dont has cateid
set @Id=(select TOP 1 CategoryId FROM ProductCategoryItems ORDER BY NEWID() )
select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  ProductCategories p
inner join ProductCategoryItems i on p.Id=i.CategoryId
 where p.IsActive=1 and i.IsActive=1 and p.Id=@Id 
 end
 else
 begin 
 _else:
 --print '_else::'
 if (select count(i.id) from ProductCategoryItems i where i.CategoryId=@Id) <> 0
 begin 
 select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  ProductCategories p
inner join ProductCategoryItems i on p.Id=i.CategoryId
where i.CategoryId=@Id
 end
 else 
 begin
  --print '_else _else::'
 set @Id=0
 goto _default
  end
 end

", model.Id);
                    break;
                case EnumSliderModules.Faq:
                    query = string.Format(@"
declare @Count int
declare  @Id int
set  @Id={0}

if @Id=0
begin
--goto _else
--print '_default:'
_default:
-- random when dont has cateid
set @Id=(select TOP 1 CategoryId FROM FaqCategoryItems ORDER BY NEWID() )
select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  FaqCategories p
inner join FaqCategoryItems i on p.Id=i.CategoryId
 where p.IsActive=1 and i.IsActive=1 and p.Id=@Id 
 end
 else
 begin 
 _else:
 --print '_else::'
 if (select count(i.id) from FaqCategoryItems i where i.CategoryId=@Id) <> 0
 begin 
 select i.Id, cast(i.CategoryId as bigint) 'SliderId',i.ImageUrl,i.[Index],i.IsActive,i.Title from  FaqCategories p
inner join FaqCategoryItems i on p.Id=i.CategoryId
where i.CategoryId=@Id
 end
 else 
 begin
  --print '_else _else::'
 set @Id=0
 goto _default
  end
 end

", model.Id);
                    break;
              
                 
                default:
                    break;
            }
            //  List<SliderItem> m = new List<SliderItem>();
            var m = db.Database.SqlQuery<SliderItem>(query);
               // ViewBag.model = model;
                return PartialView("Components/_Slider" + model.type.ToString(), m);
            

           
        }
    
        #endregion

        [HttpGet]
        [Route("dat-lich-hen")]
        public PartialViewResult CustomerRequest(string Content, EnumTypeRequest TypeRequest= EnumTypeRequest.General_Contact)
       {
                // Get the enumeration's value.
                var _typerequest = FSite.Helpers.EnumHelper<FSite.Models.Enum.EnumTypeRequest>.GetDescriptionValue(TypeRequest);
            // Display the values.
            //    foreach (string value in Enum.GetNames(typeof(FSite.Models.Enum.EnumTypeMenu)))
            //    {
            //        // Get the enumeration's value.
            //        FSite.Models.Enum.EnumTypeMenu t =
            //            (FSite.Models.Enum.EnumTypeMenu)Enum.Parse(typeof(FSite.Models.Enum.EnumTypeMenu), value);
            //        var des = FSite.Helpers.EnumHelper<FSite.Models.Enum.EnumTypeMenu>.GetDescriptionValue(t);
            //// Display the values.
            //    }
            RequestViewModel model = new RequestViewModel() {Content=Content };
            ViewBag.TypeRequest = TypeRequest;
            return PartialView("Components/_Form"+_typerequest,model);
        }
        [HttpPost]
        [Route("dat-lich-hen")]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerRequest(RequestViewModel model, EnumTypeRequest TypeRequest,string DefaultContent)
        {
            var _typerequest = FSite.Helpers.EnumHelper<FSite.Models.Enum.EnumTypeRequest>.GetDescriptionValue(TypeRequest);
            ViewBag.TypeRequest = TypeRequest;
            if (ModelState.IsValid)
            {
                string msg = "";
                bool isok = true;
                Content c = new Content();
                var maxId = 0;
                if (db.Contents.Count() != 0)
                    c.Id = db.Contents.Max(i => i.Id) + 1;
                else maxId = 1;
                c.Id = maxId;
                c.Code = $"Request-{Guid.NewGuid().ToString()}";
                c.Note = Helpers.IOHelpers.RazorRunCompile(Server.MapPath("~/Views/Shared/EmailsTemplates"), $"_Email{_typerequest}.cshtml", "_Email" + _typerequest, model);
                c.MetaType = "RequestViewModel";
                c.Meta = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                c.Title = $"Yêu cầu [ {model.Phone} - {model.FullName} ]";
                c.CreatedDate = DateTime.Now;
                c.ModifiedDate = DateTime.Now;
                db.Contents.Add(c);
                db.SaveChanges();
                //  Hangfire.BackgroundJob.Enqueue(() => RequestMailSend(c.Id));

                //string emailto = ConfigurationManager.AppSettings.Get("ContactEmail");
                //Services.EmailService emailService = new Services.EmailService();
                //await emailService.SendEmailToSystemAsync(emailto, $"Email : {model.Email} - Điện thoại:{model.Phone} - Tên : {model.FullName} - {model.Content}", $"Yêu cầu sản phẩm :[{itemid}] khách hàng :[ {model.FullName} ]");
                msg = "gửi liên hệ thành công";
                isok = true;
                //  return Json(isok, msg, JsonRequestBehavior.AllowGet);
                RequestViewModel _model = new RequestViewModel() { Content = DefaultContent };
                return PartialView("Components/_Form" + _typerequest, _model);
            }
          
            return PartialView("Components/_Form" + _typerequest, model);
        }
        //[HttpPost]
        //[Route("dat-lich-hen")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CustomerRequest(RequestViewModel model, EnumTypeRequest TypeRequest)
        //{
        //    var _typerequest = FSite.Helpers.EnumHelper<FSite.Models.Enum.EnumTypeRequest>.GetDescriptionValue(TypeRequest);
        //    if (ModelState.IsValid)
        //    {
        //        string msg = "";
        //        bool isok = true;
        //        Content c = new Content();

        //        c.Code = $"Request-{Guid.NewGuid().ToString()}";
        //        c.Note = Helpers.IOHelpers.RazorRunCompile(Server.MapPath("~/Views/Emails"), $"_Email{_typerequest}.cshtml", "_Email"+ _typerequest, model);
        //        c.MetaType = "RequestViewModel";
        //        c.Meta = Newtonsoft.Json.JsonConvert.SerializeObject(model);
        //        c.Title = $"Yêu cầu [ {model.Phone} - {model.FullName} ]";
        //        c.CreatedDate = DateTime.Now;
        //        c.ModifiedDate = DateTime.Now;
        //        db.Contents.Add(c);
        //        await db.SaveChangesAsync();
        //      //  Hangfire.BackgroundJob.Enqueue(() => RequestMailSend(c.Id));

        //        //string emailto = ConfigurationManager.AppSettings.Get("ContactEmail");
        //        //Services.EmailService emailService = new Services.EmailService();
        //        //await emailService.SendEmailToSystemAsync(emailto, $"Email : {model.Email} - Điện thoại:{model.Phone} - Tên : {model.FullName} - {model.Content}", $"Yêu cầu sản phẩm :[{itemid}] khách hàng :[ {model.FullName} ]");
        //        msg = "gửi liên hệ thành công";
        //        isok = true;
        //        return Json(isok, msg, JsonRequestBehavior.AllowGet);
        //    }
        //    ViewBag.TypeRequest = TypeRequest;
        //    return PartialView("Components/_Form"+ _typerequest, model);
        //}

        [HttpPost]
        [Route("lien-he.html")]
        public async Task<ActionResult> Contact(ContactModels model)
        {
            if (ModelState.IsValid)
            {
                //string emailto = ConfigurationManager.AppSettings.Get("ContactEmail");
                //Services.EmailService emailService = new Services.EmailService();
                //await emailService.SendEmailToSystemAsync(emailto, model.Email + " - " + model.Content, "Gửi mail liên hệ");
                //tblContent c = new tblContent();
                //c.Code = $"Contact-{Guid.NewGuid().ToString()}";
                //c.Note = Helpers.ProcessFile.RazorRunCompile(Server.MapPath("~/Views/Emails"), "_EmailContact.cshtml", "_EmailContact", model);
                //c.MetaType = "ContactViewModels";
                //c.Meta = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                //c.Title = $"Liên hệ [{model.Phone}] - [{model.FullName}]";
                //c.CreatedDate = DateTime.Now;
                //c.ModifiedDate = DateTime.Now;
                //dto.tblContents.Add(c);
                //await dto.SaveChangesAsync();
                //Hangfire.BackgroundJob.Enqueue(() => ContactMailSend(c.Id));
                if (Request.IsAjaxRequest())
                    return Json(new { result = true, FullName = model.FullName }, JsonRequestBehavior.AllowGet);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (Request.IsAjaxRequest())
                return PartialView("Components/_Contact_FrmContact", model);
            return View(model);
        }

        //[OutputCache(CacheProfile = "Cache1DayParams_key")]
        [Route("{key}.html")]
        public ActionResult Page(string key)
        {
            //if (string.IsNullOrEmpty(key))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Post m = db.Posts.FirstOrDefault(i => i.Key.Equals(key));
            if (m == null)
            {
                m = new Post();
                m.Template = (int)EnumPostTemplate.None;
            }
            if (m == null)
            {
                return HttpNotFound();
            }
            if (m.Template==EnumPostTemplate.Scroll_ContentLeft)
            {
                List<Post> groups = new List<Post>();
                if (m.CategoryId.HasValue)
                    groups = db.Posts.Where(x => x.CategoryId == m.CategoryId).ToList();

                ViewBag.Groups = groups;
            }
            return View(m);
        }
        [ChildActionOnly]
        public PartialViewResult SPostCategory(int? CategoryId)
        {
            //ViewBag.Category = db.SpecialistCategories.Where(i => i.IsActive == true).OrderBy(i => i.Index);
            var ms = db.Posts.Where(i => i.IsActive == true).AsQueryable();
            if (CategoryId.HasValue)
            {
                ms = ms.Where(i => i.CategoryId ==CategoryId);
            }
            ms = ms.OrderBy(i => i.Viewed);
            ViewBag.Category = ms;
            return PartialView("Components/_PostCategory");
        }

        //[Route("{key}.html")]
        //public ActionResult PageTab(string key)
        //{
        //    //if (string.IsNullOrEmpty(key))
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    Post m = db.Posts.FirstOrDefault(i => i.Key.Equals(key));

        //    if (m == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (!m.Template.HasValue)
        //        m.Template = (int)EnumPostTemplate.None;
        //    List<Post> groups = new List<Post>();
        //    if (m.CategoryId.HasValue)
        //        groups = db.Posts.Where(x => x.CategoryId == m.CategoryId).ToList();

        //    ViewBag.Groups = groups;
        //    return View(m);
        //}

        #region Products
        private ProductSearchViewModel ProductsView(ProductSearchViewModel m)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["cs"]))
            //    model.Category = Request.QueryString["cs"].Split(',').ToList();
            if (!string.IsNullOrEmpty(m.Category))
            {
                ViewData["category"] = db.ProductCategories.FirstOrDefault(c => c.Key == m.Category);
            }
            ViewBag.Sorts = from EnumProductSort o in Enum.GetValues(typeof(EnumProductSort))
                            select new SelectListItem
                            {
                                Value = o.ToString(),
                                Text = EnumHelper<EnumProductSort>.GetDisplayValue(o)
                            };
            ViewBag.Categories = db.ProductCategories.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Key
            });
            ViewBag.Brands = db.ProductBrands.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Key
            });
            return m;
        }

        //san-pham
        //san-pham/1
        [HttpGet]
        [Route("san-pham/{p:int?}", Name = "sanpham")]
        public ActionResult Product(int? p, ProductSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/san-pham/{0}" + Request.Url.Query;
            return View(ProductsView(model));
        }
        //san-pham/khuyen-mai
        //san-pham/khuyen-mai/1
        [HttpGet]
        [Route("san-pham/{category:minlength(2)}/{p:int?}", Name = "sanpham-loai")]
        public ActionResult Product(int? p, string category, ProductSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/san-pham/" + category + "/{0}" + Request.Url.Query;
            return View(ProductsView(model));
        }
        //2005
        [Route("ajax-san-pham")]
        public PartialViewResult AjaxProducts(int? page, int? pageSize, string sort, ProductSearchViewModel model)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["cs"]))
            //{
            //    model.Category = Request.QueryString["cs"].Split(',').ToList();

            //}

            int TotalItemCount = 0;
            string sortColumn = string.Empty;

            if (string.IsNullOrEmpty(sort))
            { sortColumn = "Id desc"; }//default column sort / must be have/ not empty
            else
            {
                sortColumn = EnumHelper<EnumProductSort>.GetDescriptionValue((EnumProductSort)Enum.Parse(typeof(EnumProductSort), sort));
            }
            ViewBag.sortColumn = sort;
            string query = @"select @columns from(
               select ROW_NUMBER() OVER (ORDER BY b.@sortColumn) AS No,b.Id from Products b  where b.isActive=1 ".Replace("@sortColumn", sortColumn);

            if (!string.IsNullOrEmpty(model.Key))
            { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

            if (!string.IsNullOrEmpty(model.Category))
            {
                var category = db.ProductCategories.FirstOrDefault(c => c.Key == model.Category);

                if (category != null)
                {
                    ViewData["category"] = category;
                    query += $" and b.[CategoryId] in (select id from productcategories where id={category.Id} or ParentId={category.Id})" ;
                }
            }
            //model.Category = model.Category.Where(c => !string.IsNullOrEmpty(c)).ToList();
            //if (model.Category.Count != 0)
            //{
            //    var j = $"'{string.Join("','", model.Category)}'";
            //    query += " and p.Category in (" + j + ")";
            //}
            //if (!string.IsNullOrEmpty(model.Project))
            //{
            //    // query += " and p.Project='" + model.Project + "'";  
            //    query += $" and p.project='{model.Project}' or p.project in (select convert(varchar,Id) from tblProject where code='{model.Project}')";
            //}
            if (!string.IsNullOrEmpty(model.PriceFrom))
                query += " and b.Price*1000000>=" + model.PriceFrom;
            if (!string.IsNullOrEmpty(model.PriceTo))
                query += " and b.Price*1000000<=" + model.PriceTo;
            query += ") AS b";
            //----------for paging
            TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();

            int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 8;
            int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
            //take is pagesize , skip is from
            int pfrom = skip + 1;
            int pto = pfrom + take - 1;

            query += $" where No >= {pfrom} AND No <= {pto} order by No";

            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Products.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();

            ViewBag.PageSize = take;
            ViewBag.TotalItemCount = TotalItemCount;
            ViewBag.Page = skip == 0 ? 1 : page;
            ViewBag.Data = from d in dt
                           from s in ids
                           where s.Id == d.Id
                           orderby s.No
                           select d;

            return PartialView("Components/AjaxProducts", model);

        }
       
        //[OutputCache(CacheProfile = "Cache1DayParams_key")]
        [Route("{key}/san-pham")]
        public ActionResult ProductDetail(string key)
        {
            //if (string.IsNullOrEmpty(key))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var m = db.Products.FirstOrDefault(i => i.Key == key);
            if (m == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("Components/AjaxProducts_QuickView",m);
            }
            ViewBag.Similars = db.Products.Where(i => i.IsActive == true && m.Id != i.Id && i.CategoryId == m.CategoryId).OrderByDescending(i => i.CreatedDate).Take(10);

            return View(m);
        }

        [Route("ajax-san-pham-quick-search")]
        public PartialViewResult AjaxProducts(string q)
        {

            int TotalItemCount = 0;
            string sortColumn =  "Id desc"; 
           
            string query = @"select @columns from(
               select ROW_NUMBER() OVER (ORDER BY b.@sortColumn) AS No,b.Id from Products b  where b.isActive=1 ".Replace("@sortColumn", sortColumn);

            if (!string.IsNullOrEmpty(q))
            { query += " and  b.Title like N'%" + q + "%' collate SQL_Latin1_General_CP1_CI_AI"; }
            query += ") AS b";
            //----------for paging
            TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();

            query += $" where No >= 0 AND No <= 5 order by No";
            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Products.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
            ViewBag.TotalItemCount = TotalItemCount;
        
           var m = from d in dt
                           from s in ids
                           where s.Id == d.Id
                           orderby s.No
                           select d;

            return PartialView("Components/AjaxProducts_QuickSearch", m);

        }
        [ChildActionOnly]
        public PartialViewResult SProductCategory()
        {
            ViewBag.Category = db.ProductCategories.Where(i => i.IsActive == true).OrderBy(i => i.Index);
            return PartialView("Components/_ProductCategory");
        }
        [ChildActionOnly]
        public PartialViewResult SProductLastest()
        {
            return PartialView("Components/_ProductLastest", db.Products.Include("Category").Where(i => i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }
        [ChildActionOnly]
        public PartialViewResult SProductFeature()
        {
            return PartialView("Components/_ProductFeature", db.Products.Include("Category").Where(i => i.IsActive == true & i.IsFeature == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }

        #endregion

        #region Blogs
        private BlogSearchViewModel BlogsView(BlogSearchViewModel m)
        {
            if (!string.IsNullOrEmpty(m.Category))
            {
                ViewData["category"] = db.BlogCategories.FirstOrDefault(c => c.Key == m.Category);
            }
            return m;
        }

        //tin-tuc
        //tin-tuc/1
        [HttpGet]
        [Route("tin-tuc/{p:int?}", Name = "tintuc")]
        public ActionResult Blog(int? p, BlogSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/tin-tuc/{0}" + Request.Url.Query;
            return View(BlogsView(model));
        }
        //tin-tuc/khuyen-mai
        //tin-tuc/khuyen-mai/1
        [HttpGet]
        [Route("tin-tuc/{category:minlength(5)}/{p:int?}", Name = "tintuc-loai")]
        public ActionResult Blog(int? p, string category, BlogSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/tin-tuc/" + category + "/{0}" + Request.Url.Query;
            return View(BlogsView(model));
        }
        //2005
        [Route("ajax-tin-tuc")]
        public PartialViewResult AjaxBlogs(int? page, int? pageSize, string sort, BlogSearchViewModel model)
        {
            int TotalItemCount = 0;
            string sortColumn = string.Empty;
         
            if (string.IsNullOrEmpty(sort))
            { sortColumn = "Id desc"; }//default column sort / must be have/ not empty
            else
            {
                sortColumn = EnumHelper<EnumBlogSort>.GetDescriptionValue((EnumBlogSort)Enum.Parse(typeof(EnumBlogSort), sort));
            }
            ViewBag.sortColumn = sort;
            string query = @"select @columns from(
               select ROW_NUMBER() OVER (ORDER BY b.@sortColumn) AS No,b.Id from Blogs b  where b.isActive=1 ".Replace("@sortColumn", sortColumn);

            if (!string.IsNullOrEmpty(model.Key))
            { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

            if (!string.IsNullOrEmpty(model.Category))
            {
                var category=db.BlogCategories.FirstOrDefault(c => c.Key == model.Category);
                if (category!=null)
                {
                    ViewData["category"] = category;
                    query += " and b.[CategoryId]=" + category.Id;
                }
            }

            query += ") AS b";
            //----------for paging
            TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();
        
            int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 6;
            int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
            //take is pagesize , skip is from
            int pfrom = skip + 1;
            int pto = pfrom + take - 1;

            query += $" where No >= {pfrom} AND No <= {pto} order by No";

            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i=>i.Id).ToArray();
            var dt = db.Blogs.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();
                     
            ViewBag.PageSize = take;
            ViewBag.TotalItemCount = TotalItemCount;
            ViewBag.Page = skip == 0 ? 1 : page;
            ViewBag.Data = from d in dt
                           from s in ids
                           where s.Id == d.Id
                           orderby s.No
                           select d;

            return PartialView("Components/AjaxBlogs", model);

        }
        //[Route("ajax-tin-tuc")]
        //public PartialViewResult AjaxBlogs(int? page, int? pageSize, string sortColumn, string sortColumnDir, BlogSearchViewModel model)
        //{
        //    int TotalItemCount = 0;
        //    string query = @"select @columns from Blogs b left join PostCategories i on b.CategoryId = i.Id  where b.isActive=1";

        //    if (!string.IsNullOrEmpty(model.Key))
        //    { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

        //    if (!string.IsNullOrEmpty(model.Category))
        //    {
        //        query += " and i.[Key]='" + model.Category + "'";
        //        ViewData["category"] = db.BlogCategories.FirstOrDefault(c => c.Key == model.Category);
        //    }
        //    //----------for paging
        //    TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();
        //    if (string.IsNullOrEmpty(sortColumn))
        //    {
        //        sortColumn = "b.Id";
        //        sortColumnDir = "desc";
        //    }//default column sort / must be have/ not empty
        //    ViewBag.SortColumn = sortColumn;
        //    ViewBag.SortColumnDir = sortColumnDir;

        //    int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 2;
        //    int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
        //    query += $" ORDER BY {sortColumn} {sortColumnDir} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";

        //    var data = db.Database.SqlQuery<Blog>(query.Replace("@columns", "b.*,i.ID '_CategoryId',i.[Key] '_CategoryKey' ,i.CategoryName '_CategoryTitle'"));

        //    ViewBag.PageSize = take;
        //    ViewBag.TotalItemCount = TotalItemCount;
        //    ViewBag.Page = skip == 0 ? 1 : page;
        //    ViewBag.Data = data;

        //    return PartialView("Components/AjaxBlogs", model);

        //}
        //[OutputCache(CacheProfile = "Cache1DayParams_key")]
        [Route("{key}/tin-tuc")]
        public ActionResult BlogDetail(string key)
        {
            //if (string.IsNullOrEmpty(key))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var m = db.Blogs.FirstOrDefault(i => i.Key == key);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.Similars = db.Blogs.Where(i => i.IsActive == true && m.Id != i.Id && i.CategoryId == m.CategoryId).OrderByDescending(i => i.CreatedDate).Take(10);
            return View(m);
        }


        [ChildActionOnly]
        public PartialViewResult SNewsCategory()
        {
            ViewBag.Category = db.BlogCategories.Where(i => i.IsActive == true).OrderBy(i => i.Index);
            return PartialView("Components/_NewsCategory");
        }
        [ChildActionOnly]
        public PartialViewResult SNewsLastest()
        {
            return PartialView("Components/_NewsLastest", db.Blogs.Include("Category").Where(i => i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }
        [ChildActionOnly]
        public PartialViewResult SNewsFeature()
        {
            return PartialView("Components/_NewsFeature", db.Blogs.Include("Category").Where(i => i.IsActive == true & i.IsFeature == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }

        #endregion


        #region Services
        private ServiceSearchViewModel ServicesView(ServiceSearchViewModel m)
        {
            if (!string.IsNullOrEmpty(m.Category))
            {
                ViewData["category"] = db.ServiceCategories.FirstOrDefault(c => c.Key == m.Category);
            }
            return m;
        }

        //dich-vu
        //dich-vu/1
        [HttpGet]
        [Route("dich-vu/{p:int?}", Name = "dichvu")]
        public ActionResult Service(int? p, ServiceSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/dich-vu/{0}" + Request.Url.Query;
            return View(ServicesView(model));
        }
        //dich-vu/khuyen-mai
        //dich-vu/khuyen-mai/1
        [HttpGet]
        [Route("dich-vu/{category:minlength(5)}/{p:int?}", Name = "dichvu-loai")]
        public ActionResult Service(int? p, string category, ServiceSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/dich-vu/" + category + "/{0}" + Request.Url.Query;
            return View(ServicesView(model));
        }
        //2005
        [Route("ajax-dich-vu")]
        public PartialViewResult AjaxService(int? page, int? pageSize, string sort, ServiceSearchViewModel model)
        {
            int TotalItemCount = 0;
            string sortColumn = string.Empty;

            if (string.IsNullOrEmpty(sort))
            { sortColumn = "Id desc"; }//default column sort / must be have/ not empty
            else
            {
                sortColumn = EnumHelper<EnumServiceSort>.GetDescriptionValue((EnumServiceSort)Enum.Parse(typeof(EnumServiceSort), sort));
            }
            ViewBag.sortColumn = sort;
            string query = @"select @columns from(
               select ROW_NUMBER() OVER (ORDER BY b.@sortColumn) AS No,b.Id from Services b  where b.isActive=1 ".Replace("@sortColumn", sortColumn);

            if (!string.IsNullOrEmpty(model.Key))
            { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

            if (!string.IsNullOrEmpty(model.Category))
            {
                var category = db.ServiceCategories.FirstOrDefault(c => c.Key == model.Category);
                if (category != null)
                {
                    ViewData["category"] = category;
                    query += " and b.[CategoryId]=" + category.Id;
                }
            }

            query += ") AS b";
            //----------for paging
            TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();

            int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 9;
            int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
            //take is pagesize , skip is from
            int pfrom = skip + 1;
            int pto = pfrom + take - 1;

            query += $" where No >= {pfrom} AND No <= {pto} order by No";

            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Services.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();

            ViewBag.PageSize = take;
            ViewBag.TotalItemCount = TotalItemCount;
            ViewBag.Page = skip == 0 ? 1 : page;
            ViewBag.Data = from d in dt
                           from s in ids
                           where s.Id == d.Id
                           orderby s.No
                           select d;

            return PartialView("Components/AjaxService", model);

        }
        //[Route("ajax-dich-vu")]
        //public PartialViewResult AjaxServices(int? page, int? pageSize, string sortColumn, string sortColumnDir, ServiceSearchViewModel model)
        //{
        //    int TotalItemCount = 0;
        //    string query = @"select @columns from Services b left join PostCategories i on b.CategoryId = i.Id  where b.isActive=1";

        //    if (!string.IsNullOrEmpty(model.Key))
        //    { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

        //    if (!string.IsNullOrEmpty(model.Category))
        //    {
        //        query += " and i.[Key]='" + model.Category + "'";
        //        ViewData["category"] = db.ServiceCategories.FirstOrDefault(c => c.Key == model.Category);
        //    }
        //    //----------for paging
        //    TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();
        //    if (string.IsNullOrEmpty(sortColumn))
        //    {
        //        sortColumn = "b.Id";
        //        sortColumnDir = "desc";
        //    }//default column sort / must be have/ not empty
        //    ViewBag.SortColumn = sortColumn;
        //    ViewBag.SortColumnDir = sortColumnDir;

        //    int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 2;
        //    int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
        //    query += $" ORDER BY {sortColumn} {sortColumnDir} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";

        //    var data = db.Database.SqlQuery<Service>(query.Replace("@columns", "b.*,i.ID '_CategoryId',i.[Key] '_CategoryKey' ,i.CategoryName '_CategoryTitle'"));

        //    ViewBag.PageSize = take;
        //    ViewBag.TotalItemCount = TotalItemCount;
        //    ViewBag.Page = skip == 0 ? 1 : page;
        //    ViewBag.Data = data;

        //    return PartialView("Components/AjaxServices", model);

        //}
        //[OutputCache(CacheProfile = "Cache1DayParams_key")]
        [Route("{key}/dich-vu")]
        public ActionResult ServiceDetail(string key)
        {
            //if (string.IsNullOrEmpty(key))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var m = db.Services.FirstOrDefault(i => i.Key == key);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.Similars = db.Services.Where(i => i.IsActive == true && m.Id != i.Id && i.CategoryId == m.CategoryId).OrderByDescending(i => i.CreatedDate).Take(10);
            return View(m);
        }


        [ChildActionOnly]
        public PartialViewResult SServiceCategory()
        {
            ViewBag.Category = db.ServiceCategories.Where(i => i.IsActive == true).OrderBy(i => i.Index);
            return PartialView("Components/_ServiceCategory");
        }
        [ChildActionOnly]
        public PartialViewResult SServiceLastest()
        {
            return PartialView("Components/_ServiceLastest", db.Services.Include("Category").Where(i => i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }
        [ChildActionOnly]
        public PartialViewResult SServiceFeature()
        {
            return PartialView("Components/_ServiceFeature", db.Services.Include("Category").Where(i => i.IsActive == true & i.IsFeature == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }

        #endregion


     

        #region Faqs
        private FaqSearchViewModel FaqsView(FaqSearchViewModel m)
        {
            //if (!string.IsNullOrEmpty(m.Category))
            //{
            //    ViewData["category"] = db.FaqCategories.FirstOrDefault(c => c.Key == m.Category);
            //}
            ViewBag.Category = new SelectList(db.FaqCategories.Where(i => i.IsActive == true).OrderBy(i => i.Index), "Key", "Title", m.Category);
            return m;
        }

        //hoi-dap
        //hoi-dap/1
        [HttpGet]
        [Route("hoi-dap/{p:int?}", Name = "hoidap")]
        public ActionResult Faq(int? p, FaqSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/hoi-dap/{0}" + Request.Url.Query;
            return View(FaqsView(model));
        }
        //hoi-dap/khuyen-mai
        //hoi-dap/khuyen-mai/1
        [HttpGet]
        [Route("hoi-dap/{category:minlength(5)}/{p:int?}", Name = "hoidap-loai")]
        public ActionResult Faq(int? p, string category, FaqSearchViewModel model)
        {
            //ViewBag.UrlPaging = "/hoi-dap/" + category + "/{0}" + Request.Url.Query;
            return View(FaqsView(model));
        }
        //2005
        [Route("ajax-hoi-dap")]
        public PartialViewResult AjaxFaq(int? page, int? pageSize, string sort, FaqSearchViewModel model)
        {
            int TotalItemCount = 0;
            string sortColumn = string.Empty;

            if (string.IsNullOrEmpty(sort))
            { sortColumn = "Id desc"; }//default column sort / must be have/ not empty
            else
            {
                sortColumn = EnumHelper<EnumFaqSort>.GetDescriptionValue((EnumFaqSort)Enum.Parse(typeof(EnumFaqSort), sort));
            }
            ViewBag.sortColumn = sort;
            string query = @"select @columns from(
               select ROW_NUMBER() OVER (ORDER BY b.@sortColumn) AS No,b.Id from Faqs b  where b.isActive=1 ".Replace("@sortColumn", sortColumn);

            if (!string.IsNullOrEmpty(model.Key))
            { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

            if (!string.IsNullOrEmpty(model.Category))
            {
                var category = db.FaqCategories.FirstOrDefault(c => c.Key == model.Category);
                if (category != null)
                {
                    ViewData["category"] = category;
                    query += " and b.[CategoryId]=" + category.Id;
                }
            }

            query += ") AS b";
            //----------for paging
            TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();

            int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 9;
            int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
            //take is pagesize , skip is from
            int pfrom = skip + 1;
            int pto = pfrom + take - 1;

            query += $" where No >= {pfrom} AND No <= {pto} order by No";

            var ids = db.Database.SqlQuery<DataResultLongModels>(query.Replace("@columns", "No,Id")).ToArray();
            var _ids = ids.Select(i => i.Id).ToArray();
            var dt = db.Faqs.Where(o => _ids.Any(i => i.Equals(o.Id))).ToArray();

            ViewBag.PageSize = take;
            ViewBag.TotalItemCount = TotalItemCount;
            ViewBag.Page = skip == 0 ? 1 : page;
            ViewBag.Data = from d in dt
                           from s in ids
                           where s.Id == d.Id
                           orderby s.No
                           select d;

            return PartialView("Components/AjaxFaq", model);

        }
        //[Route("ajax-hoi-dap")]
        //public PartialViewResult AjaxFaqs(int? page, int? pageSize, string sortColumn, string sortColumnDir, FaqSearchViewModel model)
        //{
        //    int TotalItemCount = 0;
        //    string query = @"select @columns from Faqs b left join PostCategories i on b.CategoryId = i.Id  where b.isActive=1";

        //    if (!string.IsNullOrEmpty(model.Key))
        //    { query += " and  b.Title like N'%" + model.Key + "%' collate SQL_Latin1_General_CP1_CI_AI"; }

        //    if (!string.IsNullOrEmpty(model.Category))
        //    {
        //        query += " and i.[Key]='" + model.Category + "'";
        //        ViewData["category"] = db.FaqCategories.FirstOrDefault(c => c.Key == model.Category);
        //    }
        //    //----------for paging
        //    TotalItemCount = db.Database.SqlQuery<int>(query.Replace("@columns", "count(b.Id)")).FirstOrDefault();
        //    if (string.IsNullOrEmpty(sortColumn))
        //    {
        //        sortColumn = "b.Id";
        //        sortColumnDir = "desc";
        //    }//default column sort / must be have/ not empty
        //    ViewBag.SortColumn = sortColumn;
        //    ViewBag.SortColumnDir = sortColumnDir;

        //    int take = pageSize.HasValue ? Convert.ToInt32(pageSize) : 2;
        //    int skip = page.HasValue ? Convert.ToInt32(page - 1) * take : 0;
        //    query += $" ORDER BY {sortColumn} {sortColumnDir} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";

        //    var data = db.Database.SqlQuery<Faq>(query.Replace("@columns", "b.*,i.ID '_CategoryId',i.[Key] '_CategoryKey' ,i.CategoryName '_CategoryTitle'"));

        //    ViewBag.PageSize = take;
        //    ViewBag.TotalItemCount = TotalItemCount;
        //    ViewBag.Page = skip == 0 ? 1 : page;
        //    ViewBag.Data = data;

        //    return PartialView("Components/AjaxFaqs", model);

        //}
        //[OutputCache(CacheProfile = "Cache1DayParams_key")]
        [Route("{key}/hoi-dap")]
        public ActionResult FaqDetail(string key)
        {
            //if (string.IsNullOrEmpty(key))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var m = db.Faqs.FirstOrDefault(i => i.Key == key);
            if (m == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                   return PartialView("Components/_FaqDetail",m);
            }
            ViewBag.Similars = db.Faqs.Where(i => i.IsActive == true && m.Id != i.Id && i.CategoryId == m.CategoryId).OrderByDescending(i => i.CreatedDate).Take(10);
            return View(m);
        }


        [ChildActionOnly]
        public PartialViewResult SFaqCategory()
        {
            ViewBag.Category = db.FaqCategories.Where(i => i.IsActive == true).OrderBy(i => i.Index);
            return PartialView("Components/_FaqCategory");
        }
        [ChildActionOnly]
        public PartialViewResult SFaqLastest()
        {
            return PartialView("Components/_FaqLastest", db.Faqs.Include("Category").Where(i => i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }
        [ChildActionOnly]
        public PartialViewResult SFaqFeature()
        {
            return PartialView("Components/_FaqFeature", db.Faqs.Include("Category").Where(i => i.IsActive == true & i.IsFeature == true).OrderByDescending(i => i.CreatedDate).Take(5));
        }

        #endregion

      

        public ActionResult About()
        {
            return View();
        }

    }
}