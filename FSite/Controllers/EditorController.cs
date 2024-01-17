using FSite.Models;
using System.Web.Mvc;

namespace FSite.Controllers
{
    public class EditorController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("public/static/{partical}")]
        public ActionResult Static(string id, string partical, string type, string syntax, string field_id)//[type = image , video...][syntax= project , product, news ....,post]
        {
            ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContext, partical, null);
            ViewBag.syntax = syntax;
            ViewBag.type = type;
            ViewBag.field_id = field_id;
            // check if view name requested is not found
            if (result == null || result.View == null)
            {
                return new HttpNotFoundResult();
            }
            // otherwise just return the view
            return View(partical);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("public/cropper/{partical}")]
        public PartialViewResult Cropper(FileCropperViewModel model)
        {
            // otherwise just return the view
            return PartialView(model.partical,model);
        }

        //    [HttpGet]
        //    public JsonResult Get(string key = "",string type="image",string syntax="", int page = 1, int pagesize = 10, string sort = "last_modified_dsc")
        //    {
        //        string PATH_RAW = "/Uploads/{0}";
        //        string PATH_THUMB = "/Uploads/{0}";
        //        string MEDIA_ROOT = string.Empty;
        //        string MEDIA_THUMBS = string.Empty;
        //        List<string> ALLOWED_EXTENSIONS =new List<string>();
        //        if (type == "image")
        //        {
        //            MEDIA_ROOT = Server.MapPath(string.Format(PATH_RAW, "images/" + syntax));
        //            MEDIA_THUMBS = Server.MapPath(string.Format(PATH_RAW, "thumbs/" + syntax));
        //            ALLOWED_EXTENSIONS.AddRange(new List<string>() { ".jpg", ".png", ".bmp", ".jpeg" });
        //        }

        //        var lst = new DirectoryInfo(MEDIA_ROOT).GetFiles().AsQueryable();

        //        if (!string.IsNullOrEmpty(key))
        //            lst= lst.Where(item => ALLOWED_EXTENSIONS.Contains(item.Extension.ToLower()) && item.Name.ToLower().Contains(key.ToLower()));

        //        if (!string.IsNullOrEmpty(syntax))//project_ , product_ , blog_ ....
        //            lst = lst.Where(item =>  item.Name.ToLower().StartsWith(key.ToLower()));

        //        /* after filtering, sort it */
        //        switch (sort)
        //        {
        //            case "abc_dsc":
        //              lst=  lst.OrderByDescending(i=>i.Name);
        //                break;
        //            case "abc_asc":
        //                lst = lst.OrderBy(i => i.Name);
        //                break;
        //            case "last_modified_asc":
        //                lst = lst.OrderBy(i => i.LastWriteTime);
        //                break;
        //            default:
        //                lst = lst.OrderByDescending(i => i.LastWriteTime);
        //                break;
        //        }

        //        /* I don't care about cache miss here
        //* everytime new image is uploaded, a thumb file is automatically created
        //* if you're facing cache miss issue, it's your fault. Call /api/media/verify to rebuild cache 
        //* Good luck
        //*/

        //        /* now create our list */
        //        var totalItem = (Int32)Math.Ceiling(lst.Count() / (float)pagesize);
        //        var media = lst.Skip((page - 1) * pagesize).Take(pagesize)
        //            .Select(item => new Media { path = PATH_RAW + item.Name, thumb = PATH_THUMB + item.Name, name = item.Name,length=item.Length,datecreate=item.CreationTime });

        //        return Json(new { status = "success", media, page, pagesize, total = totalItem }, JsonRequestBehavior.AllowGet);
        //    }
    }
}