using System;
using System.ComponentModel.DataAnnotations;

namespace FSite.Models.Data
{
    public class Content
    {
        [Key]
        public decimal Id { get; set; }
        [Display(Name = "_Key", ResourceType = typeof(Resources.Resource))]
        [StringLength(50, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Code { get; set; }
        [Display(Name = "_Title", ResourceType = typeof(Resources.Resource))]
        [StringLength(500, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Title { get; set; }
        [Display(Name = "_Description", ResourceType = typeof(Resources.Resource))]
        public string Note { get; set; }
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string MetaType { get; set; }//Tên Class
        public string Meta { get; set; }  //c.Meta = Newtonsoft.Json.JsonConvert.SerializeObject(model);

        public Nullable<int> Status { get; set; }

        #region Sys
        [Display(Name = "_CreatedDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime? CreatedDate { get; set; }

        [Display(Name = "_CreatedById", ResourceType = typeof(Resources.Resource))]
        public string CreatedById { get; set; }
        [Display(Name = "_ModifiedDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime? ModifiedDate { get; set; }

        [Display(Name = "_ModifiedById", ResourceType = typeof(Resources.Resource))]
        public string ModifiedById { get; set; }
        #endregion
    }
}