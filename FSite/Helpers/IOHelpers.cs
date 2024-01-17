using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace FSite.Helpers
{
    public class IOHelpers
    {
        public static string ReadFile(string path)
        {
            var reader = new System.IO.StreamReader(path);
            var contents = reader.ReadToEnd();
            return contents;
        }
        public static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
        public static Image ResizeByWidth(Image img, int width)
        {
            // lấy chiều rộng và chiều cao ban đầu của ảnh
            int originalW = img.Width;
            int originalH = img.Height;

            // lấy chiều rộng và chiều cao mới tương ứng với chiều rộng truyền vào của ảnh (nó sẽ giúp ảnh của chúng ta sau khi resize vần giứ được độ cân đối của tấm ảnh
            int resizedW = width;
            int resizedH = (originalH * resizedW) / originalW;

            // tạo một Bitmap có kích thước tương ứng với chiều rộng và chiều cao mới
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            // tạo mới một đối tượng từ Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.High;
            // vẽ lại ảnh với kích thước mới
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            // gải phóng resource cho đối tượng graphic
            graphic.Dispose();
            // trả về anh sau khi đã resize
            return (Image)bmp;
        }
        //delete file by fullmane 
        public static bool FileDelete( string filename, string syntax)//by systax
        {
            string rootpath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            bool _result = false;
            string _mes = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(syntax))
            {
                 filename = System.IO.Path.GetFileName(filename);

                string[] dirs = { "thumbs", "images" };
                string full = string.Empty;
                for (int _i = 0; _i < dirs.Length; _i++)
                {
                   
                    full = System.IO.Path.Combine($"{rootpath}Uploads\\{dirs[_i]}\\{syntax}\\", filename);
                    //    full = System.IO.Path.Combine($"{rootpath}/Uploads/{dirs[_i]}/{syntax}/", filename);
                        if (System.IO.File.Exists(full))
                    {
                        System.IO.File.Delete(full);
                    }
                }
                    _result = true;
                    _mes = "Xóa thành công";
                }
                else
                {
                    _mes = "Xóa thất bại!!!";
                }
            }
            catch (Exception ex)
            {
                _mes = ex.Message;
            }
            return _result;
        }
        //public static bool FileDelete(string rootpath, string filename, string syntax)//by systax
        //{
        //    bool _result = false;
        //    string _mes = string.Empty;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(syntax))
        //    {
        //         filename = System.IO.Path.GetFileName(filename);

        //        string[] dirs = { "thumbs", "images" };
        //        string full = string.Empty;
        //        for (int _i = 0; _i < dirs.Length; _i++)
        //        {

        //            full = System.IO.Path.Combine($"{rootpath}/Uploads/{dirs[_i]}/{syntax}/", filename);
        //            if (System.IO.File.Exists(full))
        //            {
        //                System.IO.File.Delete(full);
        //            }
        //        }
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
        //    return _result;
        //}
        #region Razor
        /// <summary>
        /// Generate an HTML document from the specified Razor template and model.
        /// </summary>
        /// <param name="rootpath">The path to the folder containing the Razor templates</param>
        /// <param name="templatename">The name of the Razor template (.cshtml)</param>
        /// <param name="templatekey">The template key used for caching Razor templates which is essential for improved performance</param>
        /// <param name="model">The model containing the information to be supplied to the Razor template</param>
        /// <returns></returns>
        public static string RazorRunCompile(string rootpath, string templatename, string templatekey, object model)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(rootpath) || string.IsNullOrEmpty(templatename) || model == null) return result;

            string templateFilePath = Path.Combine(rootpath, templatename);

            if (File.Exists(templateFilePath))
            {
                string template = File.ReadAllText(templateFilePath);

                if (string.IsNullOrEmpty(templatekey))
                {
                    templatekey = Guid.NewGuid().ToString();
                }
                //   dynamic deValues = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(model);
                //  RazorEngine.Razor.
                // result = Engine.Razor.RunCompile(template, templatekey, null, model);

                string Values = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                //DeSerializeObject
                dynamic deValues = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(Values);
                foreach (var item in deValues)
                {
                    try
                    {
                        template = template.Replace($"@ViewBag.{item.Key}", item.Value);  
                    }
                    catch (Exception ex)
                    {

                    
                    }
                  
                }
                result = template;
            }
            return result;
        }
        #endregion

    }
}