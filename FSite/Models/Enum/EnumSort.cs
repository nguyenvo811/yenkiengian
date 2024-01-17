using System.ComponentModel.DataAnnotations;

namespace FSite.Models.Enum
{ public enum EnumProductSort
    {
        [Display(Name = "Mới nhất", Description = "Id desc")]
        lastest,
        [Display(Name = "Cũ nhất", Description = "Id asc")]
        oldest,
        [Display(Name = "Sản phẩm nổi bật", Description = "Visitor desc")]
        popupar,
        [Display(Name = "Giá tăng dần", Description = "Price")]
        price,
        [Display(Name = "Giá giảm dần", Description = "Price desc")]
        price_desc
    }
    public enum EnumBlogSort
    {
        [Display(Name = "Ngày đăng mới nhất", Description = "Id desc")]
        lastest,
        [Display(Name = "Tin Xem nhiều nhất", Description = "Visitor desc")]
        popupar
    }
    public enum EnumServiceSort
    {
        [Display(Name = "Ngày đăng mới nhất", Description = "Id desc")]
        lastest,
        [Display(Name = "Tin Xem nhiều nhất", Description = "Visitor desc")]
        popupar
    } 
    public enum EnumAgencySort
    {
        [Display(Name = "Ngày đăng mới nhất", Description = "Id desc")]
        lastest,
        [Display(Name = "Tin Xem nhiều nhất", Description = "Visitor desc")]
        popupar
    }
    public enum EnumFaqSort
    {
        [Display(Name = "Ngày đăng mới nhất", Description = "Id desc")]
        lastest,
        [Display(Name = "Tin Xem nhiều nhất", Description = "Visitor desc")]
        popupar
    } public enum EnumSpecialistSort
    {
        [Display(Name = "Ngày đăng mới nhất", Description = "Id desc")]
        lastest,
        [Display(Name = "Tin Xem nhiều nhất", Description = "Visitor desc")]
        popupar
    }
}