using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FSite.Areas.Admin.Models
{
    //public class FileViewModels
    //{

    //}
    public class FileViewModel
    {
        public  FileViewModel()
        {
            Items = new HashSet<FileModel>();
        }
        public string DeleteUrl { get; set; }
        public string ActiveUrl { get; set; }
        public IEnumerable<FileModel> Items { get; set; }
    }
        public class FileModel
    {
        public string Id { get; set; }
        [Display(Name = "_Title", ResourceType = typeof(Resources.R_Admin))]
        public string Title { get; set; }
        [Display(Name = "_Path", ResourceType = typeof(Resources.R_Admin))]
        public string _Path { get; set; }
        [Display(Name = "_IsActive", ResourceType = typeof(Resources.R_Admin))]
        public bool? IsActive { get; set; }
        [Display(Name = "_Index", ResourceType = typeof(Resources.R_Admin))]
        public int? Index { get; set; }
    }
}