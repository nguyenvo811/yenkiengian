using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSite.Areas.Admin.Models
{
    public class BreadcrumbViewModels
    {

        public BreadcrumbViewModels()
        {
            Items = new HashSet<BreadcrumbItemModel>();
        }
        public string Title { get; set; }
        public ICollection<BreadcrumbItemModel> Items { get; set; }
    }
    public class BreadcrumbItemModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}