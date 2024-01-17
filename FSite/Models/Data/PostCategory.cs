using System.ComponentModel.DataAnnotations;
namespace FSite.Models.Data
{
    public partial class PostCategory
    {      
        [Key]
        public int Id { get; set; }
  
        [Display(Name = "_Key", ResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Key { get; set; }
        [Display(Name = "_Title", ResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Title { get; set; }

        [Display(Name = "_Title2", ResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Title2 { get; set; }
        [Display(Name = "_MetaTitle", ResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
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
        [Display(Name = "_Link", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]

        public string Link { get; set; }

        [Display(Name = "_Index", ResourceType = typeof(Resources.Resource))]
        public int? Index { get; set; }
        [Display(Name = "_IsActive", ResourceType = typeof(Resources.Resource))]
        public bool? IsActive { get; set; }
        [Display(Name = "_IsFeature", ResourceType = typeof(Resources.Resource))]
        public bool? IsFeature { get; set; }

    }
}
