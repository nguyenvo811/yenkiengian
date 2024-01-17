using System;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Linq;

using System.Web;
using System.Threading.Tasks;
using FSite.Areas.Admin.Models;
using FSite.Helpers;

namespace FSite.Controllers.api
{
    public class FileController : ApiController
    {
        [HttpPost]
        public async Task<JToken> Upload()
        {   bool isSavedSuccessfully = false;
            string syntax = HttpContext.Current.Request["syntax"];
            string type = HttpContext.Current.Request["type"];
            string size="360,847";//default small , large
            
            if (string.IsNullOrEmpty(type))
                type = "image";

            if (string.IsNullOrEmpty(syntax))
                syntax = HttpContext.Current.Request.UrlReferrer.AbsolutePath.Split('/')[2] ; //Blog, Product
           
            System.Drawing.Image img;
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings.Get("ImgSize")))
                size = System.Configuration.ConfigurationManager.AppSettings.Get("ImgSize");
             if (!string.IsNullOrEmpty(HttpContext.Current.Request["size"]))
                size = HttpContext.Current.Request["size"];
            
         //   int[] imgSize = { 360, 847 };
           int[] imgSize = size.Split(',').Select(s=>Convert.ToInt32(s)).ToArray();

            string[] dirs = { "thumbs", "images" };
            var files = HttpContext.Current.Request.Files;
            string filename = string.Empty;
            string fileSavePath = string.Empty;
            DropZoneModels pis = new DropZoneModels();
            DropZoneItem pi;
            foreach (string item in files)
            {
                fileSavePath = string.Empty;
                filename = string.Empty;
                var httpPostedFile = files[item];
                if (httpPostedFile != null && httpPostedFile.ContentLength != 0)
                {
                    pi = new DropZoneItem();
                    filename = $"{syntax}-{Guid.NewGuid().ToString().Substring(0, 7)}-{httpPostedFile.FileName}";
                    fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath($"~/Uploads/{syntax}"), filename);//file gốc                                                                               // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(fileSavePath);
                    img = System.Drawing.Image.FromFile(fileSavePath);
                    for (int _i = 0; _i < imgSize.Length; _i++)
                    {
                        var wi = IOHelpers.ResizeByWidth(img, imgSize[_i]);
                        wi.Save(System.IO.Path.Combine(HttpContext.Current.Server.MapPath($"/Uploads/{dirs[_i]}/{syntax}"), filename));
                        wi.Dispose();
                    }
                    pi.fileName = httpPostedFile.FileName;
                    pi.reName = filename;
                    pi.smallPicPath = $"/Uploads/{dirs[0]}/{syntax}/{filename}";
                    pi.largePicPath = $"/Uploads/{dirs[1]}/{syntax}/{filename}";
                    pi.size = httpPostedFile.ContentLength;
                    pis.pis.Add(pi);
                }
                isSavedSuccessfully = true;
            }
          //  return Json(new { Message = pis.ToString(), pis = pis });
            return JToken.FromObject(new { Message = pis.ToString(),files= pis.pis });
        }
        [HttpPost]
        public async Task<JToken> Delete()
        {
            var _request = HttpContext.Current.Request;
            var largePicPath = _request["largePicPath"];
            string syntax = HttpContext.Current.Request["syntax"];
            if (string.IsNullOrEmpty(syntax))
                syntax = _request.UrlReferrer.AbsolutePath.Split('/')[2];
            
           string filename = System.IO.Path.GetFileName(largePicPath);
           IOHelpers.FileDelete(filename, syntax);
            return JToken.FromObject(new { Message = ""});
        }

        //void FileDelete(string filename,string syntax)
        //{
        //    bool _result = false;
        //    string _mes = string.Empty;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(filename))
        //        {
        //            if (string.IsNullOrEmpty(syntax))
        //            {
        //                syntax = HttpContext.Current.Request.UrlReferrer.AbsolutePath.Split('/')[2]; //Blog, Product
        //            }
        //            filename = System.IO.Path.GetFileName(filename);
        //            string[] dirs = { "thumbs", "images" };
        //            string full = string.Empty;
        //            for (int _i = 0; _i < dirs.Length; _i++)
        //            {
        //                full = System.IO.Path.Combine(HttpContext.Current.Server.MapPath($"/Uploads/{dirs[_i]}/{syntax}/"), filename);
        //                if (System.IO.File.Exists(full))
        //                {
        //                    System.IO.File.Delete(full);
        //                }
        //            }
        //            _result = true;
        //            _mes = "Xóa thành công";
        //        }
        //        else
        //        {
        //            _mes = "Xóa thất bại!!!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _mes = ex.Message;
        //    }
        //}

    
    }
}