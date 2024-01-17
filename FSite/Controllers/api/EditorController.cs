using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.IO;

using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using FSite.Models;
using System.Text.RegularExpressions;

namespace FSite.Controllers.api
{
    public class EditorController : ApiController
    {

        class Media
        {
            public string path;
            public string thumb;
            public string name;
            public DateTime datecreate;
            public long length;
        }

        //EditorController()
        //{
        //    /* check for existence on startup */

        //}

        /* supported sort type: last_modified_dsc, last_modified_asc, abc_dsc, abc_asc */
        [HttpGet]
        public JToken List(string key = "", string type = "image", string syntax = "", int page = 1, int pagesize = 10, string sort = "last_modified_dsc")
        {

            string PATH_RAW = "/Uploads/";
            string PATH_THUMB = "/Uploads/";
            string MEDIA_ROOT = string.Empty;
            string MEDIA_THUMBS = string.Empty;
            List<string> ALLOWED_EXTENSIONS = new List<string>();
            if (type == "image")
            {
                PATH_RAW += $"images/{syntax}/";
                PATH_THUMB += $"thumbs/{syntax}/";
                MEDIA_ROOT = HttpContext.Current.Server.MapPath(PATH_RAW);
                MEDIA_THUMBS = HttpContext.Current.Server.MapPath(PATH_THUMB);
                ALLOWED_EXTENSIONS.AddRange(new List<string>() { ".jpg", ".png", ".bmp", ".jpeg" });
            }
            else
            {
                PATH_RAW += $"images/{syntax}/";
                MEDIA_ROOT = HttpContext.Current.Server.MapPath(PATH_RAW);
                MEDIA_THUMBS = HttpContext.Current.Server.MapPath(PATH_THUMB);
                ALLOWED_EXTENSIONS.AddRange(new List<string>() { ".jpg", ".png", ".bmp", ".jpeg", ".mp3", ".mp4", ".avi" });
            }

            var lst = new DirectoryInfo(MEDIA_ROOT).GetFiles("*.*", SearchOption.TopDirectoryOnly).AsQueryable();

            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(item => ALLOWED_EXTENSIONS.Contains(item.Extension.ToLower()) && item.Name.ToLower().Contains(key.ToLower()));

            //if (!string.IsNullOrEmpty(syntax))//project_ , product_ , blog_ ....
            //    lst = lst.Where(item => item.Name.ToLower().StartsWith(syntax.ToLower()));

            /* after filtering, sort it */
            switch (sort)
            {
                case "abc_dsc":
                    lst = lst.OrderByDescending(i => i.Name);
                    break;
                case "abc_asc":
                    lst = lst.OrderBy(i => i.Name);
                    break;
                case "last_modified_asc":
                    lst = lst.OrderBy(i => i.LastWriteTime);
                    break;
                default:
                    lst = lst.OrderByDescending(i => i.LastWriteTime);
                    break;
            }

            /* I don't care about cache miss here
			 * everytime new image is uploaded, a thumb file is automatically created
			 * if you're facing cache miss issue, it's your fault. Call /api/media/verify to rebuild cache 
			 * Good luck
			 */

            /* now create our list */
            var totalItem = (Int32)Math.Ceiling(lst.Count() / (float)pagesize);
            var media = lst.Skip((page - 1) * pagesize).Take(pagesize)
                .Select(item => new Media { path = PATH_RAW + item.Name, thumb = PATH_THUMB + item.Name, name = item.Name, length = item.Length, datecreate = item.CreationTime });

            return JToken.FromObject(new { status = true, media, page, pagesize, total = totalItem });
        }

        /* upload new media to collection, create thumb file after that */
        [HttpPost]
        public async Task<JToken> Upload()
        {
            string syntax = HttpContext.Current.Request["syntax"];
            string type = HttpContext.Current.Request["type"];
            if (string.IsNullOrEmpty(type))
                type = "image";

            string PATH_RAW = "/Uploads/";
            string PATH_THUMB = "/Uploads/";
            string MEDIA_ROOT = string.Empty;
            string MEDIA_THUMBS = string.Empty;
            List<string> ALLOWED_EXTENSIONS = new List<string>();
            if (type == "image")
            {
                PATH_RAW += $"images/{syntax}/";
                PATH_THUMB += $"thumbs/{syntax}/";
                MEDIA_ROOT = HttpContext.Current.Server.MapPath(PATH_RAW);
                MEDIA_THUMBS = HttpContext.Current.Server.MapPath(PATH_THUMB);
                ALLOWED_EXTENSIONS.AddRange(new List<string>() { ".jpg", ".png", ".bmp", ".jpeg" });
            }
            bool status = false;
            var msg = "";

            if (!Request.Content.IsMimeMultipartContent())
            {
                msg = "unsupported request, need mime";
                return JToken.FromObject(new { msg = msg, status = status });
            }

            // we will receive upload files to App_Data folder as a temporary place
            string tmpPlace = HttpContext.Current.Server.MapPath("~/App_Data/");
            var provider = new MultipartFormDataStreamProvider(tmpPlace);

            List<Media> media = new List<Media>();

            await Request.Content.ReadAsMultipartAsync(provider);
            // done, now move all files to their should-be destination
            foreach (MultipartFileData file in provider.FileData)
            {
                string cname = file.Headers.ContentDisposition.FileName.Trim(new char[] { '\"', ' ' });
                string tmp = "-" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                var newName = Path.GetFileNameWithoutExtension(cname) + tmp + Path.GetExtension(cname);
                var newPath = MEDIA_ROOT + newName;

                File.Move(file.LocalFileName, newPath);
                if (type == "image")
                {
                    if (this._createThumbImageFile(newPath, MEDIA_THUMBS))
                    {
                        media.Add(new Media { path = PATH_RAW + newName, thumb = PATH_THUMB + newName, name = newName });
                    }
                }
                //else
                //{
                //	File.Delete(newPath);
                //}
            }

            status = true;
            return JToken.FromObject(new { status, msg, media });
        }

        /* verify for thumbs existence and create them */
        //[HttpPost]
        //public JToken Verify()
        //{
        //    var root_list = new DirectoryInfo(MEDIA_ROOT).GetFiles("*.*", SearchOption.TopDirectoryOnly)
        //        .Where(item => ALLOWED_EXTENSIONS.Contains(item.Extension.ToLower()));

        //    int newCreateCount = 0;
        //    foreach (var item in root_list)
        //    {
        //        var thumbPath = MEDIA_THUMBS + item.Name;
        //        if (!File.Exists(thumbPath))
        //        {
        //            if (!this._createThumbImageFile(item.FullName))
        //            {
        //                return JToken.FromObject(new  { msg = "failed to convert file", status = "failed" });
        //            }
        //            else
        //            {
        //                newCreateCount++;
        //            }
        //        }
        //    }

        //    return JToken.FromObject(new { status = "success", msg = "created thumbs for " + newCreateCount + " files" });
        //}

        [HttpPost]
        public JToken Remove(string name)
        {
            string syntax = HttpContext.Current.Request["syntax"];
            if (!string.IsNullOrEmpty(syntax))
            {
                if (name.ToLower().StartsWith(syntax.ToLower() + "-"))
                {
                    return JToken.FromObject(new { status = false, msg = "Bạn không có quyền xóa hình ảnh này." });
                }
            }
            string type = HttpContext.Current.Request["type"];
            if (string.IsNullOrEmpty(type))
                type = "image";
            string PATH_RAW = "/Uploads/";
            string PATH_THUMB = "/Uploads/";
            string MEDIA_ROOT = string.Empty;
            string MEDIA_THUMBS = string.Empty;
            List<string> ALLOWED_EXTENSIONS = new List<string>();
            if (type == "image")
            {
                PATH_RAW += $"images/{syntax}/";
                PATH_THUMB += $"thumbs/{syntax}/";
                MEDIA_ROOT = HttpContext.Current.Server.MapPath(PATH_RAW);
                MEDIA_THUMBS = HttpContext.Current.Server.MapPath(PATH_THUMB);
            }
            /* SECURE: remove all dangerous string */
            name = name.Replace('\\', '_');
            name = name.Replace('/', '_');
            if (File.Exists(MEDIA_ROOT + name))
            {
                // remove both
                try
                {
                    File.Delete(MEDIA_ROOT + name);
                    if (File.Exists(MEDIA_THUMBS + name)) File.Delete(MEDIA_THUMBS + name);
                }
                catch (Exception e)
                {
                    return JToken.FromObject(new { status = false, msg = e.Message });
                }

                return JToken.FromObject(new { status = true });
            }

            return JToken.FromObject(new { status = false, msg = "invalid name or not found" });
        }

        private bool _createThumbImageFile(string originalFile, string thumbspath)
        {

            int THUMB_MAX_WIDTH = 200;
            int THUMB_MAX_HEIGHT = 200;
            //string ext = Path.GetExtension(originalFile).ToLower();
            //if (!ALLOWED_EXTENSIONS.Contains(ext)) return false;

            Image image = Image.FromFile(originalFile);

            var originalWidth = image.Width;
            var originalHeight = image.Height;

            // Figure out the ratio
            double ratioX = (double)THUMB_MAX_WIDTH / (double)originalWidth;
            double ratioY = (double)THUMB_MAX_HEIGHT / (double)originalHeight;
            // use whichever multiplier is bigger, our goal is to make min-width and min-height ~ THUMB_MAX_WIDTH
            double ratio = ratioX > ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);
            Image thumbnail = new Bitmap(newWidth, newHeight); // changed parm names
            Graphics graphic = Graphics.FromImage(thumbnail);

            /* prepare for parameter then draw */
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.DrawImage(image, 0, 0, newWidth, newHeight);

            /* we will use maximum encode quality for small images */
            System.Drawing.Imaging.ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            thumbnail.Save(thumbspath + Path.GetFileName(originalFile), info[1], encoderParameters);

            // release them
            thumbnail.Dispose();
            image.Dispose();

            return true;
        }


        #region Cropper
        [HttpPost]
        public async Task<JToken> Cropper(FileCropperViewModel model)
        {
            List<Media> media = new List<Media>();
            Match imageMatch = Regex.Match(model.filebase, @"^data:(?<mimetype>[^;]+);base64,(?<data>.+)$");
            //            if (!imageMatch.Success)
            //                throw new ArgumentException("imageData is in unknown format", nameof(imageData));

            //            string mimeType = imageMatch.Groups["mimetype"].Value;
            //            Match imageType = Regex.Match(mimeType, @"^[^/]+/(?<type>.+?)$");
            //            if (!imageType.Success)
            //                throw new ArgumentException($"mimeType format invalid for {mimeType}", nameof(mimeType));

            //            string fileExtension = imageType.Groups["type"].Value;
            //            byte[] data = Convert.FromBase64String(imageMatch.Groups["data"].Value);
            byte[] data = System.Convert.FromBase64String(imageMatch.Groups["data"].Value);
            Uri uri = new Uri(HttpContext.Current.Server.MapPath(model.url));
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            if (uri.IsFile)
            {
                using (Image image = Image.FromStream(new MemoryStream(data)))
                {
                    string newPath = HttpContext.Current.Server.MapPath(model.url);//old image
                    string MEDIA_THUMBS = newPath.Replace("images", "thumbs").Replace(filename, "");

                    image.Save(newPath);
                    if (this._createThumbImageFile(newPath, MEDIA_THUMBS))
                    {
                        media.Add(new Media { path = model.url, thumb = MEDIA_THUMBS, name = filename });
                    }
                }
            }
          
            return JToken.FromObject(new { status=true, msg="Thành công", media });
        }

//        [HttpPost]
//        public JsonResult UploadImageAsync(int total, string imageData, int? type, int? w, int? h, string idpicture)
//        {

//            if (dzfile == null)
//            {
//                dzfile = new Models.ViewModels.DzFile();
//                dzfile.totalfinish = total;
//            }
//            if (string.IsNullOrEmpty(imageData))
//                throw new ArgumentNullException(nameof(imageData), "No image data received");

//            Match imageMatch = Regex.Match(imageData, @"^data:(?<mimetype>[^;]+);base64,(?<data>.+)$");
//            if (!imageMatch.Success)
//                throw new ArgumentException("imageData is in unknown format", nameof(imageData));

//            string mimeType = imageMatch.Groups["mimetype"].Value;
//            Match imageType = Regex.Match(mimeType, @"^[^/]+/(?<type>.+?)$");
//            if (!imageType.Success)
//                throw new ArgumentException($"mimeType format invalid for {mimeType}", nameof(mimeType));

//            string fileExtension = imageType.Groups["type"].Value;
//            byte[] data = Convert.FromBase64String(imageMatch.Groups["data"].Value);

//            if (data != null)
//            {
//                int picid = int.Parse(idpicture);

//                Leasing_Listings_Pictures pic = db.Leasing_Listings_Pictures.Where(x => x.Leasing_Picture_ID == picid).FirstOrDefault();

//                Uri uri = new Uri(Server.MapPath(pic.Leasing_Picture_Path));
//                if (uri.IsFile)
//                {
//                    string path, pathwatermark;
//#pragma warning disable CS0168 // The variable 'x' is declared but never used
//#pragma warning disable CS0168 // The variable 'y' is declared but never used
//                    int x, y;
//#pragma warning restore CS0168 // The variable 'y' is declared but never used
//#pragma warning restore CS0168 // The variable 'x' is declared but never used
//                    string filename = System.IO.Path.GetFileName(uri.LocalPath);
//                    var lst = ImageSizes.Lists;
//                    // if (type==1)//one else is all
//                    if (w != 0)//all
//                        lst = lst.Where(i => i.width == w & i.height == h).ToList();

//                    for (int j = 0; j < lst.Count; j++)
//                    {
//                        path = Server.MapPath("/Uploads/_thumb/properties/" + lst[j].width + "x" + lst[j].height + "/" + filename);
//                        pathwatermark = string.Format(Configuration.Watermark, lst[j].width, lst[j].height);

//                        using (Image image = Image.FromStream(new MemoryStream(data)))
//                        using (Bitmap watermarkImage = new Bitmap(Server.MapPath(pathwatermark)))
//                        using (Graphics imageGraphics = Graphics.FromImage(image))
//                        {
//                            try
//                            {
//                                //watermarkImage.SetResolution(imageGraphics.DpiX, imageGraphics.DpiY);
//                                //x = ((image.Width - watermarkImage.Width) / 2);
//                                //y = ((image.Height - watermarkImage.Height) / 2);
//                                //imageGraphics.DrawImage(watermarkImage, x, y, watermarkImage.Width, watermarkImage.Height);
//                                //image.Save(path, ImageFormat.Jpeg);
//                                MakePhoto((int)lst[j].width, (int)lst[j].height, image, path, watermarkImage);
//                                dzfile.Currentfile = new DropZoneFile()
//                                {
//                                    deleteUrl = path,
//                                    thumbnailUrl = path,
//                                    url = path,
//                                    type = fileExtension,
//                                    name = filename,
//                                    // size = image
//                                };
//                            }
//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
//                            catch (Exception ex)
//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
//                            {
//                                throw;
//                            }
//                            finally
//                            {
//                                if (image != null)
//                                    image.Dispose();

//                                if (imageGraphics != null)
//                                    imageGraphics.Dispose();
//                                if (watermarkImage != null)
//                                    watermarkImage.Dispose();

//                            }

//                        }
//                    }




//                }

//            }
//            if (dzfile.Isfinish)
//            {
//                return Json(dzfile, JsonRequestBehavior.AllowGet);
//            }
//            else
//            {
//                return Json(new DzFile() { Isfinish = dzfile.Isfinish, files = dzfile.files.OrderByDescending(x => x.index).Take(1).ToArray() }, JsonRequestBehavior.AllowGet);
//            }
//            // Do something with your binary image data
//        }
        #endregion
    }
}
