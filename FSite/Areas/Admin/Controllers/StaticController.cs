using FSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace FSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class StaticController : Controller
    {
        // GET: Admin/Static
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

            List<StaticViewModel> lstProject = new List<StaticViewModel>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/App_Data/Sys/Static.xml"));
            DataView dvPrograms;
            dvPrograms = ds.Tables[0].DefaultView;
            dvPrograms.Sort = "Id";

            
            foreach (DataRowView dr in dvPrograms)
            {
                StaticViewModel model = new StaticViewModel();
                model.Id = Convert.ToInt32(dr[0]);
                model.Name = Convert.ToString(dr[1]);
                model.Location = Convert.ToString(dr[2]);
                lstProject.Add(model);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                lstProject = lstProject.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            DatatablesResultModels data = new DatatablesResultModels();

            data.draw = int.Parse(draw);
            data.recordsTotal = lstProject.Count;
            data.recordsFiltered = data.recordsTotal;

            //take is pagesize , skip is from
            int pfrom = skip + 1;
            int pto = pfrom + pageSize - 1;

      
           
            data.data = lstProject.Skip(skip).Take(pageSize);

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        StaticViewModel model = new StaticViewModel();
        // GET: Static/Edit/5
        public ActionResult Edit(int id)
        {
            int Id = Convert.ToInt32(id);
            if (Id > 0)
            {
                XDocument oXmlDocumentParent = XDocument.Load(Server.MapPath("~/App_Data/Sys/Static.xml"));
                var itemp = (from item in oXmlDocumentParent.Descendants("StaticViewModel")
                             where Convert.ToInt32(item.Element("Id").Value) == Id
                             select new StaticViewModel
                             {
                                 Id = Convert.ToInt32(item.Element("Id").Value),
                                 Name = item.Element("Name").Value,
                                 Location = item.Element("Location").Value,
                             }).SingleOrDefault();


                XDocument oXmlDocument = XDocument.Load(Server.MapPath($"~/App_Data/Sys/{itemp.Location}.xml"));
                var items = (from item in oXmlDocument.Descendants("StaticViewModel")
                             where Convert.ToInt32(item.Element("Id").Value) == Id
                             select new StaticViewModel
                             {
                                 Id = Convert.ToInt32(item.Element("Id").Value),
                                 MetaStr = item.Element("MetaStr").Value,
                             }).SingleOrDefault();
                if (items != null)
                {
                    model.Id = itemp.Id;
                    model.Name = itemp.Name;
                    model.Location = itemp.Location;
                    model.MetaStr = items.MetaStr;
                    StaticFrontEndMetaData meta = new StaticFrontEndMetaData();
                    //if (!string.IsNullOrEmpty(items.Meta))
                    //{
                    //    var metaDataString = model.Meta.Data["StaticFrontEndMetaData"];
                    //    meta = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticFrontEndMetaData>(metaDataString);
                    //}
                    if (!string.IsNullOrEmpty(items.MetaStr))
                    {
                        var metaDataString = model.MetaStr;
                        meta = JsonConvert.DeserializeObject<StaticFrontEndMetaData>(metaDataString);
                    }
                    ViewData["meta"] = meta;
                    //   return PartialView("Components/" + model.Location);
                    // model.Meta = items.Meta;
                }
                model.IsEdit = true;
                return View(model);
            }
            else
            {
                model.IsEdit = false;
                return View(model);
            }

        }
        //JsonSerializer _jsonWriter = new JsonSerializer
        //{
        //    NullValueHandling = NullValueHandling.Ignore
        //};
        // POST: Static/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(StaticViewModel mdl, StaticFrontEndMetaData meta)
        {
            #region Meta Dictionary<string, string>

            mdl.MetaStr = JsonConvert.SerializeObject(meta,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            // mdl.MetaStr = Newtonsoft.Json.JsonConvert.SerializeObject(meta);
            //Dictionary<string, string> dic = new Dictionary<string, string>()
            //    {{ "MetaData",Newtonsoft.Json.JsonConvert.SerializeObject(meta)}};
            //mdl.Meta = new MetaData();
            //mdl.Meta.Data = dic;
            #endregion

            if (mdl.Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath($"~/App_Data/Sys/{mdl.Location}.xml"));
                var items = (from item in xmlDoc.Descendants("StaticViewModel") select item).ToList();
                XElement selected = items.Where(p => p.Element("Id").Value == mdl.Id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath($"~/App_Data/Sys/{mdl.Location}.xml"));
                xmlDoc.Element("StaticViewModels").Add(new XElement("StaticViewModel",
                    new XElement("Id", mdl.Id),
                new XElement("MetaStr", mdl.MetaStr)
                    ));
                xmlDoc.Save(Server.MapPath($"~/App_Data/Sys/{mdl.Location}.xml"));

            }
            else
            {
                XmlDocument oXmlDocument = new XmlDocument();
                oXmlDocument.Load(Server.MapPath($"~/App_Data/Sys/{mdl.Location}.xml"));
                XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("StaticViewModel");
                var x = oXmlDocument.GetElementsByTagName("Id");
                int Max = 0;
                foreach (XmlElement item in x)
                {
                    int EId = Convert.ToInt32(item.InnerText.ToString());
                    if (EId > Max)
                    {
                        Max = EId;
                    }
                }
                Max = Max + 1;
                XDocument xmlDoc = XDocument.Load(Server.MapPath($"~/App_Data/Sys/{mdl.Location}.xml"));
                xmlDoc.Element("StaticViewModels").Add(new XElement("StaticViewModel", new XElement("Id", Max), new XElement("Name", mdl.Name),
                    new XElement("Location", mdl.Location),
                      new XElement("MetaStr", mdl.MetaStr)
                    ));
                xmlDoc.Save(Server.MapPath($"~/App_Data/Sys/{mdl.Location}.xml"));

            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { result =true}, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}