using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSite.Models.Data
{
    public class Blog
    {
        public Blog()
        {
            this.BlogItems = new HashSet<BlogItem>();
        }
        [Key]
        public long Id { get; set; }
        [Display(Name = "_Key", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Key { get; set; }
        [Display(Name = "_Title", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Title { get; set; }

       [Display(Name = "_Title2", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Title2 { get; set; }

        [Display(Name = "_MetaTitle", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string MetaTitle { get; set; }

        [Display(Name = "_MetaKeyword", ResourceType = typeof(Resources.Resource))]
        [StringLength(2000, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
     
        public string MetaKeyword { get; set; }
        [Display(Name = "_MetaDescription", ResourceType = typeof(Resources.Resource))]
        [StringLength(2000, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
       
        public string MetaDescription { get; set; }

        [Display(Name = "_Description", ResourceType = typeof(Resources.Resource))]
        [StringLength(2000, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
    
        public string Description { get; set; }

        [Display(Name = "_CategoryId", ResourceType = typeof(Resources.Resource))]
        public int? CategoryId { get; set; }
        [Display(Name = "_Detail", ResourceType = typeof(Resources.Resource))]
     
        public string Detail { get; set; }
        [Display(Name = "_Link", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]

        public string Link { get; set; }

        [Display(Name = "_ImageUrl", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string ImageUrl { get; set; }

        [Display(Name = "_IsActive", ResourceType = typeof(Resources.Resource))]
        public bool? IsActive { get; set; }
        [Display(Name = "_IsFeature", ResourceType = typeof(Resources.Resource))]
        public bool? IsFeature { get; set; }
        [Display(Name = "_Viewed", ResourceType = typeof(Resources.Resource))]
        public int? Viewed { get; set; }
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

        public virtual BlogCategory Category { get; set; }
      
        public virtual ICollection<BlogItem> BlogItems { get; set; }
    }
}