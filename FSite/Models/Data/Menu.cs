using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSite.Models.Data
{
    public class Menu
    {
        public Menu()
        {
            this.SubMenus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        
        [Display(Name = "_Key", ResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Key { get; set; }
       
        [Display(Name = "_Title", ResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Title { get; set; }
        [Display(Name = "_MenuIcon", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Icon { get; set; }

        [Display(Name = "_Link", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Link { get; set; }

        [Display(Name = "_Description", ResourceType = typeof(Resources.Resource))]
        [StringLength(2000, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Description { get; set; }

        [Display(Name = "_IsActive", ResourceType = typeof(Resources.Resource))]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "_ParentId", ResourceType = typeof(Resources.Resource))]
        public Nullable<int> ParentId { get; set; }
        [JsonIgnore]
        //[ScriptIgnore]
        public virtual Menu Parent { get; set; }

        [Display(Name = "_Index", ResourceType = typeof(Resources.Resource))]
        public Nullable<int> Index { get; set; }

        [Display(Name = "_MenuTypeKey", ResourceType = typeof(Resources.Resource))]
        public Enum.EnumTypeMenu? TypeKey { get; set; }

        public virtual ICollection<Menu> SubMenus { get; set; }

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