using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSite.Models
{
    public class EditorTinicmeViewModel
    {
        public string id { get; set; }
        public string partical { get; set; }
        public string type { get; set; }
        public string syntax { get; set; }
        public string field_id { get; set; }

    }
    public class FileCropperViewModel
    {
        public string partical { get; set; }
        public string syntax { get; set; }
        public string field_id { get; set; }
        public string url { get; set; }//all path file
        public string filebase { get; set; }//result croper
    }
}