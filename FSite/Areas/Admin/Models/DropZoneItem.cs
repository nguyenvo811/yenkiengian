using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSite.Areas.Admin.Models
{

    public class DropZoneModels
    {
        public DropZoneModels()
        {
            pis = new List<DropZoneItem>();
        }

        public List<DropZoneItem> pis { get; set; }
        public override string ToString()
        {
            return string.Join(";", pis.Select(i => i.reName));
        }
    }
    public class DropZoneItem
    {
        public string fileName { get; set; }
        public string reName { get; set; }
        public string largePicPath { get; set; }
        public string smallPicPath { get; set; }
         public double? size { get; set; }
        public string syntax { get; set; }//Folder Blogs/ Products .. = controller name
    }
}