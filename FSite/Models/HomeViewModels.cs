using FSite.Models.Data;
using FSite.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSite.Models
{
    public class HomeViewModels
    { public HomeViewModels()
        {
            Featureblogs = new HashSet<Blog>();
            // Featureservices = new HashSet<Service>();

            //  Featurefaqs = new HashSet<Faq>();

        }
        //public List<Product> Products { get; set; }
        public IEnumerable<Blog> Featureblogs { get; set; }
        // public IEnumerable<Service> Featureservices { get; set; }
        // public IEnumerable<Faq> Featurefaqs { get; set; }

    }
    public class HomeProductGroupModel: HomeProductGroupChildModel
    {
        public HomeProductGroupModel()
        {
            Subs = new HashSet<HomeProductGroupModel>();
            Features = new HashSet<Product>();
            Features = new HashSet<Product>();
        }
        public string Image { get; set; }

        public virtual IEnumerable<HomeProductGroupChildModel> Subs { get; set; }
        public virtual IEnumerable<Product> Features { get; set; }
        public virtual IEnumerable<Product> Lastest { get; set; }
    }

    public class HomeProductGroupChildModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
    }
      

    public class RequestViewModel
    {
        [Display(Name = "RequesterFullName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string FullName { get; set; }
        [Display(Name = "RequesterEmail", ResourceType = typeof(Resources.Resource))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "EmailAddressValidMes", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }
        [Phone(ErrorMessage = null, ErrorMessageResourceName = "PhoneValidMes", ErrorMessageResourceType = typeof(Resources.Resource))]
        //[AttributePhoneHelpers]
        [Display(Name = "RequesterPhone", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(20, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]

        public string Phone { get; set; }
        [Display(Name = "RequesterContent", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]

        [MaxLength(2000, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Content { get; set; }

        [Display(Name = "RequesterDate", ResourceType = typeof(Resources.Resource))]
        public DateTime? RequestDate { get; set; }

        public string RequestDateStr
        {
            get
            {
                if (RequestDate.HasValue)
                {
                    return Helpers.CommonHelper.DateDisplay(RequestDate.Value);
                }
                return string.Empty;
            }

           
        }

        
    }


    #region Contact
    public class ContactViewModels
    {
        public ContactViewModels()
        {
            Offices = new List<OfficeModels>();
        }
        public List<OfficeModels> Offices { get; set; }
    }

    public class OfficeModels
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string FullName { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        //public double Lat { get; set; }
        //public double Lng { get; set; }
        public bool? IsHead { get; set; }
        public bool? IsHideMap { get; set; }
        public virtual OfficeCategoryModels Category { get; set; }
    }
    public class OfficeCategoryModels
    {
        public int? Id { get; set; }
        public string Title { get; set; }
    }
    public class ContactModels
    {
        [Display(Name = "FCustomerName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string FullName { get; set; }
        [Display(Name = "FEmail", ResourceType = typeof(Resources.Resource))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "EmailAddressValidMes", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }
        [Phone(ErrorMessage = null, ErrorMessageResourceName = "FPhoneValidMes", ErrorMessageResourceType = typeof(Resources.Resource))]
        // [AttributePhoneHelpers]
        [Display(Name = "FCustomerPhone", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(20, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]

        public string Phone { get; set; }
        [Display(Name = "FCustomerContent", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "StringEmptyMessage", ErrorMessageResourceType = typeof(Resources.Resource))]

        [MaxLength(2000, ErrorMessage = null, ErrorMessageResourceName = "StringLengthMessage", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Content { get; set; }


    }
    #endregion

    #region Product
    public class ProductSearchViewModel
    {
        public ProductSearchViewModel()
        {
         //   Category = new List<string>();
        }
        public string Key { get; set; }
        public string Category { get; set; }
       // public List<string> Category { get; set; }//mutil v1,v2,v3...
        public string Brand { get; set; }
        public string PriceFrom { get; set; }
        public string PriceTo { get; set; }
    }


    #endregion

    #region Blog
    public class BlogSearchViewModel
    {
        public string Key { get; set; }
        public string Category { get; set; }
    }


    #endregion
    #region Service
    public class ServiceSearchViewModel
    {
        public string Key { get; set; }
        public string Category { get; set; }
    }
    #endregion

    #region Agency
    public class AgencySearchViewModel
    {
        [Display(Name = "AgencySearchKey", ResourceType = typeof(Resources.Resource))]
        public string Key { get; set; }
        //[Display(Name = "AgencySearchCategory", ResourceType = typeof(Resources.Resource))]
        //public string Category { get; set; }
        [Display(Name = "AgencySearchSpecialist", ResourceType = typeof(Resources.Resource))]
        public string Specialist { get; set; }
    }
    #endregion 
    #region Faq
    public class FaqSearchViewModel
    {
        [Display(Name = "FaqSearchKey", ResourceType = typeof(Resources.Resource))]
        public string Key { get; set; }
      
        [Display(Name = "FaqSearchCategory", ResourceType = typeof(Resources.Resource))]
        public string Category { get; set; }
    }
    #endregion 
    #region Specialist
    public class SpecialistSearchViewModel
    {
        [Display(Name = "SpecialistSearchKey", ResourceType = typeof(Resources.Resource))]
        public string Key { get; set; }
        [Display(Name = "SpecialistSearchCategory", ResourceType = typeof(Resources.Resource))]
        public string Category { get; set; }

    }
    #endregion

    #region Widget
    public class SliderSearchViewModel
    {
        public EnumSliderModules type { get; set; }
        public string Id { get; set; }//id to get items 
    }
    #endregion
}